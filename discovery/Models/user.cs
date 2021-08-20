using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace discovery.Models
{
    public class user
    {
        public int ID { get; set; }
        public string username { get; set; }
        public string  password { get; set; }
        public string name { get; set; }
        public string family { get; set; }
        public string email { get; set; }
        public DateTime createddate { get; set; }
        public DateTime dateTime { get; set; }
    }
}
