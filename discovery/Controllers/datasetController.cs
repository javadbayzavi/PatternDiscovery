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
        public datasetController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        // GET: dataset/Index
        public ActionResult Index()
        {
            this.setPageTitle("Index");

            return RedirectToAction("List");
        }
        // GET: dataset/List
        public ActionResult List()
        {
            this.setPageTitle("Index");

            ViewBag.currentScenario = (this._session.GetString(Keys._CURRENTSCENARIO) == null || this._session.GetString(Keys._CURRENTSCENARIO) == "");
            if ((bool)ViewBag.currentScenario)
                return View("List", new List<datasetviewmodel>());

            ViewBag.scenario = this.getCurrentScenario();

            var datasets = this.ormProxy.dataset.Where(a => a.scenarioid == this.currentScenario).Select(item =>
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
            this.setPageTitle("Create");

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
                //Current scenario must be stopped
                collection.scenarioid = Convert.ToInt32(this.currentScenario);
                this.ormProxy.dataset.Add(collection);
                this.ormProxy.SaveChanges();
                this._session.SetString(Keys._MSG, ExceptionType.Info + "Dataset Successfully Created");
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
            this.setPageTitle("Edit");

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
                this._session.SetString(Keys._MSG, ExceptionType.Info + "Dataset Successfully Updated");
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
            this.setPageTitle("Details");

            var item = this.ormProxy.dataset.First(a => a.ID == id);
            return View(item);
        }

        // GET: dataset/Delete/5
        public ActionResult Delete(int id)
        {
            this.setPageTitle("Delete");

            try
            {
                var item = this.ormProxy.dataset.FirstOrDefault(a => a.ID == id);
                this.ormProxy.dataset.Remove(item);
                this.ormProxy.SaveChanges();
                this._session.SetString(Keys._MSG, ExceptionType.Info + "Dataset Successfully Deleted");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        //Hook method for scenrio cheking
        public override bool needScenario()
        {
            return true;
        }

        //template method for setting the title of each page
        public override void setPageTitle(string actionRequester)
        {
            string _pageTitle = "";

            switch (actionRequester)
            {
                case "Index":
                    _pageTitle = "Dataset Management";
                    break;
                case "Create":
                    _pageTitle = "Create Dataset Item";
                    break;
                case "Edit":
                    _pageTitle = "Edit Dataset Item";
                    break;
                case "Delete":
                    _pageTitle = "Delete Dataset Item";
                    break;
                case "Details":
                    _pageTitle = "Details of Dataset Item";
                    break;
                default:
                    _pageTitle = "Dataset Management";
                    break;
            }

            ViewBag.Title = _pageTitle;
        }
    }
}
