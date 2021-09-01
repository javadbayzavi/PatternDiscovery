using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using discovery.Library.Core;
using discovery.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace discovery
{
    public class HomeController : BaseController
    {
        public HomeController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            this.setPageTitle("Delete");

            //Scenario Checking
            ViewBag.currentScenario = false;
            if (this.currentScenario < 1)
            {
                ViewBag.currentScenario = true;
                return View(new scenario());
            }


            return View(this.getCurrentScenario());
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
                    _pageTitle = "Home";
                    break;
                default:
                    _pageTitle = "Home";
                    break;
            }

            ViewBag.Title = _pageTitle;
        }
    }
}
