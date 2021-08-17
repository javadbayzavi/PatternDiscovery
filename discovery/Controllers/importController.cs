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
        // GET: import
        public ActionResult Index()
        {
            string url = this.HttpContext.Session.GetString(Keys._REMOTEURL);
            if(System.IO.Directory.GetFiles(Keys._TEMPDIRECTORY).Length > 0 && url != null)
            {
                //latest versions of raw file has downloaded

            }
            {
                //donwloaded files must be replaced with new version from the remote url
            }
            return View();
        }

        // post: import/FetchRemoteFiles
        [HttpPost]
        public ActionResult FetchRemoteFiles()
        {
            string url = this.HttpContext.Session.GetString(Keys._REMOTEURL);
            var downlaoded = Fileoperations.downloadFiles(ref url);
            if (downlaoded)
            {
                //After successfully downloading all remote files session address sets to null that shows the operation can run from the start again
                this.HttpContext.Session.SetString(Keys._REMOTEURL, "");
                return RedirectToAction("Index");
            }
            else
                return RedirectToAction("LoadRemoteFileList");
        }
        public ActionResult LoadRemoteFileList()
        {
            return View();
        }
        //POST: import/LoadRemoteFileList
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoadRemoteFileList(string url)
        {
            try
            {
                var files = Fileoperations.getFileList(ref url).Select(a =>
                    new filelistview()
                        {
                            filename = a.filename,
                            fileurl = a.fileurl
                        }
                    ).ToList();
                this.HttpContext.Session.SetString(Keys._REMOTEURL,url);
                return View(files);
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: import/ImportData/
        public ActionResult ImportData()
        {
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
            this.ormProxy.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}