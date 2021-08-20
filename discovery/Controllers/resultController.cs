using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using discovery.Library.Core;
using discovery.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static discovery.Models.patternsviewmodel;

namespace discovery.Controllers
{
    //This controller handle the presentation of text analysis results in various formats
    public class resultController : BaseController
    {
        public resultController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        public IActionResult Index()
        {

            //Scenario Checking
            ViewBag.currentScenario = (this.currentScenario < 1);
            if ((bool)ViewBag.currentScenario)
            {
                ViewBag.scenario = new scenario();
                return View(new List<resultviewmodel>());
            }

            ViewBag.scenario = this.getCurrentScenario();

            //Load result for the current scenario
            var results = this.ormProxy.result.Where(a => a.scenarioid == this.currentScenario)
                .Select(item => new resultviewmodel
            {
                subject = item.datasetItem.subject,
                ID = item.ID,
                datasetid = item.datasetitemid,
                category = (categories)item.pattern.category,
                patternid = item.patternid,
                pattern = item.pattern.title ,
                count = item.count
            });
            return View(results);
        }

        public IActionResult charts()
        {
            return View();
        }
        public IActionResult overview()
        {
            return View();
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