using discovery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace discovery.Library.Core
{
    public class BaseController : Controller
    {
        protected int scenarioidentity 
        { 
            get; 
            set; 
        }
        //public BaseController(discoveryContext db)
        public BaseController()
        {
            this._dbproxy = new discoveryContext();
        }

        private discoveryContext _dbproxy;
        public discoveryContext ormProxy
        {
            get
            {
                return _dbproxy;
            }
        }
    }
}
