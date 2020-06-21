using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GrandChallenge
{
    [TestClass]
    public class SolutionTests : SolutionTestsBase
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
        public void Test10()
        {
            Test(
                "aaaabaabcb",
                4);
        }

        [TestMethod]
        public void FindBugTest()
        {
            FindBugTest(String.Empty, 10);
        }

        private void FindBugTest(string s, int additionalLength)
        {
            if (additionalLength == 0)
            {
                int actualResult = new Solution().solution(s);
                int expectedResult = GetExpectedResult(s);
                Assert.AreEqual(expectedResult, actualResult);
            }
            else
            {
                const string chars = "abc";
                foreach (char c in chars)
                {
                    FindBugTest(s + c, additionalLength - 1);
                }
            }
        }

        private int GetExpectedResult(string s)
        {
            for (int i = s.Length; i > 0; i--)
            {
                for (int j = 0; j < s.Length; j++)
                {
                    if (i + j > s.Length)
                    {
                        continue;
                    }

                    if (i % 2 != 0)
                    {
                        continue;
                    }

                    var checkString = s.Substring(j, i);
                    if (checkString.Distinct().Count() != 2)
                    {
                        continue;
                    }

                    int aCount = checkString.Count(c => c == checkString[0]);
                    int bCount = checkString.Count(c => c != checkString[0]);
                    if (aCount == bCount)
                    {
                        return i;
                    }
                }
            }

            return 0;
        }
    }
}
