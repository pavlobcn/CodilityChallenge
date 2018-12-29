using System;
using System.Collections.Generic;
using Common;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gallium2018
{
    [TestClass]
    public class SolutionTests : BaseTest
    {
        [TestMethod]
        public void Test1()
        {
            Test(
                "[7, 15, 6, 20, 5, 10]",
                3);
        }

        [TestMethod]
        public void Test2()
        {
            Test(
                "[25, 10, 25, 10, 32]",
                4);
        }

        [TestMethod]
        public void HypothesisTest()
        {
            var random = new Random(5);
            for (int i = 0; i < 100; i++)
            {
                Number[] a = Enumerable.Range(0, 10)
                    .Select(_ => new Number(random.Next(10), random.Next(10)))
                    .ToArray();
                CheckHypothesis(a);
            }
        }

        private void CheckHypothesis(Number[] a)
        {
            int maxSum = a.Max(x => x.A + x.B);
            var combinations = new List<Combination>();
            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < a.Length; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    for (int k = 0; k < a.Length; k++)
                    {
                        if (k == i || k == j)
                        {
                            continue;
                        }

                        combinations.Add(new Combination(a[i], a[j], a[k]));
                    }
                }
            }
            int max = combinations.Select(item => item.Production).Max();
            bool cond = false;
            foreach (var combination in combinations.Where(c => c.Production == max))
            {
                cond |= new[]{combination.X, combination.Y, combination.Z}.Any(i => i.A + i.B == maxSum);
            }
            Assert.IsTrue(cond);
        }

        public class Number
        {
            public int A { get; set; }
            public int B { get; set; }

            public Number(int a, int b)
            {
                A = a;
                B = b;
            }
        }

        private class Combination
        {
            public Number X { get; set; }
            public Number Y { get; set; }
            public Number Z { get; set; }
            public int Production { get; set; }

            public Combination(Number x, Number y, Number z)
            {
                X = x;
                Y = y;
                Z = z;
                Production = Math.Min(x.A + y.A + z.A, x.B + y.B + z.B);
            }
        }

        private int GetPow(long x, int @base)
        {
            int result = 0;
            while (x % @base == 0)
            {
                result++;
                x = x / @base;
            }
            return result;
        }

        private int GetTrailingZeroCount(long x)
        {
            return GetPow(x, 10);
        }

        private int GetPow2(int x)
        {
            return GetPow(x, 2);
        }

        private int GetPow5(int x)
        {
            return GetPow(x, 5);
        }

        private void Test(string a, int expectedResult)
        {
            TestInternal(ConvertArray(a), expectedResult);
        }

        private void TestInternal(int[] a, int expectedResult)
        {
            var solution = new Solution();
            int actualResult = solution.solution(a);
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
