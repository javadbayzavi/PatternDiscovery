using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using discovery.Library.Core;
using discovery.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            this.setPageTitle("Index");

            return View();
        }
        public IActionResult list()
        {
            this.setPageTitle("list");

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
                category = item.pattern.category.category,
                patternid = item.patternid,
                pattern = item.pattern.title ,
                count = item.count
            });
            return View(results);
        }

        public IActionResult charts()
        {
            this.setPageTitle("charts");

            ViewBag.rnkforcategories = new SelectList(this.ormProxy.categories, "ID", "category");
            ViewBag.categories = new SelectList(this.ormProxy.categories, "ID", "category");

            //Scenario Checking
            ViewBag.currentScenario = false;
            if (this.currentScenario < 1)
            {
                ViewBag.currentScenario = true;
            }

            return View();
        }
        public IActionResult overview()
        {
            this.setPageTitle("overview");

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
        //template method for setting the title of each page
        public override void setPageTitle(string actionRequester)
        {
            string _pageTitle = "";

            switch (actionRequester)
            {
                case "list":
                    _pageTitle = "Result List";
                    break;
                case "details":
                    _pageTitle = "Result Details";
                    break;
                case "charts":
                    _pageTitle = "Charts";
                    break;
                case "Index":
                    _pageTitle = "Result Overview";
                    break;
                default:
                    _pageTitle = "Result Details";
                    break;
            }

            ViewBag.Title = _pageTitle;
        }
    }
}