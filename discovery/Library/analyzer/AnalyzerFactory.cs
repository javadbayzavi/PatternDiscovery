using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace discovery.Library.analyzer
{
    public class AnalyzerFactory
    {
        private static ConventionalAnalyzer GetConventionalAnalyzer(ISubmitter submitter)
        {
            return new ConventionalAnalyzer(submitter);
        }
        private static AiAnalyzer GetAiAnalyzer(ISubmitter submitter)
        {
            return new AiAnalyzer(submitter);
        }

        public static Analyzer createAnalyzer(string type,DbContext context)
        {
            if (type == "1")
                return AnalyzerFactory.GetConventionalAnalyzer(new ConventionalSubmitter(context));
            else
                return AnalyzerFactory.GetAiAnalyzer(new AiSubmitter(context));
        }
    }
}
