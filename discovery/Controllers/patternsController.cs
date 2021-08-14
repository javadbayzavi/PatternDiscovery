using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using discovery.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace discovery.Controllers
{
    public class patternsController : Controller
    {
        // GET: patterns
        public ActionResult Index()
        {
            return View();
        }

        // GET: patterns/Create
        public ActionResult Create()
        {
            //Load category models from a hook method
            ViewBag.categories = new SelectList(patternsviewmodel.getCategories(), "Value", "Text");
            return View();
        }

        // POST: patterns/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {=
            try
            {
                // TODO: Add insert logic here

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
            return View();
        }

        // POST: patterns/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }



        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}