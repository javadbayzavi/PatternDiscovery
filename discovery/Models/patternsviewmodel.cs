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
    [Serializable]
    //This model hold the name of patterns which are applied to minining function in order to find any result
    public class patternsviewmodel
    {
        public int ID { get; set; }
        
        [Required]
        public string title { get; set; }
        
        public int categoryId { get; set; }
        public string categoryTitle { get; set; }


    }
}

