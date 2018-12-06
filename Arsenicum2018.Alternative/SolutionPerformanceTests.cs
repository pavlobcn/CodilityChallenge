using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Arsenicum2018
{
    [TestClass]
    public class SolutionPerformanceTests : SolutionTestsBase
    {
        private static readonly string OneLetterString1;
        private static readonly string OneLetterString2;
        private static readonly string OneLetterString3;
        private static readonly string OneLetterString4;
        private static readonly string LongStringNoPalindromPerformance;
        private static readonly string LongStringPalindromPerformance;
        private static readonly string LongStringPalindrom2Performance;

        static SolutionPerformanceTests()
        {
            OneLetterString1 = "a";
            OneLetterString2 = "a" + Solution.Space + new string('a', 498);
            OneLetterString3 = new string('a', 498) + Solution.Space + "a";
            OneLetterString4 = string.Join(Solution.Space.ToString(), Enumerable.Range(0, 165).Select(i => "aa")) +
                               Solution.Space + "a" + Solution.Space + "aaa";

            LongStringNoPalindromPerformance = GetLongStringNoPalindromPerformance();
            LongStringPalindromPerformance = GetLongStringPalindromPerformance();
            LongStringPalindrom2Performance = GetLongStringPalindrom2Performance();
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

        [TestMethod]
        public void LongStringPalindromPerformance2Test()
        {
            const int iterationCount = 1000;
            for (int i = 0; i < iterationCount; i++)
            {
                Test(LongStringPalindrom2Performance, true);
            }
        }

        [TestMethod]
        public void CycleTest()
        {
            Test(
                "ab cdab dcba",
                false);
        }

        private void TestOneLetterPerformance(string s)
        {
            const int iterationCount = 100000;
            for (int i = 0; i < iterationCount; i++)
            {
                Test(s, true);
            }
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

        private static string GetLongStringPalindrom2Performance()
        {
            var left = "abc fdh bhe ahc aad bha gcd agg gha agc adh gca hge bae fhh hce bcd ddf fca dab ahf dah bhf fba aae gha efc dah adg gbh gab egb hca fca ehc gdh hce bhg ega bch gaa ffg acb ehf ebc edb bga ghh gbb deb";
            return left + left.Replace(Solution.Space.ToString(), string.Empty).ReverseString();
        }
    }
}