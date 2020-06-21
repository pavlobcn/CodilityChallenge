using System;
using System.Collections.Generic;

class Solution
{
    private const int Dimension = 28;

    public int solution(int[] A)
    {
        int[,] matrix = GetNumbers(A);
        int fastResult = GetFastResult(matrix);
        if (fastResult >= 0)
        {
            return fastResult;
        }

        Minimize(matrix);

        int result = GetResult(matrix);
        return result;
    }

    private void Minimize(int[,] matrix)
    {
        for (int i = 0; i < Dimension; i++)
        {
            int total = 0;
            for (int j = Dimension - 1; j >= 0; j--)
            {
                if (total >= 3)
                {
                    matrix[i, j] = 0;
                    continue;
                }

                matrix[i, j] = Math.Min(matrix[i, j], 3 - total);
                total += matrix[i, j];
            }
        }

        for (int i = 0; i < matrix.GetLength(1); i++)
        {
            int total = 0;
            for (int j = matrix.GetLength(0) - 1; j >= 0; j--)
            {
                if (total >= 3)
                {
                    matrix[j, i] = 0;
                    continue;
                }

                matrix[j, i] = Math.Min(matrix[j, i], 3 - total);
                total += matrix[j, i];
            }
        }
    }

    private int GetResult(int[,] matrix)
    {
        List<Number> numbers = GetNumbers(matrix);
        int result = -1;
        for (int i = 0; i < numbers.Count; i++)
        {
            for (int j = i + 1; j < numbers.Count; j++)
            {
                for (int k = j + 1; k < numbers.Count; k++)
                {
                    int newResult = Math.Min(numbers[i].Pow2 + numbers[j].Pow2 + numbers[k].Pow2,
                        numbers[i].Pow5 + numbers[j].Pow5 + numbers[k].Pow5);
                    if (newResult > result)
                    {
                        result = newResult;
                    }
                }
            }
        }

        return result;
    }

    private List<Number> GetNumbers(int[,] matrix)
    {
        var numbers = new List<Number>();
        for (int i = 0; i < Dimension; i++)
        {
            for (int j = 0; j < Dimension; j++)
            {
                for (int k = 0; k < matrix[i, j]; k++)
                {
                    numbers.Add(new Number(i, j));
                }
            }
        }
        return numbers;
    }

    private int GetFastResult(int[,] matrix)
    {
        int fastResultForMaximum = GetFastResultForMaximum(matrix);
        if (fastResultForMaximum >= 0)
        {
            return fastResultForMaximum;
        }

        int fastResultForIdenticalNumbers = GetFastResultForIdenticalNumbers(matrix);
        if (fastResultForIdenticalNumbers >= 0)
        {
            return fastResultForIdenticalNumbers;
        }

        int fastResultForZero = GetFastResultForZero(matrix);
        if (fastResultForIdenticalNumbers >= 0)
        {
            return fastResultForZero;
        }

        return -1;
    }

    private int GetFastResultForMaximum(int[,] matrix)
    {
        if (matrix[Dimension - 1, Dimension - 1] >= 3)
        {
            return 3 * (Dimension - 1);
        }

        return -1;
    }

    private int GetFastResultForZero(int[,] matrix)
    {
        for (int i = 1; i < Dimension; i++)
        {
            for (int j = 1; j < Dimension; j++)
            {
                if (matrix[i, j] > 0)
                {
                    return -1;
                }
            }
        }

        return 0;
    }

    private static int GetFastResultForIdenticalNumbers(int[,] matrix)
    {
        int pow2 = -1;
        int pow5 = -1;
        for (int i = 0; i < Dimension; i++)
        {
            for (int j = 0; j < Dimension; j++)
            {
                if (matrix[i, j] == 0)
                {
                    continue;
                }

                if (pow2 == -1)
                {
                    pow2 = i;
                    pow5 = j;
                }
                else
                {
                    return -1;
                }
            }
        }

        return 3 * Math.Min(pow2, pow5);
    }

    private int[,] GetNumbers(int[] a)
    {
        var matrix = new int[Dimension, Dimension];
        foreach (var i in a)
        {
            int pow2 = Math.Min(GetPow2(i), Dimension - 1);
            int pow5 = Math.Min(GetPow5(i), Dimension - 1);
            matrix[pow2, pow5]++;
        }

        return matrix;
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

    private int GetPow2(int x)
    {
        return GetPow(x, 2);
    }

    private int GetPow5(int x)
    {
        return GetPow(x, 5);
    }

    public class Number
    {
        public int Pow2 { get; set; }
        public int Pow5 { get; set; }

        public Number(int pow2, int pow5)
        {
            Pow2 = pow2;
            Pow5 = pow5;
        }
    }
}