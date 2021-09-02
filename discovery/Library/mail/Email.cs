using discovery.Library.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace discovery.Library.mail
{
    public abstract class Email : rawdocument
    {
        public abstract override void NormalizeDataSet();

        public abstract override void ReadDocument();

    }
}
