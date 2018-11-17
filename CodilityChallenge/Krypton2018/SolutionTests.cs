using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class SolutionTests
{
    [TestMethod]
    public void Test1()
    {
        Test(
            "[[2, 10, 1, 3], [10, 5, 4, 5], [2, 10, 2, 1], [25, 2, 5, 1]]",
            1);
    }

    [TestMethod]
    public void Test2()
    {
        Test(
            "[[10, 1, 10, 1], [1, 1, 1, 10], [10, 1, 10, 1], [1, 10, 1, 1]]",
            2);
    }

    [TestMethod]
    public void Test3()
    {
        Test(
            "[[10, 10, 10], [10, 0, 10], [10, 10, 10]]",
            1);
    }

    [TestMethod]
    public void Test4()
    {
        Test(
            "[[2, 2], [5, 5]]",
            1);
    }

    [TestMethod]
    public void TestMaximumMatrixFastCalculationWhenFirstElementIsZero()
    {
        var matrix = GetMatrix(500, 1);
        matrix[0][0] = 0;
        Test(matrix, 1);
    }

    [TestMethod]
    public void TestMaximumMatrixFastCalculationWhenLastElementIsZero()
    {
        var matrix = GetMatrix(500, 1);
        matrix[matrix.Length - 1][matrix.Length - 1] = 0;
        Test(matrix, 1);
    }

    [TestMethod]
    public void TestMaximumMatrixMaximumValues()
    {
        var matrix = GetMatrix(500, 1000000000);
        Test(matrix, 9 * (2 * 500 - 1));
    }

    [TestMethod]
    public void TestMaximumMatrixMaximumValues2()
    {
        var matrix = GetMatrix(20, 1000000000);
        matrix[matrix.Length - 1][matrix.Length - 1] = 1000000000 / 2;
        Test(matrix, 9 * (2 * 500 - 1));
    }

    private void Test(string matrix, int expectedResult)
    {
        Test(Convert(matrix), expectedResult);
    }

    private void Test(int[][] matrix, int expectedResult)
    {
        var solution = new Solution();
        int actualResult = solution.solution(matrix);
        Assert.AreEqual(expectedResult, actualResult);
    }

    private int[][] Convert(string matrix)
    {
        return matrix.Substring(1, matrix.Length - 2).Replace(" ", string.Empty)
            .Split(new[] {"]"}, StringSplitOptions.RemoveEmptyEntries)
            .Select(row => row.Trim(',', '[', ']').Split(',').Select(int.Parse).ToArray())
            .ToArray();
    }

    private int[][] GetMatrix(int size, int defaultValue)
    {
        return Enumerable.Range(0, size)
            .Select(row => Enumerable.Range(0, size).Select(c => defaultValue).ToArray())
            .ToArray();
    }
}
