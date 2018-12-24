using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DigitalGold
{
    [TestClass]
    public class SolutionTests : BaseTest
    {
        [TestMethod]
        public void Test1()
        {
            Test(
                5,
                5,
                "[0, 4, 2, 0]",
                "[0, 0, 1, 4]",
                3);
        }

        private void Test(int n, int m, string x, string y, int expectedResult)
        {
            TestInternal(n, m, ConvertArray(x), ConvertArray(y), expectedResult);
        }

        private void TestInternal(int n, int m, int[] x, int[] y, int expectedResult)
        {
            var solution = new Solution();
            int actualResult = solution.solution(n, m, x, y);
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
