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
        // GET: conventional
        public ActionResult Index()
        {
            //Scenario Checking
            ViewBag.currentScenario = (this.HttpContext.Session.GetString(Keys._CURRENTSCENARIO) == null || this.HttpContext.Session.GetString(Keys._CURRENTSCENARIO) == "");
            return View();
        }

        // GET: conventional/Analyze
        public ActionResult conventional()
        {
            //Load pattern 
            return View(this.ormProxy.patterns.Select(item => new patternsviewmodel 
            { 
                category = item.category.ToString(),
                ID = item.ID,
                title = item.title
            }));
        }

        // POST: conventional/Analyze
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult conventional(int[] id)
        {
            var sid = Convert.ToInt32(this.HttpContext.Session.GetString(Keys._CURRENTSCENARIO));
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
                            return RedirectToAction("conventional");
                        }
                    }
                    transaction.Complete();
                }

                return RedirectToAction("Index",new { controller = "result" });
            }
            catch (Exception ex)
            {
                return RedirectToAction("conventional");
            }
        }
    }
}