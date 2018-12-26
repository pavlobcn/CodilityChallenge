using System.Collections.Generic;
using System.Linq;
using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Germanium2018
{
    [TestClass]
    public class SolutionTests : BaseTest
    {
        [TestMethod]
        public void Test1()
        {
            Test(
                "[1, 2, 4, 3]",
                "[1, 3, 2, 3]",
                5);
        }
        [TestMethod]
        public void Test2()
        {
            Test(
                "[4, 2, 1, 6, 5]",
                "[3, 2, 1, 7, 7]",
                4);
        }

        [TestMethod]
        public void Test3()
        {
            Test(
                "[2, 3]",
                "[2, 3]",
                1);
        }

        [TestMethod]
        public void Test4()
        {
            Test(
                "[1, 1, 2, 3]",
                "[1, 1, 3, 4]",
                4);
        }

        [TestMethod]
        public void FindBugTest()
        {
            foreach (int[] arrays in GetAllArrays(4, 5))
            {
                int[] a = arrays.Take(arrays.Length / 2).ToArray();
                int[] b = arrays.Skip(arrays.Length / 2).ToArray();
                int expectedResult = GetExpectedResult(a, b);
                int actualResult = new Solution().solution(a, b);
                Assert.AreEqual(expectedResult, actualResult);
            }
        }

        private IEnumerable<int[]> GetAllArrays(int length, int maxValue)
        {
            if (length == 0)
            {
                yield return new int[0];
                yield break;
            }

            var baseArrays = GetAllArrays(length - 1, maxValue);
            foreach (int[] baseArray in baseArrays)
            {

                for (int i = 0; i < maxValue; i++)
                {
                    for (int j = 0; j < maxValue; j++)
                    {
                        yield return baseArray.Concat(new[] {i + 1, j + 1}).ToArray();
                    }
                }
            }
        }

        private int GetExpectedResult(int[] a, int[] b)
        {
            return GetValues(a, b).Max(GetMaxNotPresent);
        }

        private int GetMaxNotPresent(List<int> values)
        {
            var marks = new bool[values.Count];
            foreach (int value in values)
            {
                if (value <= marks.Length)
                {
                    marks[value - 1] = true;
                }
            }

            for (int i = 0; i < marks.Length; i++)
            {
                if (!marks[i])
                {
                    return i + 1;
                }
            }
            return values.Count + 1;
        }

        private IEnumerable<List<int>> GetValues(int[] a, int[] b)
        {
            if (a.Length == 0)
            {
                yield return new List<int>();
                yield break;
            }

            var baseLists = GetValues(a.Skip(1).ToArray(), b.Skip(1).ToArray()).ToList();
            foreach (List<int> baseList in baseLists)
            {
                yield return new[] {a[0]}.Concat(baseList).ToList();
                yield return new[] {b[0]}.Concat(baseList).ToList();
            }
        }

        private void Test(string a, string b, int expectedResult)
        {
            TestInternal(ConvertArray(a), ConvertArray(b), expectedResult);
        }

        private void TestInternal(int[] a, int[] b, int expectedResult)
        {
            var solution = new Solution();
            int actualResult = solution.solution(a, b);
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
