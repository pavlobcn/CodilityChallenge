﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rubidium2018
{
    [TestClass]
    public class SolutionTests : SolutionTestsBase
    {
        [TestMethod]
        public void Test1()
        {
            Test(
                "[0, 2]",
                "[0, 0]",
                1);
        }

        [TestMethod]
        public void Test2()
        {
            Test(
                "[0, 0, 10, 10]",
                "[0, 10, 0, 10]",
                5);
        }

        [TestMethod]
        public void Test3()
        {
            Test(
                "[1, 1, 8]",
                "[1, 6, 0]",
                2);
        }

        private void Test(string xArray, string yArray, int expectedResult)
        {
            TestInternal(ConvertArray(xArray), ConvertArray(yArray), expectedResult);
        }
    }
}
