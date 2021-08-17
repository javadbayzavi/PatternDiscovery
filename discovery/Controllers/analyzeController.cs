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

namespace discovery.Controllers
{
    public class analyzeController : BaseController
    {
        // GET: conventional
        public ActionResult Index()
        {
            return View();
        }

        // GET: conventional/Analyze
        public ActionResult conventional()
        {
            //Load pattern 
            return View(this.ormProxy.patterns);
        }

        // POST: conventional/Analyze
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult conventional(int[] id)
        {
            try
            {
                foreach (var pattern in id)
                {
                    //Create analyzing interface throguh factory method and inject dbcontext as a submitter to it
                    Analyzer analyzer = AnalyzerFactory.GetConventionalAnalyzer(new ConventionalSubmitter(this.ormProxy));

                    //Runing the whole analyzing process through Async process
                    using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        try
                        {
                            analyzer.Analyze(pattern.ToString());
                            transaction.Complete();
                        }
                        catch (Exception)
                        {
                            return RedirectToAction("conventional");
                        }
                    }
                }

                return RedirectToAction("Index",new { controller = "result" });
            }
            catch
            {
                return RedirectToAction("conventional");
            }
        }
    }
}