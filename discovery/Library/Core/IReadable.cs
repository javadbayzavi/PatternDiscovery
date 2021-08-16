using discovery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace discovery.Library.Core
{
    public interface IReadable
    {
        //Read the raw content from document
        void Read(ref dataset datast);
    }
}
