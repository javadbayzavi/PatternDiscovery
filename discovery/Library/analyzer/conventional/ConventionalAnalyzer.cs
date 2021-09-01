using discovery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace discovery.Library.analyzer
{
    //Concrete analyzer which is using database engine to do anaylzing process
    public class ConventionalAnalyzer : Analyzer
    {
        public ConventionalAnalyzer(ISubmitter submitter, int scenario) : base(submitter,scenario)
        {
        }


        //Hook method for cleaning
        public override void Clean()
        {
            
        }

        //Hook method for Filtering
        public override void Filter()
        {
        }

        //Hook method for Lemmatize
        public override void Lemmatize()
        {
        }

        //Hook method for Stemming
        public override void Stemming()
        {
        }

        public override void SubmitResult()
        {
            //Set context of this analyzer to discovery DbContext
            var context = (discoveryContext)this._submitterEngine.GetContext();

            //load result into the submitter context for adding to database
            context.result.AddRange(this.results);

            this._submitterEngine.Submit();
        }

        public override void Tokenize()
        {
            int id = System.Convert.ToInt32(this._pattern);
            var context = (discoveryContext)this._submitterEngine.GetContext();

            var pattern = context.patterns.First(item => item.ID == id);

            //Do Conventional text analyze on database engine
            this.results = context.dataset.Where(a =>
            a.body.Contains(pattern.title.ToLower()) == true
            &&
            a.scenarioid == this._currentscenario
            ).Select(item => new Models.result
            {
                count = Regex.Matches(item.body, pattern.title.ToLower(),RegexOptions.IgnoreCase).Count,
                datasetitemid = item.ID,
                patternid = pattern.ID,
                partofdocument = 1,
                scenarioid = item.scenarioid
            }); ;
        }

        //Hook method for Transform
        public override void Transform()
        {
            
        }
    }
}
