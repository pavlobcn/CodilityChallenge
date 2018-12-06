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
            string originalResult = solution.solution(s);
            string stringResult = originalResult;
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

            string[] initialWords = s.Split(Solution.Space);
            string[] resultWords = originalResult.Split(Solution.Space).Distinct().ToArray();
            CollectionAssert.IsSubsetOf(resultWords, initialWords);
        }
    }
}