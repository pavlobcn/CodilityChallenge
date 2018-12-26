using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Germanium2018
{
    [TestClass]
    public class SolutionTests : BaseTest
    {
        [TestMethod]
        public void Test1()
        {
            Test(
                "[1, 2, 4, 3]",
                "[1, 3, 2, 3]",
                5);
        }
        [TestMethod]
        public void Test2()
        {
            Test(
                "[4, 2, 1, 6, 5]",
                "[3, 2, 1, 7, 7]",
                4);
        }

        [TestMethod]
        public void Test3()
        {
            Test(
                "[2, 3]",
                "[2, 3]",
                1);
        }

        private void Test(string a, string b, int expectedResult)
        {
            TestInternal(ConvertArray(a), ConvertArray(b), expectedResult);
        }

        private void TestInternal(int[] a, int[] b, int expectedResult)
        {
            var solution = new Solution();
            int actualResult = solution.solution(a, b);
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
