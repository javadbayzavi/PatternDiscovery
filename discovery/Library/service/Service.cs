using discovery.Library.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace discovery.Library.service
{
    public abstract class Service : rawdocument, IProvider
    {
        public abstract void Connect();
        public abstract void LoadRemoteData();
    }
}
