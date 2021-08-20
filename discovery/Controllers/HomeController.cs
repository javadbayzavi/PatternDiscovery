using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using discovery.Library.Core;
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
            Redirect("https://www.google.com");
            return View();
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
