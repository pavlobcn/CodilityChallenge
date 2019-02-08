using System;
using System.Collections.Generic;
using System.Linq;

class Solution
{
    private const int NoSolution = -1;

    public int solution(int[] A, int[] B)
    {
        List<int> groups = GetGroups(A, B);
        if (groups == null)
        {
            return NoSolution;
        }

        int result = groups.Sum();
        return result;
    }

    private List<int> GetGroups(int[] a, int[] b)
    {
        var list = new List<int>();
        int groupLength = 0;
        int changes = 0;
        for (int i = 0; i <= a.Length; i++)
        {
            if (groupLength == 0)
            {
                groupLength++;
                continue;
            }

            bool continueWithoutChange = i == a.Length || a[i - 1] < a[i] && b[i - 1] < b[i];
            bool continueWithChange = i == a.Length || a[i - 1] < b[i] && b[i - 1] < a[i];
            if (!continueWithoutChange && !continueWithChange)
            {
                return null;
            }

            if (continueWithoutChange && continueWithChange)
            {
                list.Add(Math.Min(changes, groupLength - changes));
                groupLength = 1;
                changes = 0;
                continue;
            }

            if (continueWithChange)
            {
                changes++;
            }

            groupLength++;
        }

        return list;
    }
}