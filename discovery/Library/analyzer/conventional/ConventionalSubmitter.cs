using discovery.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace discovery.Library.analyzer
{
    public class ConventionalSubmitter : Submitter
    {
        public ConventionalSubmitter(DbContext dbContext):base(dbContext)
        {
            this.SetContext(dbContext);
        }
        public override void Submit()
        {
            this.GetContext().SaveChanges();
        }
    }
}
