using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rubidium2018
{
    public abstract class SolutionTestsBase : BaseTest
    {
        protected void TestInternal(int[] x, int[] y, int expectedResult)
        {
            var solution = new Solution();
            int actualResult = solution.solution(x, y);
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}