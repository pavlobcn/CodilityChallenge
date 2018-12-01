using System;
using System.Linq;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Arsenicum2018
{
    [TestClass]
    public class SolutionTests : BaseTest
    {
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
            const int wordCount = 10;
            var r = new Random(5);
            var s = new string(Enumerable.Range(0, wordCount * wordLength).Select(i => (char)('a' + r.Next(26))).ToArray());
            var words = Enumerable.Range(0, wordCount).Select(i => s.Substring(i * wordLength, wordLength)).ToList();
            var reversed = s.ReverseString();
            words.Add(reversed.Substring(0, wordLength / 2));
            words.Add(reversed.Substring(wordLength / 2, wordCount * (wordLength - 1)));
            words.Add(reversed.Substring(wordCount * (wordLength - 1)));


            Test(
                string.Join(" ", words),
                true);
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
