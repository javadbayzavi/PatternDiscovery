using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using discovery.Library.Core;
using discovery.Library.file;
using discovery.Library.zip;
using discovery.Models;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace discovery.Controllers
{
    public partial class scenarioController : BaseController
    {
        public scenarioController(IHttpContextAccessor httpContextAccessor): base(httpContextAccessor)
        {
        }
        // GET: scenario
        public ActionResult Index()
        {
            ViewBag.currentScenario = this.HttpContext.Session.GetString(Keys._CURRENTSCENARIO);

            var scenarios = this.ormProxy.scenario.Select(item => new scenarioviewmodel()
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
        public ActionResult Start(int id)
        {
            var currentScenario = this.HttpContext.Session.GetString(Keys._CURRENTSCENARIO);
            if (currentScenario != null && currentScenario == id.ToString())
            {
                //Current scenario must be stopped
                this.HttpContext.Session.SetString(Keys._CURRENTSCENARIO, "");
            }
            else
            {
                this.HttpContext.Session.SetString(Keys._CURRENTSCENARIO, id.ToString());
            }
            return RedirectToAction("Index");
        }
        // GET: scenario/Create
        public ActionResult Create()
        {
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
                collection.status = (int)scenariostatus.Created;
                collection.sversion = System.Guid.NewGuid();
                collection.createddate = DateTime.Now;

                this.ormProxy.scenario.Add(collection);
                this.ormProxy.SaveChanges();
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
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // GET: scenario/Details/5
        public ActionResult Details(int id)
        {
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

        // GET: scenario/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var item = this.ormProxy.scenario.FirstOrDefault(a => a.ID == id);
                this.ormProxy.scenario.Remove(item);
                this.ormProxy.SaveChanges();

                //Check whether if current scenario is active. it must be stopped
                var currentScenario = this.HttpContext.Session.GetString(Keys._CURRENTSCENARIO);
                if (currentScenario != null && currentScenario == id.ToString())
                {
                    //Current scenario must be stopped
                    this.HttpContext.Session.SetString(Keys._CURRENTSCENARIO, "");
                }

                    return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        //Hook method for scenrio cheking
        public override bool needScenario()
        {
            return false;
        }

        //Hook method for authentication cheking 
        public override bool needAuthentication()
        {
            return true;
        }
    }
}
