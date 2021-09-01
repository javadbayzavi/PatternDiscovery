using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using discovery.Library.Core;
using discovery.Library.analyzer;
using discovery.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static discovery.Models.patternsviewmodel;
using discovery.Library.identity;

namespace discovery.Controllers
{
    public class analyzeController : BaseController
    {
        public analyzeController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        // GET: analyze
        public ActionResult Index()
        {
            this.setPageTitle("Index");

            //Scenario Checking
            ViewBag.currentScenario = false;
            ViewBag.notready = false;

            if (this.currentScenario < 1)
            {
                ViewBag.currentScenario = true;
                return View();
            }

            ViewBag.notready = (this.getCurrentScenario().status < (int)scenariostatus.Importted);

            return View();
        }

        // GET: analyze/conventional
        public ActionResult conventional()
        {
            this.setPageTitle("conventional");

            ViewBag.wrongmethod = (this.getCurrentScenario().method == (int)scenarionmethod.AIBased);
            if ((bool)ViewBag.wrongmethod)
                return View(new List<patternsviewmodel>());

            //Load pattern 
            return View(this.ormProxy.patterns.Include(a => a.category)
                .Where(sc => sc.category.ownerID == User.GetUserId())
                .Select(item => new patternsviewmodel 
            { 
                categoryId = item.categoryId,
                categoryTitle = item.category.category,
                ID = item.ID,
                title = item.title
            }));
        }

        // POST: analyze/conventional
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> conventional(int[] id)
        {
            var sid = Convert.ToInt32(this._session.GetString(Keys._CURRENTSCENARIO));
            try
            {
                using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    //Delete all previous
                    foreach (var resultItem in this.ormProxy.result.Where(a => a.scenarioid == sid))
                        this.ormProxy.Remove(resultItem);

                    this.ormProxy.SaveChanges();

                    foreach (var pattern in id)
                    {
                        var scenarioMethod = this.getCurrentScenario().method.ToString();

                        //Create analyzing interface throguh factory method and inject dbcontext as a submitter to it
                        Analyzer analyzer = AnalyzerFactory.createAnalyzer(scenarioMethod,this.ormProxy,this.currentScenario);

                        //Runing the whole analyzing process through Async process

                        try
                        {
                            //Using strategy pattern to change the default Analyzing function of the analyzer to conventional execution
                            analyzer.Analyze(pattern.ToString(), new ConventionalExecutor(analyzer));
                        }
                        catch (Exception ex)
                        {
                            this._session.SetString(Keys._MSG, "Analyzing process has failed for. Please restart the process");
                            return RedirectToAction("conventional");
                        }
                    }
                    //Update the current scenario status
                    var scen = getCurrentScenario();
                    //Set the status to downloaded
                    scen.status = (int)scenariostatus.Analyzed;
                    this.ormProxy.scenario.Update(scen);

                    await this .ormProxy.SaveChangesAsync();

                    transaction.Complete();
                }

                this._session.SetString(Keys._MSG, "Analyzing completed!");

                return RedirectToAction("Index",new { controller = "result" });
            }
            catch (Exception ex)
            {
                this._session.SetString(Keys._MSG, "Analyzing process internal error");
                return RedirectToAction("conventional");
            }
        }

        public ActionResult ai()
        {
            this.setPageTitle("ai");

            ViewBag.wrongmethod = (this.getCurrentScenario().method == (int)scenarionmethod.Conventional);
            if ((bool)ViewBag.wrongmethod)
                return View(new List<patternsviewmodel>());

            //Load pattern 
            return View(this.ormProxy.patterns.Include(a => a.category)
                .Where(sc => sc.category.ownerID == User.GetUserId())
                .Select(item => new patternsviewmodel
                    {
                        categoryId = item.categoryId,
                        categoryTitle = item.category.category,
                        ID = item.ID,
                        title = item.title
                    }));
        }

        // POST: analyze/ai
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ai(int[] id)
        {
            var sid = Convert.ToInt32(this._session.GetString(Keys._CURRENTSCENARIO));
            try
            {
                using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    //Delete all previous
                    foreach (var resultItem in this.ormProxy.result.Where(a => a.scenarioid == sid))
                        this.ormProxy.Remove(resultItem);

                    this.ormProxy.SaveChanges();
                    string inputPattern = "";
                    //Fetch the method of current scenario
                    var scenarioMethod = this.getCurrentScenario().method.ToString();

                    foreach (var pattern in id)
                    {
                        //Contatinate all selected patterns to make a long inputString
                        //inputPattern += this.ormProxy.patterns.Find(pattern).title + ",";
                        inputPattern += pattern.ToString()+ ",";
                    }

                    //Create analyzing interface throguh factory method and inject dbcontext as a submitter to it
                    Analyzer analyzer = AnalyzerFactory.createAnalyzer(scenarioMethod, this.ormProxy, this.currentScenario);

                    try
                    {
                        
                        //Runing the whole analyzing process through Async process
                        analyzer.Analyze(inputPattern);
                    }
                    catch (Exception ex)
                    {
                        this._session.SetString(Keys._MSG, "Analyzing process has failed for. Please restart the process");
                        return RedirectToAction("ai");
                    }
                    //Update the current scenario status
                    var scen = getCurrentScenario();
                    //Set the status to downloaded
                    scen.status = (int)scenariostatus.Analyzed;
                    this.ormProxy.scenario.Update(scen);

                    this.ormProxy.SaveChanges();

                    transaction.Complete();
                }

                this._session.SetString(Keys._MSG, "Analyzing completed!");

                return RedirectToAction("Index", new { controller = "result" });
            }
            catch (Exception ex)
            {
                this._session.SetString(Keys._MSG, "Analyzing process internal error");
                return RedirectToAction("ai");
            }
        }
        //Hook method for scenrio cheking
        public override bool needScenario()
        {
            return true;
        }

        //template method for setting the title of each page
        public override void setPageTitle(string actionRequester)
        {
            string _pageTitle = "";

            switch (actionRequester)
            {
               case "Index":
                _pageTitle = "Index";
                break;
                case "conventional":
                    _pageTitle = "Conventional Analyze";
                    break;
                case "ai":
                    _pageTitle = "AI-Based Analyze";
                    break;
                default:
                    _pageTitle = "Analyzing...";
                    break;
            }

            ViewBag.Title = _pageTitle;
        }
    }
}