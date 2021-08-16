using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using discovery.Library.Core;
using discovery.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace discovery.Controllers
{
    public class patternsController : BaseController
    {
        // GET: patterns
        public ActionResult Index()
        {
            var res = this.ormProxy.patterns.Select(item =>
                new patternsviewmodel()

                {
                    category = item.category.ToString(),
                    ID = item.ID,
                    title = item.title
                }
                );
            return View(res);
        }

        // GET: patterns/Create
        public ActionResult Create()
        {
            //Load category models from a hook method
            ViewBag.category = new SelectList(patternsviewmodel.getCategories(), "Value", "Text");
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
            var item = this.ormProxy.patterns.First(a => a.ID == id);
            //Load category models from a hook method
            ViewBag.category = new SelectList(patternsviewmodel.getCategories(), "Value", "Text", item.category);
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
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                var item = this.ormProxy.patterns.FirstOrDefault(a => a.ID == id);
                this.ormProxy.patterns.Remove(item);
                this.ormProxy.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}