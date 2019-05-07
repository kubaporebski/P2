using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BDL
{
    public static class Downloader
    {
        public static string X_CLIENT_ID = "";
        
        public static void SetClientId(string newClientId)
        {
            X_CLIENT_ID = newClientId;
        }

        public static XDocument DownloadXML(string requestUri)
        {
            using (WebClient wc = new WebClient())
            {
                string tmpFile = Path.GetTempFileName();
                try
                {

                    wc.Headers["X-ClientId"] = X_CLIENT_ID;
                    wc.DownloadFile(requestUri, tmpFile);

                    return XDocument.Parse(File.ReadAllText(tmpFile));
                }
                finally
                {
                    if (File.Exists(tmpFile))
                        File.Delete(tmpFile);
                }
            }
        }
    }
}
