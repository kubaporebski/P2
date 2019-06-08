using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace BDL
{
    [XmlRoot(ElementName = "LogListData")]
    public class RequestLogList
    {
        [XmlArray(ElementName = "List"), XmlArrayItem(typeof(RequestLog), ElementName = "Log")]
        public List<RequestLog> List { get; set; }

        /// <summary>
        /// Liczba wpisów
        /// </summary>
        public int Count
        {
            get { return List.Count; }
        }

        private RequestLogList()
        {
        }

        /// <summary>
        /// Utworzenie instacji.
        /// Metoda wczytuje dane z pliku %HOMEPATH%/bdl_requestlog.xml
        /// </summary>
        /// <returns></returns>
        public static RequestLogList Create()
        {
            var path = Path.Combine(Environment.GetEnvironmentVariable("APPDATA"), "bdl_requestlog.xml");
            return Deserialize(path);
        }
        
        public void Add(RequestLog log)
        {
            List.Add(log);
            Serialize();
        }

        /// <summary>
        /// Iterowanie po wszystkich wpisach w logu. Logi sortowane są tu malejąco po czasie wpisu.
        /// </summary>
        /// <param name="callback">metoda wykonywana dla każdego wpisu</param>
        public void ForEach(Action<RequestLog> callback)
        {
            lock (this)
            {
                foreach (var log in List.OrderByDescending(rlt => rlt.LogTime))
                    callback(log);
            }
        }

        private void Serialize()
        {
            var path = Path.Combine(Environment.GetEnvironmentVariable("APPDATA"), "bdl_requestlog.xml");
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                var serializer = new XmlSerializer(typeof(RequestLogList));
                serializer.Serialize(fs, this);
            }
        }

        private static RequestLogList Deserialize(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                if (fs.Length > 0)
                {
                    var serializer = new XmlSerializer(typeof(RequestLogList));
                    return serializer.Deserialize(fs) as RequestLogList;
                }
                else
                {
                    return new RequestLogList()
                    {
                        List = new List<RequestLog>()
                    };
                }
            }
        }
    }
}