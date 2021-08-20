using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace discovery.Controllers
{
    public class userController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Login(string userInfo)
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(string userInfo)
        {
            return View();
        }

        public IActionResult Logout()
        {
            return View();
        }
    }
}
