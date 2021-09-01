using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace discovery.Models
{
    public class categoryItem
    {
        public int ID { get; set; }
        public string category { get; set; }

        [ForeignKey("ownerID")]
        public IdentityUser owner { get; set; }
        public string ownerID { get; set; }
    }
}
