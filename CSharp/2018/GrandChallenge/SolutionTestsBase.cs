using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GrandChallenge
{
    public abstract class SolutionTestsBase : BaseTest
    {
        protected void Test(string s, int expectedResult)
        {
            var solution = new Solution();
            int actualResult = solution.solution(s);
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}