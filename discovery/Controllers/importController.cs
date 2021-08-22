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
    public partial class importController : BaseController
    {
        private List<IReadable> documents = new List<IReadable>();
        public importController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        // GET: import
        public ActionResult Index()
        {
            this.setPageTitle("Index");

            //Scenario Checking
            ViewBag.currentScenario = false;
            if (this.currentScenario < 1)
            {
                ViewBag.currentScenario = true;
                return View(new scenario());
            }


            //Show the status of the current scenario
            string url = this._session.GetString(Keys._REMOTEURL);

            return View(this.getCurrentScenario());
        }

        // post: import/DownloadFiles
        [HttpPost]
        public ActionResult DownloadFiles(int[] id)
        {
            string url = this._session.GetString(Keys._REMOTEURL);
            //Log the download vesion for the scenario
            Fileoperations.WriteTofile(Keys._SCENARIODIRECTORY + "/" + getCurrentScenario().sversion.ToString() + ".txt", url);

            var version = getCurrentScenario().sversion.ToString();

            var downlaoded = Fileoperations.downloadFiles(ref id , version);
            if (downlaoded)
            {
                //After successfully downloading all remote files session address sets to null that shows the operation can run from the start again
                this._session.SetString(Keys._REMOTEURL, "");

                //Update the current scenario status
                var scen = getCurrentScenario();
                //Set the status to downloaded
                scen.status = (int)scenariostatus.Downloaded;
                scen.datasource = url;
                this.ormProxy.scenario.Update(scen);
                this.ormProxy.SaveChanges();

                this._session.SetString(Keys._MSG, "All Selected files have downloaded");

                return RedirectToAction(nameof(Index));
            }
            else
            {
                this._session.SetString(Keys._MSG, "File donwload failed");
                return RedirectToAction("LoadRemoteFileList");
            }
        }

        //GET: import/LoadRemoteFileList
        public ActionResult LoadRemoteFileList()
        {
            this.setPageTitle("LoadRemoteFileList");

            ViewBag.wrongtype = false;
            var item = getCurrentScenario();
            ViewBag.sourcetype = ((scenariosourcetype)item.sourcetype).ToString();
            if (item.sourcetype != (int)scenariosourcetype.File)
                ViewBag.wrongtype = true;

            return View();
        }

        //POST: import/LoadRemoteFileList
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoadRemoteFileList(string url)
        {
            ViewBag.wrongtype = false;
            var item = getCurrentScenario();
            ViewBag.sourcetype = ((scenariosourcetype)item.sourcetype).ToString();
            if (item.sourcetype != (int)scenariosourcetype.File)
                ViewBag.wrongtype = true; 
            
            try
            {
                var files = Fileoperations.getFileList(ref url).Select(a =>
                    new filelistview()
                        {
                            filename = a.filename,
                            fileurl = a.fileurl
                        }
                    ).ToList();
                this._session.SetString(Keys._REMOTEURL,url);
                return View(files);
            }
            catch(Exception ex)
            {
                return View();
            }
        }
        public ActionResult ImportData()
        {
            this.setPageTitle("ImportData");

            ViewBag.downlaoded = true;
            if (getCurrentScenario().status < (int) scenariostatus.Downloaded)
                ViewBag.downlaoded = false;
            //Number of files downloaded for this scenario
            ViewBag.filenumbers = System.IO.Directory.GetFiles(Keys._TEMPDIRECTORY).Where(a => a.Contains(this.currentScenario.ToString())).Count();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // GET: import/ImportData/
        public ActionResult ImportData(string version)
        {
            //Check if the user want to import data for scenario that has no downloaded file


            //Use factory method to create appropriate file document based on downloaded file in temp directory
            var files = System.IO.Directory.GetFiles(Keys._TEMPDIRECTORY).Select(a =>
                new filefactory(a).createData()
            );

            //temp list of extracted data from documents
            var datasets = new List<dataset>();
            foreach (var file in files)
            {
                var item = new dataset();
                //Read document content through template method
                file.Read(ref item);

                item.scenarioid = this.currentScenario;

                datasets.Add(item);

                //Check for memory overloads and dump the list into databse
                if(new performancetuner().MemoryOveload(ref datasets))
                {
                    //Dump data to database
                    this.ormProxy.dataset.AddRange(datasets);
                    this.ormProxy.SaveChanges();
                    //free dynamic memory
                    datasets.Clear();
                }
            }

            //import remaining data into database
            this.ormProxy.dataset.AddRange(datasets);

            //Update the current scenario status
            var scen = getCurrentScenario();
            //Set the status to downloaded
            scen.status = (int)scenariostatus.Importted;
            this.ormProxy.scenario.Update(scen);

            this.ormProxy.SaveChanges();

            this._session.SetString(Keys._MSG, "Data Import Successfully Done!");

            return RedirectToAction(nameof(Index));
        }

        //Hook method for scenrio cheking
        public override bool needScenario()
        {
            return true;
        }

        //Hook method for authentication cheking 
        public override bool needAuthentication()
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
                    _pageTitle = "Data Import";
                    break;
                case "ImportData":
                    _pageTitle = "Import";
                    break;
                case "LoadRemoteFileList":
                    _pageTitle = "Load Remote Data";
                    break;
                default:
                    _pageTitle = "Data Import";
                    break;
            }

            ViewBag.Title = _pageTitle;
        }
    }
}