using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace discovery.Models
{
    [Serializable]
    public class scenario
    {
        public int ID { get; set; }
        public string name { get; set; }
        public Guid sversion { get; set; }
        public string datasource { get; set; }
        public DateTime createddate { get; set; }

        //Define the datsource type of analysis (FileUrl = 1, Email = 2, ServiceUrl = 3)
        public int sourcetype { get; set; }

        //Show the status of current scenario (Created =1, Data Downloaded = 2, Data Import = 3, Data Analyzed = 4)
        public int status { get; set; }

        //Analyzing method (Conventional = 1, AI Based = 2)
        public int method { get; set; }
    }
}
