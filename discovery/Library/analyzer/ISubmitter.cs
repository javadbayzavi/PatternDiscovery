using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace discovery.Library.analyzer
{
    public interface ISubmitter
    {
        void Submit();
        DbContext GetContext();
        void SetContext(DbContext context);
    }
}
