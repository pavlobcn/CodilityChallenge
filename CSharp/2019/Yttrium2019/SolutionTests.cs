using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Yttrium2019
{
    [TestClass]
    public class SolutionTests : BaseTest
    {
        [TestMethod]
        public void Test1()
        {
            Test(
                "abaacbca",
                2,
                3);
        }

        [TestMethod]
        public void Test2()
        {
            Test(
                "aabcabc",
                1,
                5);
        }

        [TestMethod]
        public void Test3()
        {
            Test(
                "zaaaa",
                1,
                1);
        }

        [TestMethod]
        public void Test4()
        {
            Test(
                "aaaa",
                2,
                -1);
        }

        [TestMethod]
        public void Test5()
        {
            Test(
                "aaaa",
                0,
                4);
        }

        private void Test(string s, int k, int expectedResult)
        {
            var solution = new Solution();
            int actualResult = solution.solution(s, k);
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
