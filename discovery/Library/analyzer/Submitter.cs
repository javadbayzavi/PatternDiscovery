using discovery.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace discovery.Library.analyzer
{
    public abstract class Submitter : ISubmitter
    {
        //DbContext ISubmitter.context { get; set; }
        private DbContext context { get; set; }

        //public DbContext context { get; set; }
        public Submitter(DbContext context)
        {
            this.context = context;
        }

        public DbContext GetContext()
        {
            return this.context;
        }

        public void SetContext(DbContext context)
        {
            this.context = context;
        }
        public abstract void Submit();
    }
}
