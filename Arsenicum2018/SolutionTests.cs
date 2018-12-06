using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Arsenicum2018
{
    [TestClass]
    public class SolutionTests : SolutionTestsBase
    {
        [TestMethod]
        public void FindLongPalindromTest()
        {
            var randomWord = new RandomWord(5);
            var words = new List<string> {"abc"};
            do
            {
                string word = randomWord.Next(3);
                string s = word + Solution.Space + string.Join(Solution.Space.ToString(), words);
                var a = new Solution().solution(s);
                if (a == "NO")
                {
                    words.Add(word);
                }
            } while (words.Count < 50);
        }

        [TestMethod]
        public void FindBugTest()
        {
            var randomWord = new RandomWord(5);
            var r = new Random(10);
            for (int i = 0; i < 10000; i++)
            {
                var s = randomWord.Next(100);
                s = s + s.ReverseString();
                for (int j = 0; j < 20; j++)
                {
                    s = s.Insert(r.Next(s.Length), Solution.Space.ToString());
                }

                s = s.Trim(Solution.Space);
                while (s.Contains(Solution.Space.ToString() + Solution.Space))
                {
                    s = s.Replace(Solution.Space.ToString() + Solution.Space, string.Empty);
                }

                Test(
                    s,
                    true);
            }
        }

        [TestMethod]
        public void FindBug2Test()
        {
            var randomWord = new RandomWord(5);
            var r = new Random(10);
            for (int i = 0; i < 10000; i++)
            {
                var s = randomWord.Next(100);
                s = s + randomWord.Next(1) + s.ReverseString();
                for (int j = 0; j < 20; j++)
                {
                    s = s.Insert(r.Next(s.Length), Solution.Space.ToString());
                }

                s = s.Trim(Solution.Space);
                while (s.Contains(Solution.Space.ToString() + Solution.Space))
                {
                    s = s.Replace(Solution.Space.ToString() + Solution.Space, string.Empty);
                }

                Test(
                    s,
                    true);
            }
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
        public void SentenceWithOneLetterTest()
        {
            Test(
                "a",
                "a");
        }

        [TestMethod]
        public void SentenceWithOneLetterWordInTheBeginning()
        {
            Test(
                "a ab",
                "a");
        }

        [TestMethod]
        public void SentenceWithOneLetterWordInTheEnd()
        {
            Test(
                "ab a",
                "a");
        }

        [TestMethod]
        public void SentenceWithOneLetterWordInTheMiddle()
        {
            Test(
                "ab a bc",
                "a");
        }

        [TestMethod]
        public void SentenceWithSemiPalindromicWordInTheMiddle()
        {
            Test(
                "ab ccba",
                "ab ccba");
        }

        [TestMethod]
        public void TestSymmetricWords()
        {
            Test(
                "abbacc aaa",
                "aaa");
        }

        [TestMethod]
        public void MidPalindrom1Test()
        {
            Test(
                "abcdc ba",
                "abcdc ba");
        }

        [TestMethod]
        public void MidPalindrom2Test()
        {
            Test(
                "ab cdcba",
                "ab cdcba");
        }

        [TestMethod]
        public void MidPalindrom3Test()
        {
            Test(
                "ab cba",
                "ab cba");
        }

        [TestMethod]
        public void MidPalindrom4Test()
        {
            Test(
                "abc ba",
                "abc ba");
        }

        [TestMethod]
        public void MidPalindrom5Test()
        {
            Test(
                "abcddc ba",
                "abcddc ba");
        }

        [TestMethod]
        public void MidPalindrom6Test()
        {
            Test(
                "ab cddcba",
                "ab cddcba");
        }

        [TestMethod]
        public void MidPalindrom7Test()
        {
            Test(
                "abcd dc ba",
                "abcd dc ba");
        }

        [TestMethod]
        public void MidPalindrom8Test()
        {
            Test(
                "ab cd dcba",
                "ab cd dcba");
        }

        [TestMethod]
        public void OneLetterInTheMiddleTest()
        {
            Test(
                "fbdfhhgcfebbg hedbfafcaccffcdhcfd h fh gddefdd ahfba afedchcaehb hcfdddg gbbagaf bdbhhcf decfcgddfechcecahheb hhbehhacechcefddgcfce dfchhbdbfagabbggdddfch bheachcde faabfhaddfeddghfhd fchdcffccacfafbdeh gbbefcghhfdbf",
                "h");
        }

        [TestMethod]
        public void BigTest()
        {
            Test(
                "fcacagcebagd cb chdbh fdfbchcb geehafgaegbdafa fccfheecgc gahhbgdgeeaebfgdbfabfha da ga bcf ccg fcfhacafeeaabbe ebbaaeefacahfcf gccfcb agadahf bafbdg fbeaeegd gbhhagc gceehfccfafadbgea gfaheegbchcbfdfhbdhcbcdgabecgaca cf",
                true);
        }

        private void Test(string s, string expectedResult)
        {
            var solution = new Solution();
            string stringResult = solution.solution(s);
            Assert.AreEqual(expectedResult, stringResult);
        }

        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
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