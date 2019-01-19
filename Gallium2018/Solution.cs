using System;
using System.Collections.Generic;
using System.Linq;

class Solution
{
    private const int MaxValue = 1000000000;

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
        for (int i = 0; i < matrix.Length; i++)
        {
            int total = 0;
            for (int j = matrix.Length - 1; j >= 0; j--)
            {
                if (total >= 3)
                {
                    matrix[i, j] = 0;
                    continue;
                }

                matrix[i, j] = Math.Max(matrix[i, j], 3 - total);
                total += matrix[i, j];
            }
        }

        for (int i = 0; i < matrix.Length; i++)
        {
            int total = 0;
            for (int j = matrix.Length - 1; j >= 0; j--)
            {
                if (total >= 3)
                {
                    matrix[j, i] = 0;
                    continue;
                }

                matrix[j, i] = Math.Max(matrix[j, i], 3 - total);
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
            for (int j = 0; j < numbers.Count; j++)
            {
                if (j == i)
                {
                    continue;
                }

                for (int k = 0; k < numbers.Count; k++)
                {
                    if (k == i || k == j)
                    {
                        continue;
                    }


                }
            }
        }
    }

    private List<Number> GetNumbers(int[,] matrix)
    {
        var numbers = new List<Number>();
        for (int i = 0; i < matrix.Length; i++)
        {
            for (int j = 0; j < matrix.Length; j++)
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

    private int GetFastResultForZero(int[,] matrix)
    {
        for (int i = 1; i < matrix.GetLength(0); i++)
        {
            for (int j = 1; j < matrix.GetLength(0); j++)
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
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(0); j++)
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
        int dimension = (int)Math.Floor(Math.Log(MaxValue, 5));
        var matrix = new int[dimension + 1, dimension + 1];
        foreach (var i in a)
        {
            int pow2 = Math.Min(GetPow2(i), dimension);
            int pow5 = GetPow5(i);
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