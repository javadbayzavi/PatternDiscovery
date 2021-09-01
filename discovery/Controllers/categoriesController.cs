using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using discovery.Library.Core;
using discovery.Library.identity;
using discovery.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace discovery.Controllers
{
    public class categoriesController : BaseController
    {
        public categoriesController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        // GET: categories
        public ActionResult Index()
        {
            this.setPageTitle("Index");

            var res = this.ormProxy.categories.Where(sc => sc.ownerID == User.GetUserId());
            return View(res);
        }

        // GET: categories/Create
        public ActionResult Create()
        {
            this.setPageTitle("Create");
            return View();
        }

        // POST: categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(categoryItem collection)
        {
            try
            {
                collection.ownerID = User.GetUserId();

                this.ormProxy.categories.Add(collection);
                this.ormProxy.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: categories/Edit/5
        public ActionResult Edit(int id)
        {
            this.setPageTitle("Edit");

            var item = this.ormProxy.categories.First(a => a.ID == id);
            return View(item);
        }

        // POST: categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, categoryItem collection)
        {
            try
            {
                this.ormProxy.categories.Update(collection);
                this.ormProxy.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // POST: categories/Edit/5
        public ActionResult Delete(int id)
        {
            this.setPageTitle("Delete");

            try
            {
                var item = this.ormProxy.categories.FirstOrDefault(a => a.ID == id);
                this.ormProxy.categories.Remove(item);
                this.ormProxy.SaveChanges();
                this._session.SetString(Keys._MSG, "Category Successfully Deleted");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                this._session.SetString(Keys._MSG, "Delete category Failed");
                return View(nameof(Index));
            }
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
                    _pageTitle = "Category Management";
                    break;
                case "Create":
                    _pageTitle = "Create Category";
                    break;
                case "Delete":
                    _pageTitle = "Delete Category";
                    break;
                case "Edit":
                    _pageTitle = "Edit Category";
                    break;
                default:
                    _pageTitle = "Category Management";
                    break;
            }

            ViewBag.Title = _pageTitle;
        }
    }
}