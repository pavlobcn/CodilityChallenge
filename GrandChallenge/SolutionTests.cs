using System.Linq;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GrandChallenge
{
    [TestClass]
    public class SolutionTests : BaseTest
    {
        [TestMethod]
        public void Test1()
        {
            Test(
                "cabbacc",
                4);
        }
        [TestMethod]
        public void Test2()
        {
            Test(
                "abababa",
                6);
        }

        [TestMethod]
        public void Test3()
        {
            Test(
                "aaaaaaa",
                0);
        }

        [TestMethod]
        public void Test4()
        {
            Test(
                "aaaaaaaabb",
                4);
        }

        [TestMethod]
        public void Test5()
        {
            Test(
                "aaaaaaabba",
                4);
        }

        [TestMethod]
        public void Test6()
        {
            Test(
                "aaaaaaabbb",
                6);
        }

        [TestMethod]
        public void Test7()
        {
            Test(
                "aaaaaabaab",
                4);
        }

        [TestMethod]
        public void Test8()
        {
            Test(
                "baabaaaaaa",
                4);
        }

        [TestMethod]
        public void Test9()
        {
            Test(
                "aaaaabaaba",
                4);
        }

        [TestMethod]
        public void FindBugTest()
        {
            var chars = "ab";
            foreach (char a in chars)
            {
                foreach (char b in chars)
                {
                    foreach (char c in chars)
                    {
                        foreach (char d in chars)
                        {
                            foreach (char e in chars)
                            {
                                foreach (char f in chars)
                                {
                                    foreach (char g in chars)
                                    {
                                        foreach (char h in chars)
                                        {
                                            foreach (char i in chars)
                                            {
                                                foreach (char j in chars)
                                                {
                                                    var s = $"{a}{b}{c}{d}{e}{f}{g}{h}{i}{j}";
                                                    int actualResult = new Solution().solution(s);
                                                    int expectedResult = GetExpectedResult(s);
                                                    Assert.AreEqual(expectedResult, actualResult);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private int GetExpectedResult(string s)
        {
            for (int i = s.Length - 1; i >= 0; i--)
            {
                for (int j = 0; j < s.Length; j++)
                {
                    if (i + j > s.Length)
                    {
                        continue;
                    }

                    var checkString = s.Substring(j, i);
                    int aCount = checkString.Count(c => c == 'a');
                    int bCount = checkString.Count(c => c == 'b');
                    if (aCount == bCount)
                    {
                        return i;
                    }
                }
            }

            return 0;
        }

        private void Test(string s, int expectedResult)
        {
            var solution = new Solution();
            int actualResult = solution.solution(s);
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
