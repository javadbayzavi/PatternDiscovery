using discovery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace discovery.Library.Core
{
    public abstract class rawdocument : IReadable
    {
        protected dataset data;
        public void Read(ref dataset datast)
        {
                this.data = datast;
                this.ReadDocument();
                this.NormalizeDataSet();
                this.signAsImported();
        }
        public abstract void NormalizeDataSet();
        public abstract void ReadDocument();
        public abstract void signAsImported();
        public abstract bool isImported();
    }
}
