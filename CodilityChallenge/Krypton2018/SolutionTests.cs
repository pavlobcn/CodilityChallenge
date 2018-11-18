using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class SolutionTests
{
    private const int MaxMatrixSize = 500;
    private const int MaxCellValue = 1000000000;
    private static readonly int MaxCellValueTrailingZero = (int) Math.Log10(MaxCellValue);

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
    public void MaximumMatrixFastCalculationWhenFirstElementIsZeroTest()
    {
        var matrix = GetMatrix(MaxMatrixSize, 1);
        matrix[0][0] = 0;
        Test(matrix, 1);
    }

    [TestMethod]
    public void MaximumMatrixFastCalculationWhenLastElementIsZeroTest()
    {
        var matrix = GetMatrix(MaxMatrixSize, 1);
        matrix[matrix.Length - 1][matrix.Length - 1] = 0;
        Test(matrix, 1);
    }

    [TestMethod]
    public void MaximumMatrixMaximumValuesTest()
    {
        var matrix = GetMatrix(MaxMatrixSize, MaxCellValue);
        Test(matrix, MaxCellValueTrailingZero * (2 * MaxMatrixSize - 1));
    }

    [TestMethod]
    public void MaximumMatrixMaximumValuesExceptLastValueTest()
    {
        var matrix = GetMatrix(MaxMatrixSize, MaxCellValue);
        matrix[matrix.Length - 1][matrix.Length - 1] = MaxCellValue / 2;
        Test(matrix, MaxCellValueTrailingZero * (2 * MaxMatrixSize - 1) - 1);
    }

    [TestMethod]
    public void MaximumMatrixEqualValuesTest1()
    {
        var matrix = GetMatrix(MaxMatrixSize, 20);
        Test(matrix, 2 * MaxMatrixSize - 1);
    }

    [TestMethod]
    public void MaximumMatrixEqualValuesTest2()
    {
        var matrix = GetMatrix(MaxMatrixSize, 50);
        Test(matrix, 2 * MaxMatrixSize - 1);
    }

    [TestMethod]
    [Timeout(1000)]
    public void MaximumMatrixRandomValuesPerfomanceTest()
    {
        var random = new Random(5);
        var matrix = Enumerable.Range(0, MaxMatrixSize)
            .Select(row => Enumerable.Range(0, MaxMatrixSize).Select(c =>
                (int) Math.Pow(2, random.Next(MaxCellValueTrailingZero + 1)) *
                (int) Math.Pow(5, random.Next(MaxCellValueTrailingZero + 1))).ToArray())
            .ToArray();

        var solution = new Solution();
        solution.solution(matrix);
        // Do not assert expected and actual value because actual value can vary randomly
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
