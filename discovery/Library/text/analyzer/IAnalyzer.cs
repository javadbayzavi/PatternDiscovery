using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace discovery.Library.text.analyzer
{
    public interface IAnalyzer
    {
        void Tokenize();
        void Normlize();
    }
}
