using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Strontium2019
{
    [TestClass]
    public class SolutionTests : BaseTest
    {
        [TestMethod]
        public void Test1()
        {
            Test(
                new []{"aabb", "aaaa", "bbab"},
                6);
        }
        [TestMethod]
        public void Test2()
        {
            Test(
                new[] { "xxbxx", "xbx", "x" },
                4);
        }

        [TestMethod]
        public void Test3()
        {
            Test(
                new[] { "dd", "bb", "cc", "dd" },
                4);
        }

        private void Test(string[] words, int expectedResult)
        {
            var solution = new Solution();
            int actualResult = solution.solution(words);
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
