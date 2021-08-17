using discovery.Library.file;
using discovery.Library.text;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace discovery.Library.zip
{
    public class zipfile : textfile, IUnzipper
    {
        public zipfile(string adres):base(adres)
        {
            this.filename = adres;
        }

        public override void prepareforread()
        {
            this.filename = this.unzip();
        }

        public string unzip()
        {
            var fileToDecompress = new FileInfo(this.filename);
            string unzippedFile = "";
            using (FileStream originalFileStream = fileToDecompress.OpenRead())
            {
                string currentFileName = fileToDecompress.FullName;
                string newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);

                using (FileStream decompressedFileStream = File.Create(newFileName))
                {
                    using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(decompressedFileStream);
                        unzippedFile = newFileName;
                    }
                }
            }

            //Delete the zip file
            if(unzippedFile.Length > 0)
                File.Delete(this.filename);

            return unzippedFile;
        }
    }
}


