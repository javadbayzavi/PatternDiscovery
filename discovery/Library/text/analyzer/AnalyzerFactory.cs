using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace discovery.Library.text.analyzer
{
    public class AnalyzerFactory
    {
        public static ConventionalAnalyzer GetConventionalAnalyzer(ISubmitter submitter)
        {
            return new ConventionalAnalyzer(submitter);
        }
    }
}
