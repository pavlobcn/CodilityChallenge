﻿using System.Linq;
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

            bool actualResultIsPalindrom = new string(stringResult.Reverse().ToArray()) == s;
            if (!actualResultIsPalindrom)
            {
                Assert.Fail("Returned value is not a palindrom.");
            }

            Assert.IsTrue(isPalindromExpected);
        }
    }
}
