using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace discovery.Library.service
{
    public interface IProvider
    {
        void Connect();
        void LoadRemoteData();
    }
}
