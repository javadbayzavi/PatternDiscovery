using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static discovery.Models.patternsviewmodel;

namespace discovery.Models
{
    [Serializable]
    //This model hold the name of patterns which are applied to minining function in order to find any result
    public class resultviewmodel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        
        [Required]
        public string pattern { get; set; }

        public string subject { get; set; }
        public string category { get; set; }

        public int patternid { get; set; }
        public int datasetid { get; set; }

        public int count { get; set; }
    }
}
