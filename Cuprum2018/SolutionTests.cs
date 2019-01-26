using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cuprum2018
{
    [TestClass]
    public class SolutionTests : BaseTest
    {
        [TestMethod]
        public void Test1()
        {
            Test(
                "6daa6ccaaa6d",
                8);
        }

        [TestMethod]
        public void Test2()
        {
            Test(
                "abca",
                0);
        }

        private void Test(string s, int expectedResult)
        {
            var solution = new Solution();
            int actualResult = solution.solution(s);
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
