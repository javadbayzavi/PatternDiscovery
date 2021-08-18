using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace discovery.Models
{
    [Serializable]
    //This model hold the information on representation matter
    public class datasetviewmodel
    {
        public int ID { get; set; }
        
        [Required]
        public string subject { get; set; }

        //Date of the publish
        public string date { get; set; }

        //Author of the document
        public string author { get; set; }
        
        
    }
}
