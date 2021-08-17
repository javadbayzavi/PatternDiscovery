using discovery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace discovery.Library.text.analyzer
{
    public abstract class Analyzer : IAnalyzer
    {
        private ISubmitter _submitter;
        protected string _pattern;
        protected IQueryable<result> results;

        //Dependency Injected to let Analayzer submit result into datawarehoue
        public Analyzer(ISubmitter submitter)
        {
            this._submitter = submitter;
        }
        //Template method to analyze in text content for input pattern
        public void Analyze(string pattern)
        {
            this._pattern = pattern;
            this.Tokenize();
            this.SubmitResult(this._submitter);
        }
        public abstract void Tokenize();
        public abstract void SubmitResult(ISubmitter submitter);
    }
}
