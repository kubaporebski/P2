using System;
using BDL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestBDL
{
    [TestClass]
    public class LogTest
    {
        
        [TestMethod]
        public void SerializationTest()
        {
            const string someUri = "http://google.pl/ążśźęćńół";

            RequestLogList rll = RequestLogList.Create();
            Assert.IsNotNull(rll);

            rll.Add(new RequestLog(someUri));
            Assert.IsTrue(rll.Count > 0);

            rll = null;

            rll = RequestLogList.Create();
            Assert.IsNotNull(rll);
            Assert.IsTrue(rll.Count > 0);
            Assert.AreEqual(someUri, rll.List[0].RequestUri);
        }



    }
}
