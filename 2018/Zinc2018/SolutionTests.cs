using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Zinc2018
{
    [TestClass]
    public class SolutionTests : BaseTest
    {
        [TestMethod]
        public void Test1()
        {
            Test(
                "[1, 2, 1, 1]",
                3);
        }
        [TestMethod]
        public void Test2()
        {
            Test(
                "[1, 2, 3, 4]",
                4);
        }

        [TestMethod]
        public void Test3()
        {
            Test(
                "[2, 2, 2, 2]",
                1);
        }

        [TestMethod]
        public void Test4()
        {
            Test(
                "[2, 2, 1, 2, 2]",
                4);
        }

        [TestMethod]
        public void Test5()
        {
            Test(
                "[1, 2]",
                0);
        }

        private void Test(string a, int expectedResult)
        {
            TestInternal(ConvertArray(a) , expectedResult);
        }

        private void TestInternal(int[] a, int expectedResult)
        {
            var solution = new Solution();
            int actualResult = solution.solution(a);
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
