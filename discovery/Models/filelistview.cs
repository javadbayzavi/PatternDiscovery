using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace discovery.Models
{
    //This class hold the representation of remote file trackd by application
    public class filelistview
    {
        //Name of the remote file
        public string filename { get; set; }

        //Remote adrress of file which is used for download
        public string fileurl { get; set; }
    }
}
