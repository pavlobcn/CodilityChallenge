using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ferrum2018
{
    [TestClass]
    public class SolutionTests : BaseTest
    {
        [TestMethod]
        public void Test1()
        {
            Test(
                new[] { -1, -1, 1, -1, 1, 0, 1, -1, -1 },
                7);
        }
        [TestMethod]
        public void Test2()
        {
            Test(
                new[] { 1, 1, -1, -1, -1, -1, -1, 1, 1 },
                4);
        }

        private void Test(int[] a, int expectedResult)
        {
            var solution = new Solution();
            int actualResult = solution.solution(a);
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
