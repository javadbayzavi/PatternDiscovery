using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using discovery.Library.zip;

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
        public static bool downloadFiles(ref string url)
        {
            var files = Fileoperations.getFileList(ref url);
            foreach (var item in files)
            {
                try
                {
                    WebClient wbc = new WebClient();
                    wbc.DownloadFileAsync(new Uri(item.fileurl), Keys._TEMPDIRECTORY + item.filename);
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
            return true;
        }
    }
}
