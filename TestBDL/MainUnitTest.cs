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
            Downloader.SetClientId("51b8ff67-411c-47c6-8dea-08d689f6cc93");
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