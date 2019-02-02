using System.Collections.Generic;
using System.Linq;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CuttingComplexity
{
    [TestClass]
    public class SolutionTests : BaseTest
    {
        [TestMethod]
        public void Test1()
        {
            Test(
                "MLMMLLM",
                3,
                1);
        }

        [TestMethod]
        public void Test2()
        {
            Test(
                "MLMMMLMMMM",
                2,
                2);
        }

        [TestMethod]
        public void Test3()
        {
            Test(
                "LLMMM",
                0,
                3);
        }

        [TestMethod]
        public void Test4()
        {
            Test(
                "LLMLMM",
                1,
                1);
        }

        [TestMethod]
        public void Test5()
        {
            Test(
                "LLLLMM",
                1,
                1);
        }

        [TestMethod]
        public void Test6()
        {
            Test(
                "MLLLLM",
                1,
                0);
        }

        [TestMethod]
        public void Test7()
        {
            Test(
                "MMMMMM",
                3,
                1);
        }

        [TestMethod]
        public void Test8()
        {
            Test(
                "MMMMMM",
                4,
                1);
        }

        [TestMethod]
        public void Test9()
        {
            Test(
                "LLLLMMMMMM",
                1,
                3);
        }

        [TestMethod]
        public void Test10()
        {
            Test(
                "LLLMMLMMMM",
                1,
                3);
        }

        [TestMethod]
        public void Test11()
        {
            Test(
                "MMLLLMMLMM",
                1,
                3);
        }

        [TestMethod]
        public void Test12()
        {
            Test(
                "M",
                1,
                0);
        }

        [TestMethod]
        public void Test13()
        {
            Test(
                "LM",
                2,
                1);
        }

        [TestMethod]
        public void Test14()
        {
            Test(
                "LM",
                1,
                0);
        }

        [TestMethod]
        public void Test15()
        {
            Test(
                "MMM",
                1,
                1);
        }

        private const int MaxLength = 10;
        private const int ExactlyOneLMaxLength = 10;

        [TestMethod]
        public void FindBugTest()
        {
            for (int length = 1; length < MaxLength; length++)
            {
                foreach (string s in GenerateStrings(length))
                {
                    for (int k = 0; k <= length; k++)
                    {
                        int expectedResult = GetExpectedResult(s, k);
                        Test(
                            s,
                            k,
                            expectedResult);
                    }
                }
            }
        }

        [TestMethod]
        public void FindBugExactlyOneLTest()
        {
            for (int length = 1; length < ExactlyOneLMaxLength; length++)
            {
                foreach (string s in GenerateStringsExactlyOneL(length))
                {
                    for (int k = 0; k <= length; k++)
                    {
                        int expectedResult = GetExpectedResult(s, k);
                        Test(
                            s,
                            k,
                            expectedResult);

                    }
                }
            }
        }

        private int GetExpectedResult(string s, int k)
        {
            int result = int.MaxValue;
            foreach (bool[] replacement in GenerateReplacements(s.Length))
            {
                if (replacement.Count(x => x) >= result)
                {
                    continue;
                }

                string replacedString = Replace(s, replacement);
                int maxSubstringLength = GetMaxSubstringLength(replacedString);
                if (maxSubstringLength == k)
                {
                    result = replacement.Count(x => x);
                }

            }

            return result;
        }

        private string Replace(string s, bool[] replacement)
        {
            string result = s;
            for (int i = 0; i < replacement.Length; i++)
            {
                if (replacement[i])
                {
                    result = new string(result
                        .Select((c, j) => j != i ? c : (c == Solution.L ? Solution.M : Solution.L)).ToArray());
                }
            }
            return result;
        }

        private int GetMaxSubstringLength(string replacedString)
        {
            var split = replacedString.Split(Solution.L);
            if (split.Length == 0)
            {
                return 0;
            }

            return split.Max(x => x.Length);
        }

        private IEnumerable<bool[]> GenerateReplacements(int length)
        {
            if (length == 1)
            {
                yield return new []{false};
                yield return new []{true};
                yield break;
            }

            foreach (bool[] subset in GenerateReplacements(length - 1))
            {
                yield return new[] { false }.Concat(subset).ToArray();
                yield return new[] { true }.Concat(subset).ToArray();
            }
        }

        private IEnumerable<string> GenerateStrings(int length)
        {
            if (length == 1)
            {
                yield return Solution.L.ToString();
                yield return Solution.M.ToString();
                yield break;
            }

            foreach (string substring in GenerateStrings(length - 1))
            {
                yield return substring + Solution.L;
                yield return substring + Solution.M;
            }
        }

        private IEnumerable<string> GenerateStringsExactlyOneL(int length)
        {
            for (int i = 0; i < length; i++)
            {
                yield return new string(Solution.M, length - 1).Insert(i, Solution.L.ToString());
            }
        }

        private void Test(string s, int k, int expectedResult)
        {
            var solution = new Solution();
            int actualResult = solution.solution(s, k);
            Assert.AreEqual(expectedResult, actualResult, $"{s} - K: {k}");
        }
    }
}
