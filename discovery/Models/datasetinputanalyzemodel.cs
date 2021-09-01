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
        public int ID { get; set; }
        public string body { get; set; }
        public string lemmatizedbody { get; set; }
    }
}
