using discovery.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace discovery.Library.Core
{
    public abstract class BaseController : Controller, IScenarioable
    {
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

        public BaseController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            this._dbproxy = new discoveryContext();
            this._dbemergencyproxy = new emergncyDbContext();
        }

        private discoveryContext _dbproxy;
        public discoveryContext ormProxy
        {
            get
            {
                return _dbproxy;
            }
        }

        private emergncyDbContext _dbemergencyproxy;
        public emergncyDbContext ormEmergencyProxy
        {
            get
            {
                return _dbemergencyproxy;
            }
        }

        protected scenario getCurrentScenario()
        {
            var item = this.ormProxy.scenario.Find(this.currentScenario);
            return item;
        }

        public abstract bool needScenario();


        public abstract void setPageTitle(string actionRequester);

    }
}
