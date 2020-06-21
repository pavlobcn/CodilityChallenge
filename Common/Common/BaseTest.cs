using System;
using System.Linq;

namespace Common
{
    public class BaseTest
    {
        protected int[] ConvertArray(string array)
        {
            return array.Substring(1, array.Length - 2).Split(',').Select(int.Parse).ToArray();
        }

        protected int[][] ConvertMatrix(string matrix)
        {
            return matrix.Substring(1, matrix.Length - 2).Replace(" ", string.Empty)
                .Split(new[] { "]" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(row => row.Trim(',', '[', ']').Split(',').Select(int.Parse).ToArray())
                .ToArray();
        }
    }
}
