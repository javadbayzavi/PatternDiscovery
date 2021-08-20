using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace discovery.Models
{
    public class categoryrankingviewmodel
    {
        //name of the pattern
        public string category { get; set; }
        
        //number of finding in result
        public int count { get; set; }
    }
}
