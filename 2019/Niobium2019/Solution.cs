using System;
using System.Collections.Generic;
using System.Linq;

class Solution
{
    public int solution(int[][] A)
    {
        var lines = new List<string>();
        for (int i = 0; i < A.Length; i++)
        {
            int[] array = A[i];
            var charArray = new char[array.Length - 1];
            for (int j = 1; j < array.Length; j++)
            {
                charArray[j - 1] = array[j] == array[j - 1] ? '0' : '1';
            }
            lines.Add(new string(charArray));
        }
        return lines.GroupBy(x => x).OrderByDescending(g => g.Count()).First().Count();
    }
}
