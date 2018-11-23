using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class SolutionTests
{
    private const int MaxN = 100000;
    private const int MaxValue = 1000000000;
    private const int BaseMod = 1000000007;

    [TestMethod]
    public void Test1()
    {
        Test(
            "[1, 1, 2, 4, 3]",
            "[2, 2, 2, 3, 2]",
            3);
    }

    [TestMethod]
    public void Test2()
    {
        Test(
            "[0, 0, 2, 1, 8, 8, 2, 0]",
            "[8, 5, 2, 4, 0, 0, 0, 2]",
            31);
    }

    [TestMethod]
    public void Test3()
    {
        Test(
            "[1000000000, 1000000000, 1000000000, 0, 0, 0]",
            "[0, 0, 0, 1000000000, 1000000000, 1000000000]",
            999999972);
    }

    [TestMethod]
    public void Test4()
    {
        Test(
            "[2]",
            "[1]",
            -1);
    }

    private void Test(string A, string B, int expectedResult)
    {
        Test(Convert(A), Convert(B), expectedResult);
    }

    private void Test(int[] A, int[] B, int expectedResult)
    {
        var solution = new Solution();
        int actualResult = solution.solution(A, B);
        Assert.AreEqual(expectedResult, actualResult);
    }

    private int[] Convert(string array)
    {
        return array.Substring(1, array.Length - 2).Split(',').Select(int.Parse).ToArray();
    }
}
