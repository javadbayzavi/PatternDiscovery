using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace discovery.Models
{
    //This model hold the information about mail document
    public class dataset
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        
        [Required]
        public string subject { get; set; }

        //Date of the publish
        public string date { get; set; }

        //Which part of the mail document contains the patterns
        public int author { get; set; }
        
        //Title of the mail document
        public string body { get; set; }
        
    }
}
