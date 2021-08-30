using discovery.Library.identity;
using discovery.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace discovery.Controllers
{
    public class AccountController : Controller
    {
        public readonly UserManager<IdentityUser> _userManager;
        public readonly SignInManager<IdentityUser> _signInManager;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signinManager)
        {
            _userManager = userManager;
            _signInManager = signinManager;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
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
            return View();
        }
    }
}
