using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML.Data;

namespace discovery.Models
{
    //This model hold the information about mail document
    public class datasetinputanalyzemodel
    {
        //public int ID { get; set; }
        
        ////Title of the mail document
        //[LoadColumn(0)]
        //public string subject { get; set; }

        //Body of the mail document
        public string body { get; set; }

    }
}
