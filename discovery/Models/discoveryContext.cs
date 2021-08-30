using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace discovery.Models
{
    public class discoveryContext : DbContext
    {
        public discoveryContext(DbContextOptions options) : base(options)
        {
        }

        public discoveryContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Data Source=.;Initial Catalog=patternproject;Persist Security Info=True;User ID=discovery;Password=1234567890;Encrypt=False;ApplicationIntent=ReadWrite;",
                    opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(30).TotalSeconds)
                    );
            }
        }

        public DbSet<patterns> patterns { get; set; }
        public DbSet<result> result { get; set; }
        public DbSet<dataset> dataset { get; set; }
        public DbSet<scenario> scenario { get; set; }
        public DbSet<categoryItem> categories { get; set; }
    }


}
