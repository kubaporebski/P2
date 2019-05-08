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

        private static string X_CLIENT_ID = "";

        public static List<RequestLog> Logs { get; private set; }

        static Downloader()
        {
            Logs = new List<RequestLog>();
        }

        /// <summary>
        /// Ustawienie identyfikatora użytkownika API.
        /// </summary>
        /// <param name="newClientId"></param>
        public static void SetClientId(string newClientId)
        {
            X_CLIENT_ID = newClientId;
        }

        /// <summary>
        /// Pobranie dokumentu XML znajdującego się pod podanym adresem URI.
        /// Metoda wysyła żądanie z nagłówkiem X-ClientId ustawionym za pomocą metody SetClientId.
        /// </summary>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        public static XDocument DownloadXML(string requestUri)
        {
            WaitBefore();
            SaveRequestLog(requestUri);

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

        /// <summary>
        /// Dodanie wpisu do logów
        /// </summary>
        /// <param name="requestUri"></param>
        private static void SaveRequestLog(string requestUri)
        {
            Logs.Add(new RequestLog(requestUri));
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
            var diff = DateTime.Now - LastRequest;
            if (diff.TotalSeconds < 1)
                System.Threading.Thread.Sleep(1000);

            LastRequest = DateTime.Now;
        }
    }
}
