using Microsoft.SqlServer.Server;
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
        private static DateTime LastRequest = DateTime.Now;
        
        /// <summary>
        /// Nasze zarejestrowane identyfikatory do pobierania danych
        /// </summary>
        private static NeverendingList<string> clientIdList = new NeverendingList<string>()
        {
            "e595b260-5874-48e6-d0a1-08d6d44dd662",
            "caaf0c0a-ef9e-41de-05a0-08d6d49aeefd",
            "e06b2f38-0840-437d-daaa-08d6d518672f",
            "bbee9905-1eb3-4520-e60d-08d6dde4b8b3",
            "057f1a35-51f4-4cb2-430b-08d6e741d253",
            "91d3510e-67fe-45d8-430c-08d6e741d253",
            "965147c4-449d-433f-d59c-08d6eb771137"
        };

        /// <summary>
        /// Logi wywołań.
        /// </summary>
        public static RequestLogList Logs { get; private set; }

        static Downloader()
        {
            Logs = RequestLogList.Create();
        }
        
        /// <summary>
        /// Pobranie dokumentu XML znajdującego się pod podanym adresem URI.
        /// </summary>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        public static XDocument DownloadXML(string requestUri)
        {
            WaitBefore();
            SaveRequestLog(requestUri);

            var tmpFile = Path.GetTempFileName();
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Headers["X-ClientId"] = clientIdList.NextElement;
                    wc.DownloadFile(requestUri, tmpFile);

                    return XDocument.Load(tmpFile);
                }
            }
            finally
            {
                File.Delete(tmpFile);
            }
        }

        /// <summary>
        /// Dodanie wpisu do logów
        /// </summary>
        /// <param name="requestUri"></param>
        private static void SaveRequestLog(string requestUri)
        {
            var log = new RequestLog(requestUri);
            Logs.Add(log);
        }

        /// <summary>
        /// Odczekanie sekundy przed wywołaniem żądania HTTP.
        /// Potrzebne ze względu na limity żądań:
        ///     Okres 	Użytkownik zarejestrowany
        ///     1s 	    10
        ///     15m 	500
        ///     12h 	5000
        ///     7d 	    50000
        /// </summary>
        private static void WaitBefore()
        {
            var n = Logs.Count / 100;
            
            System.Threading.Thread.Sleep(1000 + (n * 50));
            LastRequest = DateTime.Now;
        }
    }
}
