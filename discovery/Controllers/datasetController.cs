using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using discovery.Library.Core;
using discovery.Library.file;
using discovery.Library.zip;
using discovery.Models;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace discovery.Controllers
{
    public partial class datasetController : BaseController
    {
        // GET: dataset/Index
        public ActionResult Index()
        {
                return RedirectToAction("List");
        }
        // GET: dataset/List
        public ActionResult List()
        {
            ViewBag.currentScenario = (this.HttpContext.Session.GetString(Keys._CURRENTSCENARIO) == null || this.HttpContext.Session.GetString(Keys._CURRENTSCENARIO) == "");
            if ((bool)ViewBag.currentScenario)
                return View("List", new List<datasetviewmodel>());

            var currentScenario = Convert.ToInt32(this.HttpContext.Session.GetString(Keys._CURRENTSCENARIO));

            var datasets = this.ormProxy.dataset.Where(a => a.scenarioid == currentScenario).Select(item =>
            new datasetviewmodel()
            {
                author = item.author,
                date = item.date,
                ID = item.ID,
                subject = item.subject
            });
            //Load category models from a hook method
            return View(datasets);
        }

        // GET: dataset/Create
        public ActionResult Create()
        {
            //Load category models from a hook method
            return View();
        }

        // POST: dataset/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(dataset collection)
        {
            try
            {
                //Adding current scenario to it
                var currentScenario = this.HttpContext.Session.GetString(Keys._CURRENTSCENARIO);
                if (currentScenario != null)
                {
                    //Current scenario must be stopped
                    collection.scenarioid = Convert.ToInt32(currentScenario);
                }
                    this.ormProxy.dataset.Add(collection);
                this.ormProxy.SaveChanges();
                return RedirectToAction("List");
            }
            catch
            {
                return View();
            }
        }

        // GET: dataset/Edit/5
        public ActionResult Edit(int id)
        {
            var item = this.ormProxy.dataset.First(a => a.ID == id);
            return View(item);
        }

        // POST: dataset/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, dataset collection)
        {
            try
            {
                this.ormProxy.dataset.Update(collection);
                this.ormProxy.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // GET: dataset/Details/5
        public ActionResult Details(int id)
        {
            var item = this.ormProxy.dataset.First(a => a.ID == id);
            return View(item);
        }

        // GET: dataset/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var item = this.ormProxy.dataset.FirstOrDefault(a => a.ID == id);
                this.ormProxy.dataset.Remove(item);
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
