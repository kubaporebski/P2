﻿using System;
using BDL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestBDL
{
    [TestClass]
    public class MainUnitTest
    {
        public MainUnitTest()
        {
            
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
        
    }
}
