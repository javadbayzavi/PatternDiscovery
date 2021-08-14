using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace discovery.Controllers
{
    public class patternsController : Controller
    {
        // GET: patterns
        public ActionResult Index()
        {
            return View();
        }

        // GET: patterns/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: patterns/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: patterns/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
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

        // GET: patterns/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: patterns/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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