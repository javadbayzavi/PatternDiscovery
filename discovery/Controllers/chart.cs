using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using discovery.Library.Core;
using discovery.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace discovery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class chart : ControllerBase
    {
        private discoveryContext _dbproxy;
        public discoveryContext ormProxy
        {
            get
            {
                return _dbproxy;
            }
        }
        private int _currentscenario;
        private int _currentuser;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected ISession _session => _httpContextAccessor.HttpContext.Session;

        protected int currentScenario
        {
            get
            {
                var crnttScenario = (this._session.GetString(Keys._CURRENTSCENARIO) == null || this._session.GetString(Keys._CURRENTSCENARIO) == "");
                if (crnttScenario)
                    _currentscenario = 0;
                else
                    _currentscenario = Convert.ToInt32(this._session.GetString(Keys._CURRENTSCENARIO));

                return _currentscenario;
            }
            set
            {
                this._currentscenario = value;
                this._session.SetString(Keys._CURRENTSCENARIO, value.ToString());
            }
        }
        protected int currentUser
        {
            get
            {
                var crntUser = (this._session.GetString(Keys._CURRENTUSER) == null || this._session.GetString(Keys._CURRENTUSER) == "");
                if (crntUser)
                    _currentuser = 0;
                else
                    _currentuser = Convert.ToInt32(this._session.GetString(Keys._CURRENTUSER));

                return _currentuser;
            }
            set
            {
                this._currentuser = value;
                this._session.SetString(Keys._CURRENTUSER, value.ToString());
            }
        }
        public chart(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            this._dbproxy = new discoveryContext();
        }


        [HttpGet("getPatternRanking")]
        public object getPatternRanking()
        {
            var res = this.ormProxy.result
                .Where(item => item.scenarioid == this.currentScenario)
                .Include(a => a.pattern).ToList()
                .GroupBy(a => a.patternid)
                .Select(outputItem => new patternrankingviewmodel()
                { 
                    count = outputItem.Count(),
                    pattern = outputItem.FirstOrDefault().pattern.title
                }).ToList();

            return res;
        }

        [HttpGet("getCategoryRanking")]
        public object getCategoryRanking()
        {
            var res = this.ormProxy.result
                .Where(item => item.scenarioid == this.currentScenario)
                .Include(a => a.pattern).ToList()
                .GroupBy(a => a.pattern.category)
                .Select(a => new categoryrankingviewmodel()
                {
                    category = ((patternsviewmodel.categories)a.First().pattern.category).ToString(),
                    count = a.Count()
                });

            return res;
        }

        [HttpGet("getAuthorInterests")]
        public object getAuthorInterests()
        {
            var res = this.ormProxy.result
                .Where(item => item.scenarioid == this.currentScenario)
                .Include(a => a.datasetItem).ToList()
                .GroupBy(a => a.datasetItem.author)
                .Select(a => new authorrankingviewmodel()
                {
                    author = a.First().datasetItem.author,
                    count = a.Count()
                });
            return null;
        }

        [HttpGet("getPatternRankinCategory")]
        public object getPatternRankinCategory(int categoryId)
        {
            return null;
        }

        [HttpGet("getYearlyTimeline")]
        public object getYearlyTimeline()
        {
            //Show the usage of pattern with the caluse of grouping by year
            return null;
        }

        //// GET: api/<chart>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<chart>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}


    }
}
