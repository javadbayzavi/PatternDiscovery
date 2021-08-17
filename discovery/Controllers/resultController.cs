using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using discovery.Library.Core;
using discovery.Models;
using Microsoft.AspNetCore.Mvc;
using static discovery.Models.patternsviewmodel;

namespace discovery.Controllers
{
    //This controller handle the presentation of text analysis results in various formats
    public class resultController : BaseController
    {
        public IActionResult Index()
        {
            var results = this.ormProxy.result.Select(item => new resultviewmodel
            {
                subject = item.datasetItem.subject,
                ID = item.ID,
                datasetid = item.datasetitemid,
                category = (categories)item.pattern.category,
                patternid = item.patternid
            });
            return View(results);
        }
    }
}