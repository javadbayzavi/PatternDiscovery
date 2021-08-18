﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace discovery.Models
{
    public class discoveryContext : DbContext
    {
        //public discoveryContext(DbContextOptions options) : base(options)
        //{ 
        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=patternproject;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                //optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\PatternDiscovery\\discovery\\db\\patternproject.mdf;Integrated Security=True");
            }
        }


        public DbSet<patterns> patterns { get; set; }
        public DbSet<result> result { get; set; }
        public DbSet<dataset> dataset { get; set; }
        public DbSet<scenario> scenario { get; set; }
    }


}
