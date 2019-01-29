using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CuttingComplexity
{
    [TestClass]
    public class SolutionTests : BaseTest
    {
        [TestMethod]
        public void Test1()
        {
            Test(
                "MLMMLLM",
                3,
                1);
        }

        [TestMethod]
        public void Test2()
        {
            Test(
                "MLMMMLMMMM",
                2,
                2);
        }

        [TestMethod]
        public void Test3()
        {
            Test(
                "LLMMM",
                0,
                3);
        }

        private void Test(string s, int k, int expectedResult)
        {
            var solution = new Solution();
            int actualResult = solution.solution(s, k);
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
