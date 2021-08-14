using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace discovery.Models
{
    //This model hold the name of patterns which are applied to minining function in order to find any result
    public class dataset
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        
        [Required]
        public string pattern { get; set; }

        //Date of the publish
        public string date { get; set; }

        //Which part of the mail document contains the patterns
        public int partofdocument { get; set; }
        
        //Title of the mail document
        public string title { get; set; }
        
        //Authors of the mail document
        public string author { get; set; }

        //Subject of the mail document
        public string subject { get; set; }
        
    }
}
