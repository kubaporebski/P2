using System;

namespace BDL
{
    public class RequestLog
    {
        private static int LogCount = 1;

        public int RID { get; private set; }
        
        public string RequestUri { get; private set; }

        public DateTime LogTime { get; private set; }

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