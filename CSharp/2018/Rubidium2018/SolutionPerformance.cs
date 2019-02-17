using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rubidium2018
{
    [TestClass]
    public class SolutionPerformanceTests : SolutionTestsBase
    {
        private static readonly Tuple<int[],int[]> GridArray;
        private const int MaxN = 100000;

        static SolutionPerformanceTests()
        {
            GridArray = GetGridArray();
        }

        private static Tuple<int[],int[]> GetGridArray()
        {
            var xArray = new List<int>();
            var yArray = new List<int>();
            var baseArray = Enumerable.Range(0, MaxN / 300).Select(i => i * 2).ToArray();
            foreach (int i in baseArray)
            {
                foreach (int j in baseArray)
                {
                    xArray.Add(i);
                    yArray.Add(j);
                }
            }
            return new Tuple<int[], int[]>(xArray.ToArray(), yArray.ToArray());
        }

        [TestMethod]
        public void GridTest()
        {
            TestInternal(
                GridArray.Item1,
                GridArray.Item2,
                1);
        }
    }
}
