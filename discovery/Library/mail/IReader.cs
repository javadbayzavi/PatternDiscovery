using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace discovery.Library.mail
{
    public interface IReader
    {
        //Read the raw mail content
        void Read();
        
        //Save the read content into a data source
        void Save();
        
        //Load the content from a data source
        void Load();
    }
}
