using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using discovery.Library.Core;
using discovery.Models;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace discovery.Controllers
{
    public class datasetController : BaseController
    {
        // GET: dataset/
        public ActionResult Index()
        {
            return View();
        }

        // GET: dataset/FetchRemoteFiles
        public ActionResult FetchRemoteFiles()
        {
            return View();
        }

        // GET: dataset/LoadRemoteFileList
        public ActionResult LoadRemoteFileList(string url)
        {
            try
            {
                HtmlWeb hw = new HtmlWeb();
                HtmlDocument doc = hw.Load("");
                foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
                {

                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: dataset/NormalizeData/
        public ActionResult NormalizeData()
        {
            return View();
        }

        // GET: dataset/Edit/5
        public ActionResult Create()
        {
            try
            {
                return View();
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