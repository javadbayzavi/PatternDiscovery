using discovery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace discovery.Library.analyzer
{
    public abstract class Analyzer : IAnalyzer
    {
        protected ISubmitter _submitterEngine;
        protected object _pattern;
        protected IQueryable<result> results;

        //This property hold the strategist of the analyzer
        protected IAnalyzerExecutor _executorEngine;

        //Dependency Injected to let Analayzer submit result into datawarehoue
        public Analyzer(ISubmitter submitter)
        {
            //set the defaul result submitter
            this._submitterEngine = submitter;

            //set the default executor
            this._executorEngine = new DefaultExecutor(this);
        }

        //Template method to analyze in text content for input pattern using defaul executer
        public void Analyze(object pattern)
        {
            this._pattern = pattern;

            this._executorEngine.Execute();

            //6.Finilize the Analysis
            this.SubmitResult();
        }

        //Template method to analyze in text content for input pattern using customized executor
        public void Analyze(object pattern, IAnalyzerExecutor executor)
        {
            this._pattern = pattern;

            //Change the execution engine
            this._executorEngine = executor;

            //Run the execution engine
            this._executorEngine.Execute();
           
            this.SubmitResult();
        }

        //Stategy pattern for various type form of result submission into data set
        public void SubmitResult(ISubmitter submitter)
        {
            this._submitterEngine = submitter;
            this.SubmitResult();
        }


        public abstract void Tokenize();
        public abstract void SubmitResult();
        public abstract void Clean();
        public abstract void Filter();
        public abstract void Stemming();
        public abstract void Transform();
        public abstract void Lemmatize();
    }
}
