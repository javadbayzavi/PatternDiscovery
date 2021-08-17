using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace discovery.Library.text.analyzer
{
    //Concrete analyzer which is using database engine to do anaylzing process
    public class ConventionalAnalyzer : Analyzer
    {
        private ConventionalSubmitter _submitter;
        public ConventionalAnalyzer(ISubmitter submitter) : base(submitter)
        {
            this._submitter = (ConventionalSubmitter)submitter;
        }
        public override void SubmitResult(ISubmitter submitter)
        {
            //load result into the submitter context for adding to database
            this._submitter.context.result.AddRange(this.results);

            submitter.Submit();
        }

        public override void Tokenize()
        {
            int id = System.Convert.ToInt32(this._pattern);

            var pattern = this._submitter.context.patterns.First(item => item.ID == id);

            //Do Conventional text analyze on database engine
            this.results = this._submitter.context.dataset.Where(a =>
            a.body.Contains(pattern.title) == true
            ).Select(item => new Models.result 
            { 
                datasetitemid = item.ID,
                patternid = pattern.ID,
                partofdocument = 1
            });
        }
    }
}
