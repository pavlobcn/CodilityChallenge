﻿using System;
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
        private static readonly string LongStringNoPalindromPerformance;
        private static readonly string LongStringPalindromPerformance;

        static SolutionTests()
        {
            OneLetterString1 = "a";
            OneLetterString2 = "a" + Solution.Space + new string('a', 498);
            OneLetterString3 = new string('a', 498) + Solution.Space + "a";
            OneLetterString4 = string.Join(Solution.Space.ToString(), Enumerable.Range(0, 165).Select(i => "aa")) +
                               Solution.Space + "a" + Solution.Space + "aaa";

            LongStringNoPalindromPerformance = GetLongStringNoPalindromPerformance();
            LongStringPalindromPerformance = GetLongStringPalindromPerformance();
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

        [TestMethod]
        public void LongStringNoPalindromPerformanceTest()
        {
            const int iterationCount = 1000;
            for (int i = 0; i < iterationCount; i++)
            {
                Test(LongStringNoPalindromPerformance, false);
            }
        }

        [TestMethod]
        public void LongStringPalindromPerformanceTest()
        {
            const int iterationCount = 1000;
            for (int i = 0; i < iterationCount; i++)
            {
                Test(LongStringPalindromPerformance, true);
            }
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

        private static string GetLongStringNoPalindromPerformance()
        {
            var randomWord = new RandomWord(5);
            string[] words = Enumerable.Range(0, 80).Select(i => randomWord.Next(3))
                .Union(Enumerable.Range(0, 70).Select(i => randomWord.Next(4))).ToArray();
            string[] excludePalindromes = words.Where(w => w == new string(w.Reverse().ToArray())).ToArray();
            words = words.Where(w => !excludePalindromes.Contains(w)).ToArray();
            string[] exclude = { "bhe", "ahb", "adh", "had", "ehc", "agc", "age", "efc",
                "fhdf", "facd", "fgga", "dgga", "agbd", "abcb", "abcg", "bdcb", "bcag", "ghgd", "chdg", "fech", "hdga", "hadc", "gbed", "dfgc", "dfgh",
                "cfhe", "habc"
            };
            words = words.Where(w => !exclude.Contains(w)).ToArray();
            return string.Join(Solution.Space.ToString(), words);
        }

        private static string GetLongStringPalindromPerformance()
        {
            var randomWord = new RandomWord(5);
            string[] words = Enumerable.Range(0, 80).Select(i => randomWord.Next(3))
                .Union(Enumerable.Range(0, 70).Select(i => randomWord.Next(4))).ToArray();
            string[] excludePalindromes = words.Where(w => w == new string(w.Reverse().ToArray())).ToArray();
            words = words.Where(w => !excludePalindromes.Contains(w)).ToArray();
            string[] exclude = {"bhe", "ahb", "adh", "had", "ehc", "agc", "age", "efc"};
            words = words.Where(w => !exclude.Contains(w)).ToArray();
            return string.Join(Solution.Space.ToString(), words);
        }
    }

    class RandomWord
    {
        private readonly Random _random;

        public RandomWord(int seed)
        {
            _random = new Random(seed);
        }

        public string Next(int length)
        {
            return new string(Enumerable.Range(0, length).Select(_ => (char) ('a' + _random.Next(8))).ToArray());
        }
    }
}