using discovery.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace discovery.Library.analyzer
{
    public class AiSubmitter : Submitter
    {
        public AiSubmitter(DbContext dbContext) : base(dbContext)
        {
            this.SetContext(dbContext);
        }
        public override void Submit()
        {
            //Do something before saving data into data store
            this.GetContext().SaveChanges();
        }
    }
}
