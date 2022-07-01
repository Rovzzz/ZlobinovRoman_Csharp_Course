using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GCD_Test_Project
{
    [TestClass]
    public class GCDAlgorithmsTest
    {

        private TestContext testContextInstance;
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        [TestMethod()]
        public void FindGCDEuclidTest()
        {
            int a = 2806;
            int b = 345;
            int expected = 23; 
            GCDAlgorithmsTest c = new GCDAlgorithmsTest();
            int actual = c.FindGCDEuclidTest(a,b);
            Assert.AreEqual(expected, actual);
        }

        private int FindGCDEuclidTest(int a, int b)
        {
            throw new NotImplementedException();
        }
    }
}


