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
    public class result
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [ForeignKey("patternid")]
        public patterns pattern { get; set; }
        [Required]
        public int patternid { get; set; }

        //Date of the publish
        [ForeignKey("datasetitemid")]
        public dataset datasetItem { get; set; }
        public int datasetitemid { get; set; }

        //Which part of the mail document contains the patterns (Title, Body)
        public int partofdocument { get; set; }

        public int scenarioid { get; set; }

        [ForeignKey("scenarioid")]
        public scenario scenario { get; set; }
    }
}
