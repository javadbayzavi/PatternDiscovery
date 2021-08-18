using discovery.Library.Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace discovery.Models
{
    enum scenariosourcetype
    {
        File = 1,
        Email,
        Service
    }
    enum scenariostatus
    {
        Created = 1,
        Downloaded,
        Importted,
        Analyzed
    }
    enum scenarionmethod
        {
        Conventional = 1,
        AIBased = 2
    }
    [Serializable]
    public class scenarioviewmodel
    {
        public int ID { get; set; }
        public string name { get; set; }
        public Guid sversion { get; set; }
        public string datasource { get; set; }
        public DateTime createddate { get; set; }

        //Define the datsource type of analysis (FileUrl = 1, Email = 2, ServiceUrl = 3)
        private scenariosourcetype _srctype;
        public string sourcetype {
            get
            {
                return this._srctype.ToString();
            }
            set
            {
                this._srctype = (scenariosourcetype)Convert.ToInt32(value);
            }
        }

        //Show the status of current scenario (Created =1, Data Downloaded = 2, Data Import = 3, Data Analyzed = 4)
        private scenariostatus _srcstatus;
        public string status 
        {
            get
            {
                return this._srcstatus.ToString();
            }
            set
            {
                this._srcstatus = (scenariostatus)Convert.ToInt32(value);
            }
        }

        //Analyzing method (Conventional = 1, AI Based = 2)
        private scenarionmethod _srcmethod;
        public string method 
        {
            get
            {
                return this._srcmethod.ToString();
            }
            set
            {
                this._srcmethod = (scenarionmethod)Convert.ToInt32(value);
            }
        }


        //Hook method to generated default list of scenario SourceType.
        public static List<SelectListItem> getTypes()
        {
            var categories = new List<SelectListItem>(4)
            {
                new SelectListItem(){Text =Keys._FILE , Value = "1" },
                new SelectListItem(){Text = Keys._EMAIL , Value = "2" },
                new SelectListItem(){Text = Keys._SERVICE , Value = "3" },
            };

            return categories;
        }        
        //Hook method to generated default list of scenario Status.
        public static List<SelectListItem> getStatus()
        {
            var categories = new List<SelectListItem>(4)
            {
                new SelectListItem(){Text =Keys._CREATED , Value = "1" },
                new SelectListItem(){Text = Keys._DOWNLOADED , Value = "2" },
                new SelectListItem(){Text = Keys._IMPORTTED , Value = "3" },
                new SelectListItem(){Text = Keys._ANALYZED , Value = "4" }
            };

            return categories;
        }        
        
        //Hook method to generated default list of scenario method.
        public static List<SelectListItem> getMethods()
        {
            var categories = new List<SelectListItem>(4)
            {
                new SelectListItem(){Text =Keys._CONVENTIONAL , Value = "1" },
                new SelectListItem(){Text = Keys._AIBASED , Value = "2" },
            };

            return categories;
        }
    }
}
