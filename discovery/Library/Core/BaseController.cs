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
    public abstract class BaseController : Controller, IAuthenticable, IScenarioable
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

        //Template Method for cheking user and scenario integrity
        protected void integritycheking()
        {
            //    //if(needAuthentication())
            //    //{
            //    //In case of need for authorized access to page and the user isn't authenticated, user be redirected to login page
            //    //    if(this.currentUser < 1)
            //    //    {
            //    //        //return RedirectToActionPermanent("Login", "user");
            //    //    }
            //    //}
            //    if (needScenario())
            //    {
            //        //In case of need for scenario selection and the user did not selected, user be redirected to scenario page
            //        if (this.currentScenario < 1)
            //            //RedirectToActionPermanent("Index", "scenario");
            //            return RedirectToAction("Index","scenario");

            //    }

            //    if (this.ControllerContext.RouteData == null)
            //        return RedirectToAction(nameof(Index));
            //    else
            //    {
            //        string actionName = (this.ControllerContext.RouteData == null || this.ControllerContext.RouteData.Values["action"] == null)? "Home" : this.ControllerContext.RouteData.Values["action"].ToString();

            //        return RedirectToAction(actionName);
            //    }

        }

        public BaseController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            this._dbproxy = new discoveryContext();
            this.integritycheking();
        }
        private discoveryContext _dbproxy;
        public discoveryContext ormProxy
        {
            get
            {
                return _dbproxy;
            }
        }
        protected scenario getCurrentScenario()
        {
            var item = this.ormProxy.scenario.Find(this.currentScenario);
            return item;
        }

        public abstract bool needScenario();


        public abstract bool needAuthentication();

        public abstract void setPageTitle(string actionRequester);

    }
}
