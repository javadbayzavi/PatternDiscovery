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
    public class patternsController : BaseController
    {
        public patternsController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        // GET: patterns
        public ActionResult Index()
        {
            this.setPageTitle("Index");

            var res = this.ormProxy.patterns.Include(a => a.category)
                .Where(sc => sc.category.ownerID == User.GetUserId())
                .Select(item =>
                new patternsviewmodel()

                {
                    categoryId = item.categoryId,
                    categoryTitle = item.category.category,
                    ID = item.ID,
                    title = item.title
                }
                );
            return View(res);
        }

        // GET: patterns/Create
        public ActionResult Create()
        {
            this.setPageTitle("Create");

            //Load category models from a hook method
            ViewBag.category = new SelectList(this.ormProxy.categories.Where(sc => sc.ownerID == User.GetUserId()), "ID", "category");
            return View();
        }

        // POST: patterns/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(patterns collection)
        {
            try
            {
                this.ormProxy.patterns.Add(collection);
                this.ormProxy.SaveChanges();
                this._session.SetString(Keys._MSG, ExceptionType.Info + "Pattern Successfully Created");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: patterns/Edit/5
        public ActionResult Edit(int id)
        {
            this.setPageTitle("Edit");

            var item = this.ormProxy.patterns.First(a => a.ID == id);
            //Load category models from a hook method
            ViewBag.category = new SelectList(this.ormProxy.categories.Where(sc => sc.ownerID == User.GetUserId()), "ID", "category");
            return View(item);
        }

        // POST: patterns/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, patterns collection)
        {
            try
            {
                this.ormProxy.patterns.Update(collection);
                this.ormProxy.SaveChanges();
                this._session.SetString(Keys._MSG, ExceptionType.Info + "Pattern Successfully Updated");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            this.setPageTitle("Delete");

            try
            {
                var item = this.ormProxy.patterns.FirstOrDefault(a => a.ID == id);
                this.ormProxy.patterns.Remove(item);
                this.ormProxy.SaveChanges();
                this._session.SetString(Keys._MSG, ExceptionType.Info + "Pattern Successfully Deleted");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                this._session.SetString(Keys._MSG, ExceptionType.Eror + "Delete pattern Failed");
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
                    _pageTitle = "Pattern Management";
                    break;
                case "Create":
                    _pageTitle = "Create Pattern";
                    break;
                case "Delete":
                    _pageTitle = "Delete Pattern";
                    break;
                case "Edit":
                    _pageTitle = "Edit Pattern";
                    break;
                default:
                    _pageTitle = "Pattern Management";
                    break;
            }

            ViewBag.Title = _pageTitle;
        }
    }
}