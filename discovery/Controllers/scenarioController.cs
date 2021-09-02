using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using discovery.Library.Core;
using discovery.Library.file;
using discovery.Library.zip;
using discovery.Models;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using discovery.Library.identity;
using System.Transactions;

namespace discovery.Controllers
{
    public partial class scenarioController : BaseController
    {
        public readonly UserManager<IdentityUser> _userManager;
        public scenarioController(IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager): base(httpContextAccessor)
        {
            this._userManager = userManager;
        }
        // GET: scenario
        public ActionResult Index()
        {
            this.setPageTitle("Index");

            ViewBag.currentScenario = this.HttpContext.Session.GetString(Keys._CURRENTSCENARIO);
            var scenarios = this.ormProxy.scenario.Where(sc => sc.ownerID == User.GetUserId()).Select(item => new scenarioviewmodel()
            {
                createddate = item.createddate,
                method = item.method.ToString(),
                status = item.status.ToString(),
                sourcetype = item.sourcetype.ToString(),
                name = item.name,
                ID = item.ID
            });
            // category models from a hook method
            return View(scenarios);
        }
        public ActionResult Reset(int id)
        {
            this.setPageTitle(Keys._SCENARIORESET);

            ViewBag.hadData = (this.ormProxy.dataset.Count(a => a.scenarioid == this.currentScenario) > 0);
            ViewBag.hasFiles = (System.IO.Directory.GetFiles(Keys._SCENARIODIRECTORY).Count(a => a.Contains(this.ormProxy.scenario.Where(bb => bb.ID == id).First().sversion.ToString())) > 0);
            ViewBag.id = id;

            return View();
        }
        
        [HttpPost]
        [ActionName(Keys._SCENARIORESET)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetConfirmed(int id)
        {
            var results = this.ormProxy.result.Where(a => a.scenarioid == id);
            var datasets = this.ormProxy.dataset.Where(a => a.scenarioid == id);

            TransactionOptions options = new TransactionOptions();
            options.Timeout = TimeSpan.MaxValue;
            options.IsolationLevel = IsolationLevel.ReadCommitted;

            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, options, TransactionScopeAsyncFlowOption.Enabled))
            {

                if (System.IO.File.Exists(Keys._SCENARIODIRECTORY + "/" + this.GetScenario(id).sversion.ToString() + Keys._TXT))
                    System.IO.File.Delete(Keys._SCENARIODIRECTORY + "/" + this.GetScenario(id).sversion.ToString() + Keys._TXT);

                string filename = GetScenario(id).sversion.ToString();
                var files = System.IO.Directory.GetFiles(Keys._TEMPDIRECTORY).Where(a => a.Contains(filename));

                foreach (var itt in files)
                {
                    if (System.IO.File.Exists(itt))
                        System.IO.File.Delete(itt);
                }

                this.ormProxy.dataset.RemoveRange(datasets);
                this.ormProxy.result.RemoveRange(results);
                var scenario = this.GetScenario(id);

                //Reset the status of the scenario
                scenario.status = (int)scenariostatus.Created;
                this.ormProxy.scenario.Update(scenario);

                await this.ormProxy.SaveChangesAsync();
                transaction.Complete();
            }

            this._session.SetString(Keys._MSG,ExceptionType.Info + Keys._SCENARIORESETT);
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Start(int id)
        {
            var currentScenario = this._session.GetString(Keys._CURRENTSCENARIO);
            if (currentScenario != null && currentScenario == id.ToString())
            {
                //Current scenario must be stopped
                this._session.SetString(Keys._CURRENTSCENARIO, "");
                this._session.SetString(Keys._MSG, ExceptionType.Info + Keys._SCENARIOSTOP);
            }
            else
            {
                this._session.SetString(Keys._CURRENTSCENARIO, id.ToString());
                this._session.SetString(Keys._MSG, ExceptionType.Info + Keys._SCENARIOSTART);
            }
            return RedirectToAction("Index");
        }
        // GET: scenario/Create
        public ActionResult Create()
        {
            this.setPageTitle(Keys._SCENARIOCREATE);

            //Load necessary dropdowns
            ViewBag.sourcetype = new SelectList(scenarioviewmodel.getTypes(), "Value", "Text");            
            ViewBag.method = new SelectList(scenarioviewmodel.getMethods(), "Value", "Text"); 
            return View();
        }

        // POST: scenario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(scenario collection)
        {
            try
            {
                //Add controlling information to scenario
                collection.datasource = "";
                collection.ownerID = User.GetUserId();
                collection.status = (int)scenariostatus.Created;
                collection.sversion = System.Guid.NewGuid();
                collection.createddate = DateTime.Now;

                this.ormProxy.scenario.Add(collection);
                this.ormProxy.SaveChanges();
                this._session.SetString(Keys._MSG, ExceptionType.Info + "Scenario Successfully Created");
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: scenario/Edit/5
        public ActionResult Edit(int id)
        {
            this.setPageTitle("Edit");

            var item = this.ormProxy.scenario.First(a => a.ID == id);
            //Load necessary dropdowns
            ViewBag.sourcetype = new SelectList(scenarioviewmodel.getTypes(), "Value", "Text",item.sourcetype);
            ViewBag.method = new SelectList(scenarioviewmodel.getMethods(), "Value", "Text",item.method);
            return View(item);
        }

        // POST: scenario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, scenario collection)
        {
            try
            {
                var item = this.ormProxy.scenario.First(a => a.ID == id);
                //only these fields of a scenario are alowed to be modified
                item.name = collection.name;
                item.method = collection.method;
                item.sourcetype = collection.sourcetype;

                this.ormProxy.scenario.Update(item);
                this.ormProxy.SaveChanges();
                this._session.SetString(Keys._MSG, ExceptionType.Info + "Scenario Successfully Updated");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                this._session.SetString(Keys._MSG, ExceptionType.Info + "Scenario Update Failed");
                return View();
            }
        }
        // GET: scenario/Details/5
        public ActionResult Details(int id)
        {
            this.setPageTitle("Details");

            var item = this.ormProxy.scenario.Where(a => a.ID == id).Select(a => new scenarioviewmodel
            {
                createddate = a.createddate,
                datasource = a.datasource,
                ID = a.ID,
                method = a.method.ToString(),
                name = a.name,
                sourcetype = a.sourcetype.ToString(),
                status = a.status.ToString(),
                sversion = a.sversion
            }).FirstOrDefault();
            return View(item);
        }

        // POST: scenario/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                TransactionOptions options = new TransactionOptions();
                options.Timeout = TimeSpan.MaxValue;
                options.IsolationLevel = IsolationLevel.ReadCommitted;
                using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, options, TransactionScopeAsyncFlowOption.Enabled))
                {

                    var results = this.ormProxy.result.Where(a => a.scenarioid == id);
                    var datasets = this.ormProxy.dataset.Where(a => a.scenarioid == id);

                    if (System.IO.File.Exists(Keys._SCENARIODIRECTORY + "/" + this.GetScenario(id).sversion.ToString() + Keys._TXT))
                        System.IO.File.Delete(Keys._SCENARIODIRECTORY + "/" + this.GetScenario(id).sversion.ToString() + Keys._TXT);

                    string filename = GetScenario(id).sversion.ToString();

                    var files = System.IO.Directory.GetFiles(Keys._TEMPDIRECTORY).Where(a => a.Contains(filename));

                    foreach (var itt in files)
                    {
                        if (System.IO.File.Exists(itt))
                            System.IO.File.Delete(itt);
                    }

                    this.ormProxy.dataset.RemoveRange(datasets);
                    this.ormProxy.result.RemoveRange(results);

                    await this.ormProxy.SaveChangesAsync();
                    this._session.SetString(Keys._MSG, ExceptionType.Info + Keys._SCENARIODELETE);

                    var item = this.ormProxy.scenario.FirstOrDefault(a => a.ID == id);
                    this.ormProxy.scenario.Remove(item);
                    this.ormProxy.SaveChanges();

                    transaction.Complete();

                }
                //Check whether if current scenario is active. it must be stopped
                var currentScenario = this._session.GetString(Keys._CURRENTSCENARIO);
                if (currentScenario != null && currentScenario == id.ToString())
                {
                    //Current scenario must be stopped
                    this._session.SetString(Keys._CURRENTSCENARIO, "");
                }

                    return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // POST: scenario/Delete/5
        public ActionResult Delete(int id)
        {
            this.setPageTitle(Keys._SCENARIODELETED);

            ViewBag.hadData = (this.ormProxy.dataset.Count(a => a.scenarioid == this.currentScenario) > 0);
            ViewBag.hasFiles = (System.IO.Directory.GetFiles(Keys._SCENARIODIRECTORY).Count(a => a.Contains(this.ormProxy.scenario.Where(bb => bb.ID == id).First().sversion.ToString())) > 0);
            ViewBag.id = id;

            return View();
        }

        private scenario GetScenario(int id)
        {
            return this.ormProxy.scenario.Find(id);
        }

        //Hook method for scenrio cheking
        public override bool needScenario()
        {
            return false;
        }

        //template method for setting the title of each page
        public override void setPageTitle(string actionRequester)
        {
            string _pageTitle = "";

            switch (actionRequester)
            {
                case "Index":
                    _pageTitle = "Scenario Management";
                    break;
                case "Create":
                    _pageTitle = "Create Scenario";
                    break;
                case "Edit":
                    _pageTitle = Keys._SCENARIOEDITTITLE;
                    break;
                case "Delete":
                    _pageTitle = Keys._SCENARIODELETETITLE;
                    break;
                case "Reset":
                    _pageTitle = Keys._SCENARIORESETTITLE;
                    break;
                case "Details":
                    _pageTitle = Keys._SCENARIODETAILSTITLE;
                    break;
                default:
                    _pageTitle = "Scenario Management";
                    break;
            }

            ViewBag.Title = _pageTitle;
        }
    }
}
