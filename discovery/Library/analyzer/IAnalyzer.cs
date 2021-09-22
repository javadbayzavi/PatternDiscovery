using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace discovery.Library.analyzer
{
    public interface IAnalyzer : ITokenizer, ICleaner, IFilterable, IStemable, ILemmatizable, ITransformable
    {
        void Analyze(object pattern);
        
        //Customized Analyze
        void Analyze(object pattern, IAnalyzerExecutor executor);
        
        //Customized Submission
        void SubmitResult(ISubmitter submitter);
        void SubmitResult();
        //Task AnalyzeAsnyc(object pattern);
    }
}
