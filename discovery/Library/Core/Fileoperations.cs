using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using discovery.Library.zip;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace discovery.Library.Core
{
    //Contains all the key names are using throughth the app
    public class Fileoperations
    {
        public static List<fileItem> getFileList(ref string url)
        {
            var files = new List<fileItem>();

            string remoteUrl = (url != null && url != "") ? url : Keys._REMOTEADDRESS;
            HtmlWeb hw = new HtmlWeb();
            HtmlDocument doc = hw.Load(remoteUrl);
            foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
            {
                string linkhref = link.Attributes["href"].Value;
                string linkfile = "";
                //check whether url is relative or fixed
                if (linkhref.LastIndexOf("/") >= 0)
                {
                    //fixed address
                    linkfile = linkhref.Substring(linkhref.LastIndexOf("/"));
                }
                else
                {
                    //relative address
                    linkfile = linkhref;
                }

                //check for valid files (txt / gz / csv)
                if (linkfile.Contains(".txt") || linkfile.Contains(".csv") || linkfile.Contains(".gz"))
                {
                    files.Add(
                         new fileItem()
                         {
                             filename = linkfile,
                             fileurl = remoteUrl + link.Attributes["href"].Value
                         }
                        );
                }
            }
            url = remoteUrl;
            return files;
        }
        public static async Task downloadFiles(string url)
        {
            var files = Fileoperations.getFileList(ref url);
            foreach (var item in files)
            {
                try
                {
                    WebClient wbc = new WebClient();
                    wbc.DownloadFileAsync(new Uri(item.fileurl), Keys._TEMPDIRECTORY + item.filename);
                }
                catch (System.Exception ex)
                {
                    continue;
                }
            }
        }

        public static async Task<bool> downloadFiles( int[] filelist,string version)
        {
            if (File.Exists(Keys._SCENARIODIRECTORY + "/" + version + ".txt") == false)
                return false;

            string url = File.ReadLines(Keys._SCENARIODIRECTORY + "/" + version + ".txt").First();

            var files = Fileoperations.getFileList(ref url);
            int counter = 1;
            foreach (var item in files)
            {
                try
                {
                    if (filelist.Any(a => a == counter))
                    {
                        //Existing file will be deleted
                        if (File.Exists(Keys._TEMPDIRECTORY + version + item.filename))
                            File.Delete(Keys._TEMPDIRECTORY + version + item.filename);
                        if (new FileInfo(Keys._TEMPDIRECTORY + version + item.filename).FullName.Contains(version))
                            File.Delete(Keys._TEMPDIRECTORY + version + item.filename);

                        WebClient wbc = new WebClient();
                        wbc.DownloadFileAsync(new Uri(item.fileurl), Keys._TEMPDIRECTORY + version + item.filename);
                    }
                    counter++;
                }
                catch (System.Exception ex)
                {
                    continue;
                }
            }
            return true;
        }
        public static void WriteTofile(string filename, string content)
        {
            var contt = new List<string>();
            contt.Add(content);
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            File.WriteAllLines(filename, contt);
        }
    }
}
