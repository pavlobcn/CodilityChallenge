using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Selenium2018
{
    [TestClass]
    public class SolutionTests : BaseTest
    {
        [TestMethod]
        public void Test1()
        {
            Test(
                "[1, 2, 2, 3, 4]",
                "[1, 1, 4, 5, 4]",
                5);
        }
        [TestMethod]
        public void Test2()
        {
            Test(
                "[1, 1, 1, 1]",
                "[1, 2, 3, 4]",
                6);
        }

        [TestMethod]
        public void Test3()
        {
            Test(
                "[1, 1, 2]",
                "[1, 2, 1]",
                4);
        }

        private void Test(string x, string y, int expectedResult)
        {
            TestInternal(ConvertArray(x), ConvertArray(y), expectedResult);
        }

        private void TestInternal(int[] x, int[] y, int expectedResult)
        {
            var solution = new Solution();
            int actualResult = solution.solution(x, y);
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
