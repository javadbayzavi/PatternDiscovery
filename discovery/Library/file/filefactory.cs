using discovery.Library.Core;
using discovery.Library.text;
using discovery.Library.zip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace discovery.Library.file
{
    public class filefactory : datafactory
    {
        private string _fileaddress;
        public filefactory(string address)
        {
            this._fileaddress = address;
        }
        public override IReadable createLocalData()
        {
            if (this._fileaddress.Length > 0)
            {
                if (this._fileaddress.Contains(".gz"))
                    return new zipfile(this._fileaddress);
                else 
                    return new textfile(this._fileaddress);
            }
            else
                return new textfile(this._fileaddress);
        }
    }
}
