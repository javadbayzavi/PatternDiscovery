using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace discovery.Library.text.analyzer
{
    public class ConventionalExecutor : AnalyzerExecutor
    {
        public ConventionalExecutor(IAnalyzer context):base(context)
        {
        }
        
        //This is a template method that simulate the specialized running behavior of a conventional text analyzer
        public override void Execute()
        {
            //Tokenization the imported pattern
            this._context.Tokenize();

        }
    }
}
