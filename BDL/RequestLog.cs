using System;
using System.Xml.Serialization;

namespace BDL
{
    public class RequestLog
    {
        private static int LogCount = 1;

        [XmlElement(ElementName = "Id")]
        public int RID { get; set; }
        
        [XmlElement(ElementName = "Uri")]
        public string RequestUri { get; set; }

        [XmlElement]
        public DateTime LogTime { get; set; }

        public RequestLog()
        {
        }

        public RequestLog(string requestUri)
        {
            RID = LogCount++;
            RequestUri = requestUri;
            LogTime = DateTime.Now;
        }

        public override string ToString()
        {
            return string.Format("{0}\t{1}\t{2}\r\n", RID, LogTime.ToShortDateString() + " " + LogTime.ToLongTimeString(), RequestUri);
        }
    }
}