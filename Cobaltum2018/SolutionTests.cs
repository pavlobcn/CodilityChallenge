using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cobaltum2018
{
    [TestClass]
    public class SolutionTests : BaseTest
    {
        [TestMethod]
        public void Test1()
        {
            Test(
                new[] { 5, 3, 7, 7, 10 },
                new[] { 1, 6, 6, 9, 9 },
                2);
        }

        [TestMethod]
        public void Test2()
        {
            Test(
                new[] { 5, -3, 6, 4, 8 },
                new[] { 2, 6, -5, 1, 0 },
                -1);
        }

        [TestMethod]
        public void Test3()
        {
            Test(
                new[] { 1, 5, 6 },
                new[] { -2, 0, 2 },
                0);
        }

        [TestMethod]
        public void Test4()
        {
            Test(
                new[] { 1, 3, 5, 7, 8, 10 },
                new[] { 2, 4, 6, 6, 7, 11 },
                1);
        }

        private void Test(int[] a, int[] b, int expectedResult)
        {
            int actualResult = new Solution().solution(a, b);
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
