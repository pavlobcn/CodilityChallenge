using System;
using System.Linq;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Arsenicum2018
{
    public class SolutionTestsBase : BaseTest
    {
        protected void Test(string s, bool isPalindromExpected)
        {
            var solution = new Solution();
            string stringResult = solution.solution(s);
            if (stringResult == "NO")
            {
                Assert.IsFalse(isPalindromExpected);
                return;
            }

            bool actualResultIsPalindrom = stringResult.IsPalindrom();
            if (!actualResultIsPalindrom)
            {
                Assert.Fail("Returned value is not a palindrom.");
            }

            Assert.IsTrue(isPalindromExpected);

            string[] initialWords = s.Split(Solution.Space);
            string[] resultWords = stringResult.Split(Solution.Space).Distinct().ToArray();
            CollectionAssert.IsSubsetOf(resultWords, initialWords);
        }
    }
}