using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FutureMobility
{
    [TestClass]
    public class SolutionTests : BaseTest
    {
        [TestMethod]
        public void Test1()
        {
            Test(
                "[1, 1, 2, 4, 3]",
                "[2, 2, 2, 3, 2]",
                3);
        }

        [TestMethod]
        public void Test2()
        {
            Test(
                "[0, 0, 2, 1, 8, 8, 2, 0]",
                "[8, 5, 2, 4, 0, 0, 0, 2]",
                31);
        }

        [TestMethod]
        public void Test3()
        {
            Test(
                "[1000000000, 1000000000, 1000000000, 0, 0, 0]",
                "[0, 0, 0, 1000000000, 1000000000, 1000000000]",
                999999972);
        }

        [TestMethod]
        public void Test4()
        {
            Test(
                "[2]",
                "[1]",
                -1);
        }

        [TestMethod]
        public void TestSimple1()
        {
            Test(
                "[5, 0]",
                "[0, 5]",
                5);
        }

        private void Test(string a, string b, int expectedResult)
        {
            TestInternal(ConvertArray(a), ConvertArray(b), expectedResult);
        }

        private void TestInternal(int[] a, int[] b, int expectedResult)
        {
            var solution = new Solution();
            int actualResult = solution.solution(a, b);
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
