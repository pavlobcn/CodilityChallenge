using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gallium2018
{
    [TestClass]
    public class SolutionTests : BaseTest
    {
        [TestMethod]
        public void Test1()
        {
            Test(
                "[7, 15, 6, 20, 5, 10]",
                3);
        }

        [TestMethod]
        public void Test2()
        {
            Test(
                "[25, 10, 25, 10, 32]",
                4);
        }

        private void Test(string a, int expectedResult)
        {
            TestInternal(ConvertArray(a), expectedResult);
        }

        private void TestInternal(int[] a, int expectedResult)
        {
            var solution = new Solution();
            int actualResult = solution.solution(a);
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
