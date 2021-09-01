using discovery.Library.Core;
using discovery.Library.identity;
using discovery.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace discovery.Controllers
{
    public class AccountController : BaseController
    {
        public readonly UserManager<IdentityUser> _userManager;
        public readonly SignInManager<IdentityUser> _signInManager;
        public AccountController(IHttpContextAccessor httpContextAccessor,UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signinManager): base(httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signinManager;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            this.setPageTitle("Register");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser()
                {
                    UserName = model.Username,
                    EmailConfirmed = true,
                    Email = model.Email
                };
                var res = await this._userManager.CreateAsync(user, model.Password);
                var res2 = await _userManager.AddToRoleAsync(user, Roles.Basic.ToString());
                if (res.Succeeded)
                {

                    return RedirectToAction(nameof(Index), "Home");
                }
                else
                {
                    foreach (var item in res.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                    }
                }
            }

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await this._signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index),"Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            this.setPageTitle("Login");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {

                var res = await this._signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);
                if(res.Succeeded)
                {
                    return RedirectToAction(nameof(Index), "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Login Failed");
                }
            }

            return View(model);
        }

        public IActionResult Profile()
        {
            this.setPageTitle("Profile");
            return View();
        }

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
                case "Register":
                    _pageTitle = "Register new account";
                    break;
                case "Login":
                    _pageTitle = "Login";
                    break;
                case "Profile":
                    _pageTitle = "Profile";
                    break;
                default:
                    _pageTitle = "Login";
                    break;
            }

            ViewBag.Title = _pageTitle;
        }

    }
}
