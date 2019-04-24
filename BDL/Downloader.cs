using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BDL
{
    public class Downloader
    {
        public static string X_CLIENT_ID = "";
        
        public static void SetClientId(string newClientId)
        {
            X_CLIENT_ID = newClientId;
        }

        public static async Task<XDocument> DownloadXML(string requestUri)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-ClientId", X_CLIENT_ID);
                HttpResponseMessage response = await client.GetAsync(requestUri);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                return XDocument.Parse(responseBody);
            }
        }
    }
}
