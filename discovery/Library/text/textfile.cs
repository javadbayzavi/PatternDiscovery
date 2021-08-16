using discovery.Library.file;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace discovery.Library.text
{
    public class textfile : filedata
    {
        string[] filecontent;
        public textfile(string adres)
        {
            this.filename = adres;
        }
        public override void NormalizeDataSet()
        {
            //Identify Author
            this.data.author = this.filecontent[1].Substring(this.filecontent[1].IndexOf("(") + 1, this.filecontent[1].Length - (this.filecontent[1].IndexOf("(") + 2));
            //Identify Subject
            this.data.subject = this.filecontent[3].Substring(this.filecontent[3].IndexOf("Subject:")).Trim();
            //Indetify Date
            this.data.date = this.filecontent[2].Substring(this.filecontent[2].IndexOf("Date:")).Trim();
            for (int index = 7; index < filecontent.Length; index++)
                this.data.body += filecontent[index] + "/r/n";
            
        }

        //Template method implementation for zip file
        public override void readfile()
        {
            this.filecontent = textfile.ReadfileLines(this.filename);
        }

        public static string[] ReadfileLines(string address)
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
