using discovery.Library.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace discovery.Library.file
{
    public abstract class filedata : rawdocument
    {
        public string filename { get; set; }

        //Templae method for reading file content
        public override void ReadDocument()
        {
            this.prepareforread();
            this.readfile();
        }
        //Do all necessary operations before reading a file
        public abstract void prepareforread();

        //Read file function
        public abstract void readfile();
    }
}
