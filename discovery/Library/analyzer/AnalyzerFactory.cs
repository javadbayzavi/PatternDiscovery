using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace discovery.Library.analyzer
{
    public class AnalyzerFactory
    {
        private static ConventionalAnalyzer GetConventionalAnalyzer(ISubmitter submitter, int scenario)
        {
            return new ConventionalAnalyzer(submitter,scenario);
        }
        private static AiAnalyzer GetAiAnalyzer(ISubmitter submitter, int scenario)
        {
            return new AiAnalyzer(submitter,scenario);
        }

        public static Analyzer createAnalyzer(string type,DbContext context, int scenario)
        {
            if (type == "1")
                return AnalyzerFactory.GetConventionalAnalyzer(new ConventionalSubmitter(context),scenario);
            else
                return AnalyzerFactory.GetAiAnalyzer(new AiSubmitter(context),scenario);
        }
    }
}
