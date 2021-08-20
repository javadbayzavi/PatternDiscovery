using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using discovery.Library.Core;
using discovery.Library.text.analyzer;
using discovery.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using static discovery.Models.patternsviewmodel;

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
            ViewBag.wrongmethod = (this.getCurrentScenario().method == (int)scenarionmethod.AIBased);
            if ((bool)ViewBag.wrongmethod)
                return View(new List<patternsviewmodel>());

            //Load pattern 
            return View(this.ormProxy.patterns.Select(item => new patternsviewmodel 
            { 
                category = item.category.ToString(),
                ID = item.ID,
                title = item.title
            }));
        }

        // POST: analyze/conventional
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult conventional(int[] id)
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
                        //Create analyzing interface throguh factory method and inject dbcontext as a submitter to it
                        Analyzer analyzer = AnalyzerFactory.GetConventionalAnalyzer(new ConventionalSubmitter(this.ormProxy));

                        //Runing the whole analyzing process through Async process

                        try
                        {
                            analyzer.Analyze(pattern.ToString());
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

                    this.ormProxy.SaveChanges();

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

        //Hook method for scenrio cheking
        public override bool needScenario()
        {
            return true;
        }

        //Hook method for authentication cheking 
        public override bool needAuthentication()
        {
            return true;
        }
    }
}