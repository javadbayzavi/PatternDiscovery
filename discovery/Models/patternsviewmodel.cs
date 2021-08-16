using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using discovery.Library.Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace discovery.Models
{
    //This model hold the name of patterns which are applied to minining function in order to find any result
    public class patternsviewmodel
    {
        private enum categories
        {
            Structural = 1,
            Behavioral,
            Creational,
            Concurrency
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        
        [Required]
        public string title { get; set; }
        
        private categories _catnumber;
        public string category { 
            get
            {
                return this._catnumber.ToString();
            }
            set
            {
                this._catnumber =(categories)Convert.ToInt32(value);
            }
        }

        //Hook method to generated default list of categories.
        public static List<SelectListItem> getCategories()
        {
            var categories = new List<SelectListItem>(4) 
            {
                new SelectListItem(){Text =Keys._STRUCTURAL , Value = "1" },
                new SelectListItem(){Text = Keys._BEHAVIORAL , Value = "2" },
                new SelectListItem(){Text = Keys._CREATIONAL , Value = "3" },
                new SelectListItem(){Text = Keys._CONCURRENCY , Value = "4" }
            };

            return categories;
        }
    }
}

