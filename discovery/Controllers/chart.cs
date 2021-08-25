using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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


        //For pie chart {Done}
        [HttpGet("getPatternRanking")]
        public object getPatternRanking()
        {
            var res = this.ormProxy.result
                .Where(item => item.scenarioid == this.currentScenario)
                .Include(a => a.pattern).ToList()
                .GroupBy(a => a.patternid)
                .Select(outputItem => new patternrankingviewmodel()
                { 
                    count = outputItem.Sum(a => a.count),
                    pattern = outputItem.FirstOrDefault().pattern.title
                }).ToList();

            return res;
        }

        //For pie chart {Done}
        [HttpGet("getCategoryRanking")]
        public object getCategoryRanking()
        {
            var res = this.ormProxy.result
                .Where(item => item.scenarioid == this.currentScenario)
                .Include(a => a.pattern).ThenInclude(a => a.category).ToList()
                .GroupBy(a => a.pattern.categoryId)
                .Select(a => new categoryrankingviewmodel()
                {
                    category = a.FirstOrDefault().pattern.category.category,
                    count = a.Sum(b => b.count)
                });

            return res.ToList();
        }

        //For bar chart{Done}
        [HttpGet("getAuthorInterested")]
        public object getAuthorInterested()
        {
            var res = this.ormProxy.result
                .Where(item => item.scenarioid == this.currentScenario)
                .Include(a => a.datasetItem).ToList()
                .GroupBy(a => a.datasetItem.author)
                .Select(a => new authorrankingviewmodel()
                {
                    author = a.First().datasetItem.author,
                    count = a.Sum(b => b.count)
                }).ToList();
            return res;
        }

        //For bar chart{Done}
        [HttpGet("getPatternRankinCategory/{categoryId}")]
        public object getPatternRankinCategory(int categoryId)
        {
            var res = this.ormProxy.result
                .Where(item => item.scenarioid == this.currentScenario && item.pattern.categoryId == categoryId)
                .Include(a => a.pattern).ToList()
                .GroupBy(a => a.patternid)
                .Select(outputItem => new patternrankingviewmodel()
                {
                    count = outputItem.Sum(a => a.count),
                    pattern = outputItem.FirstOrDefault().pattern.title
                }).ToList();

            return res;
        }

        //For bar chart{Dome}
        [HttpGet("getYearlyTimeline")]
        public object getYearlyTimeline()
        {
            var regexpr = new Regex("\\d{4}");
            var res = this.ormProxy.result
                .Where(item => item.scenarioid == this.currentScenario)
                .Include(a => a.datasetItem).ToList()
                .Select(a => new yearrankingviewmodel()
                {
                    year = (regexpr.Match(a.datasetItem.date).Success) ? regexpr.Match(a.datasetItem.date).Value : "2010",
                    count = a.count
                })
                .GroupBy(a => a.year)
                .Select(outputItem => new yearrankingviewmodel()
                {
                    count = outputItem.Sum(a => a.count),
                    year = outputItem.FirstOrDefault().year
                })
                .OrderByDescending(a => a.year)
                .ToList();
            return res;
        }

        //For bar chart{Done}
        [HttpGet("getMonthlyTimeline")]
        public object getMonthlyTimeline()
        {
            var regexpr = new Regex(discovery.Library.Core.Keys._MONTHREGEXPRESSION);
            var res = this.ormProxy.result
                .Where(item => item.scenarioid == this.currentScenario)
                .Include(a => a.datasetItem).ToList()
                .Select(a => new yearrankingviewmodel()
                {
                    year = (regexpr.Match(a.datasetItem.date).Success) ? regexpr.Match(a.datasetItem.date).Value : "Jan",
                    count = a.count
                })
                .GroupBy(a => a.year)
                .Select(outputItem => new yearrankingviewmodel()
                {
                    count = outputItem.Sum(a => a.count),
                    year = outputItem.FirstOrDefault().year
                })
                .OrderByDescending(a => a.year)
                .ToList();
            return res;
        }

        //For list
        [HttpGet("notFoundCategories")]
        public object notFoundCategories()
        {
            //var res = this.ormProxy.result
            //    .Where(item => item.scenarioid == this.currentScenario)
            //    .Include(a => a.pattern)
            //    .ToList()
            //    .GroupBy(a => a.pattern.category);

            return null;
        }

        //For list
        [HttpGet("notFoundPatterns")]
        public object notFoundPatterns()
        {
            return this.ormProxy.patterns.Where(pattern =>
                this.ormProxy.result
                .Any(item => item.scenarioid == this.currentScenario && item.patternid != pattern.ID))
                .Select(a => new patternsviewmodel 
                { 
                    ID = a.ID,
                    title = a.title
                })
                .ToList();
                
        }

        //For pie chart {Done}
        [HttpGet("percentageOfWholePatterns")]
        public object percentageOfWholePatterns()
        {
            var fountnumbers = (float)this.ormProxy.result
                .Where(item =>
                item.scenarioid == this.currentScenario)
                .ToList()
                .GroupBy(a => a.patternid).Count(); 
            var pattenrscnt = (float)this.ormProxy.patterns.Count();

            float result = (fountnumbers / pattenrscnt);
            //Define an anonymous object
            return new
            {
                //Define in percent
                covered = result * 100,
                notcovered = 100 - (result * 100)
            };
        }

        //For pie chart{Done}
        [HttpGet("percentageOfPatternsInCategory/{id}")]
        public object percentageOfPatternsInCategory(int id)
        {
            var result = (float)this.ormProxy.result
                .Where(item =>
                item.scenarioid == this.currentScenario && item.pattern.categoryId == id).
                ToList()
                //In order to remove duplicate patterns in result
                .GroupBy(a => a.patternid)
                .Count()
                / (float)this.ormProxy.patterns.Count(item =>
                item.categoryId == id);
            
            //Define an anonymous object
            return new
            {
                //Define in percent
                covered = result * 100,
                notcovered = 100 - (result * 100)
            };
        }

        //For bar chart
        [HttpGet("subjectWithMostPattern")]
        public object subjectWithMostPattern(int top = 10)
        {
            var res = this.ormProxy.result
                .Where(item => item.scenarioid == this.currentScenario)
                .Include(a => a.datasetItem).ToList()
                .GroupBy(a => a.datasetitemid)
                //Create Anonymous object as output
                .Select(outputItem => new
                {
                    count = outputItem.Sum(a => a.count),
                    subject = outputItem.FirstOrDefault().datasetItem.subject
                }).OrderByDescending(a => a.count)
                .Take(top)
                .ToList();

            return res;
        }

        //For bar chart
        [HttpGet("subjectWithMostDiversePattern/{top}")]
        public object subjectWithMostDiversePattern(int top = 10)
        {
            var res = this.ormProxy.result
                .Where(item => item.scenarioid == this.currentScenario)
                .Include(a => a.datasetItem).ToList()
                .GroupBy(a => a.datasetitemid)
                //Create Anonymous object as output
                .Select(outputItem => new 
                {
                    //Number of pattern found in each dataset
                    count = outputItem.Count(),
                    subject = outputItem.FirstOrDefault().datasetItem.subject
                }).OrderByDescending(a => a.count)
                .Take(top)
                .ToList();

            return res;
        }
    }
}
