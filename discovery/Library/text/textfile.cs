using discovery.Library.file;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace discovery.Library.text
{
    public class textfile : filedata
    {
        protected string[] filecontent;
        public textfile(string adres)
        {
            this.filename = adres;
        }
        public override void NormalizeDataSet()
        {
            //Identify Author
            this.data.author = this.filecontent[1].Substring(this.filecontent[1].IndexOf("(") + 1, this.filecontent[1].Length - (this.filecontent[1].IndexOf("(") + 2));
            //Identify Subject
            this.data.subject = this.filecontent[3].Substring(this.filecontent[3].IndexOf("Subject:") + ("Subject").Length + 1).Trim();
            //Indetify Date
            this.data.date = this.filecontent[2].Substring(this.filecontent[2].IndexOf("Date:") + ("Date").Length + 1).Trim();

            //Remove document header and concate the remaining content into the body part of data
            //this.filecontent = this.filecontent.Where((source, index) => index > 6).ToArray();
            this.data.body = String.Join("\r\n", this.filecontent.Where((source, index) => index > 6).ToArray());
            
            
        }

        //Template method implementation for zip file
        public override void readfile()
        {
            this.filecontent = textfile.ReadfileLines(this.filename);
        }

        protected static string[] ReadfileLines(string address)
        {
            try
            {
                return System.IO.File.ReadAllLines(@address);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void prepareforread()
        {
            //Hook method and do nothing for text file reading
        }
    }
}
