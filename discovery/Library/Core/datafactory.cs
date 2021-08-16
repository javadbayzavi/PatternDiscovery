using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace discovery.Library.Core
{
    public abstract class datafactory
    {
        //Factory method for raw data document
        public abstract IReadable createData();
    }
}
