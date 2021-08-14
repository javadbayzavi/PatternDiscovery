using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace discovery.Controllers
{
    //This controller handle the presentation of text analysis results in various formats
    public class reportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}