using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Zirconium2019
{
    [TestClass]
    public class SolutionTests
    {
        [TestMethod]
        public void Test1()
        {
            Test(
                new[] { 4, 2, 1 },
                new[] { 2, 5, 3 },
                2,
                10);
        }
        [TestMethod]
        public void Test2()
        {
            Test(
                new[] { 7, 1, 4, 4 },
                new[] { 5, 3, 4, 3 },
                2,
                18);
        }

        [TestMethod]
        public void Test3()
        {
            Test(
                new[] { 5, 5, 5 },
                new[] { 5, 5, 5 },
                3,
                15);
        }

        private void Test(int[] a, int[] b, int f, int expectedResult)
        {
            var solution = new Solution();
            int actualResult = solution.solution(a, b, f);
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
