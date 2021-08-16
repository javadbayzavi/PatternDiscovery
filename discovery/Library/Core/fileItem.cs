using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace discovery.Library.Core
{
    //This class hold the representation of remote file trackd by application
    public class fileItem
    {
        //Name of the remote file
        public string filename { get; set; }

        //Remote adrress of file which is used for download
        public string fileurl { get; set; }
    }
}
