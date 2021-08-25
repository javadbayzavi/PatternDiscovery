using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace discovery.Library.text.analyzer
{
    public abstract class AnalyzerExecutor :IAnalyzerExecutor
    {
        protected IAnalyzer _context;
        public AnalyzerExecutor(IAnalyzer context)
        {
            this._context = context;
        }
        public abstract void Execute();
    }
}
