using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Molybdenum2019
{
    [TestClass]
    public class SolutionTests : BaseTest
    {
        [TestMethod]
        public void Test1()
        {
            Test(new[] {2, 3}, 3, 5, new[] {2, 1, 3, 1, 2, 2, 3});
        }
        [TestMethod]
        public void Test2()
        {
            Test(new[] { 2, 3 }, 4, 2, new[] { 1, 2, 2, 1, 2 });
        }

        private void Test(int[] expectedResult, int k, int m, int[] a)
        {
            var solution = new Solution();
            int[] actualResult = solution.solution(k, m, a);
            CollectionAssert.AreEquivalent(expectedResult, actualResult);
        }
    }
}
