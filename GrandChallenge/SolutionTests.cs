using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GrandChallenge
{
    [TestClass]
    public class SolutionTests : BaseTest
    {
        [TestMethod]
        public void Test1()
        {
            Test(
                "cabbacc",
                4);
        }
        [TestMethod]
        public void Test2()
        {
            Test(
                "abababa",
                6);
        }

        [TestMethod]
        public void Test3()
        {
            Test(
                "aaaaaaa",
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
