using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Nickel2018
{
    [TestClass]
    public class SolutionTests : BaseTest
    {
        [TestMethod]
        public void Test1()
        {
            Test(
                new[] { true, false, false, true, false },
                11);
        }

        [TestMethod]
        public void Test2()
        {
            Test(
                new []{ true, false, false, true },
                7);
        }

        [TestMethod]
        public void Test3()
        {
            Test(
                new []{ false },
                0);
        }

        [TestMethod]
        public void Test4()
        {
            Test(
                new []{ false, false, false, false, false },
                0);
        }

        private void Test(bool[] p, int expectedResult)
        {
            var solution = new Solution();
            int actualResult = solution.solution(p);
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
