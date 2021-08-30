﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace discovery.Library.analyzer
{
    public class DefaultExecutor : AnalyzerExecutor
    {
        public DefaultExecutor(Analyzer context):base(context)
        {
        }
        
        //This is a template method that simulate the default running behavior of a generic text analyzer
        public override void Execute()
        {

            //1.Filtering
            this._context.Filter();

            //2. Tokenization
            this._context.Tokenize();

            //3. Text Cleaning
            this._context.Clean();

            //4. Stemming
            this._context.Stemming();

            //5. Lemmitizing
            this._context.Lemmatize();

            //6. Transform the result
            this._context.Transform();
        }
    }
}