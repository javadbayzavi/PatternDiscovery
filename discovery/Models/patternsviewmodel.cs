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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        
        [Required]
        public string text { get; set; }

        public string category { get; set; }

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

