using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace discovery.Models
{
    public class discoveryContext : DbContext
    {
        public discoveryContext() : base()
        {

        }

        public DbSet<patterns> patterns { get; set; }
        public DbSet<dataset> datasets { get; set; }
    }
}
