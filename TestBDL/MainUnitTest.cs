using System;
using BDL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestBDL
{
    [TestClass]
    public class MainUnitTest
    {
        public MainUnitTest()
        {
            System.Diagnostics.Debug.WriteLine("Hello!");
        }

        [TestMethod]
        public void TestSubjects()
        {
            var result = DataGetter.Subjects(null, 10);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestVariables()
        {
            var result = DataGetter.Variables("P3183", 10);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestMeasures()
        {
            var result = DataGetter.Measures();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestAtributes()
        {
            var result = DataGetter.Attributes();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestUnits()
        {
            var result = DataGetter.Units(100);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestDataByVariable()
        {
            var result = DataGetter.DataByVariable(123, null, 2000, 2000, 0, 100);
            Assert.IsNotNull(result);
        }
    }
}
