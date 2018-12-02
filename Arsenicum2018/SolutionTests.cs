using System;
using System.Linq;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Arsenicum2018
{
    [TestClass]
    public class SolutionTests : BaseTest
    {
        private static readonly string OneLetterString1;
        private static readonly string OneLetterString2;
        private static readonly string OneLetterString3;
        private static readonly string OneLetterString4;

        static SolutionTests()
        {
            OneLetterString1 = "a";
            OneLetterString2 = "a" + Solution.Space + new string('a', 498);
            OneLetterString3 = new string('a', 498) + Solution.Space + "a";
            OneLetterString4 = string.Join(Solution.Space.ToString(), Enumerable.Range(0, 165).Select(i => "aa")) + Solution.Space + "a" + Solution.Space + "aaa";
        }

        [TestMethod]
        public void Test1()
        {
            Test(
                "by metal owl egg mr crow worm my ate",
                true);
        }
        [TestMethod]
        public void Test2()
        {
            Test(
                "live on time emit no evil",
                true);
        }

        [TestMethod]
        public void Test3()
        {
            Test(
                "abcc bc ac",
                false);
        }

        [TestMethod]
        public void TestOneLetterWord()
        {
            Test(
                "a ab ba aa bb",
                "a");
        }

        [TestMethod]
        public void TestSymmetricWords()
        {
            Test(
                "abbacc aaa",
                "aaa");
        }

        [TestMethod]
        public void TestPerformance()
        {
            const int wordLength = 6;
            const int wordCount = 10000;
            var r = new Random(5);
            var s = new string(Enumerable.Range(0, wordCount * wordLength).Select(i => (char)('a' + r.Next(26))).ToArray());
            var words = Enumerable.Range(0, wordCount).Select(i => s.Substring(i * wordLength, wordLength)).ToList();

            Test(
                string.Join(" ", words),
                false);
        }

        [TestMethod]
        public void OneLetterPerformanceTest1()
        {
            TestOneLetterPerformance(OneLetterString1);
        }

        [TestMethod]
        public void OneLetterPerformanceTest2()
        {
            TestOneLetterPerformance(OneLetterString2);
        }

        [TestMethod]
        public void OneLetterPerformanceTest3()
        {
            TestOneLetterPerformance(OneLetterString3);
        }

        [TestMethod]
        public void OneLetterPerformanceTest4()
        {
            TestOneLetterPerformance(OneLetterString4);
        }

        private void TestOneLetterPerformance(string s)
        {
            const int iterationCount = 1000000;
            for (int i = 0; i < iterationCount; i++)
            {
                Test(s, true);
            }
        }

        private void Test(string s, string expectedResult)
        {
            var solution = new Solution();
            string stringResult = solution.solution(s);
            Assert.AreEqual(expectedResult, stringResult);
        }

        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        private void Test(string s, bool isPalindromExpected)
        {
            var solution = new Solution();
            string stringResult = solution.solution(s);
            if (stringResult == "NO")
            {
                Assert.IsFalse(isPalindromExpected);
                return;
            }

            stringResult = stringResult.Replace(Solution.Space.ToString(), string.Empty);
            bool actualResultIsPalindrom = stringResult.ReverseString() == stringResult;
            if (!actualResultIsPalindrom)
            {
                Assert.Fail("Returned value is not a palindrom.");
            }

            Assert.IsTrue(isPalindromExpected);
        }
    }
}
