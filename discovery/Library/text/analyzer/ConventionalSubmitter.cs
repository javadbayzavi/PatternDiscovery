using discovery.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace discovery.Library.text.analyzer
{
    public class ConventionalSubmitter : ISubmitter
    {
        public discoveryContext context { get; set; }
        public ConventionalSubmitter(discoveryContext dbContext)
        {
            this.context = dbContext;
        }
        public void Submit()
        {
            this.context.SaveChanges();
        }
    }
}
