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
        /// Do synchronizacji między wątkami
        public static readonly object lockobject = new object();

        [XmlArray(ElementName = "List"), XmlArrayItem(typeof(RequestLog), ElementName = "Log")]
        public List<RequestLog> List { get; set; }

        /// <summary>
        /// Liczba wpisów
        /// </summary>
        public int Count
        {
            get { return List.Count; }
        }

        /// <summary>
        /// Instancję pobieramy poprzez metodę Deserialize.
        /// </summary>
        /// <see cref="Deserialize(string)"/>
        private RequestLogList()
        {
        }

        /// <summary>
        /// Utworzenie instacji.
        /// Metoda wczytuje dane z pliku %APPDATA%\bdl_requestlog.xml.
        /// Dla SQL Servera będzie to zwykle C:\Users\MSSQL$D2017\AppData\Roaming
        /// </summary>
        /// <returns></returns>
        public static RequestLogList Create()
        {
            var path = Path.Combine(Environment.GetEnvironmentVariable("APPDATA"), "bdl_requestlog.xml");
            return Deserialize(path);
        }
        
        /// <summary>
        /// Dodanie wpisu.
        /// </summary>
        /// <param name="log"></param>
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
            lock (lockobject)
            {
                foreach (var log in List.OrderByDescending(rlt => rlt.LogTime))
                    callback(log);
            }
        }

        /// <summary>
        /// Serializacja. Mamy w pamięci listę, chcemy ją zapisać do pliku na dysk.
        /// </summary>
        private void Serialize()
        {
            try
            {
                lock (lockobject)
                {
                    var path = Path.Combine(Environment.GetEnvironmentVariable("APPDATA"), "bdl_requestlog.xml");
                    using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                    {
                        var serializer = new XmlSerializer(typeof(RequestLogList));
                        serializer.Serialize(fs, this);
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Deserializacja. Mamy plik xml, chcemy mieć dane w postaci listy.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static RequestLogList Deserialize(string path)
        {
            try
            {
                lock (lockobject)
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
                            return createNewEmpty();
                        }
                    }
                }
            }
            catch
            {
                return createNewEmpty();
            }
        }

        /// <summary>
        /// Pomocnicza metoda do utworzenia pustej instancji
        /// </summary>
        /// <returns></returns>
        private static RequestLogList createNewEmpty()
        {
            return new RequestLogList()
            {
                List = new List<RequestLog>()
            };
        }
    }
}