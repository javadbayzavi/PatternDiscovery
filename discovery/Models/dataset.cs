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
    [Serializable]
    public class dataset
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        
        [Required]
        //Title of the mail document
        public string subject { get; set; }

        //Date of the publish
        public string date { get; set; }

        //Author of the document
        public string author { get; set; }

        //Body of the mail document
        public string body { get; set; }
        
    }
}
