using System;
using System.Linq;

class Solution
{
    public int solution(int[] A, int[] B, int F)
    {
        if (F > A.Length / 2)
        {
            return solution(B, A, A.Length - F);
        }

        int? fastResult = GetFastResult(A, B, F);
        if (fastResult.HasValue)
        {
            return fastResult.Value;
        }

        int result = GetResult(A, B, F);
        return result;
    }

    private int? GetFastResult(int[] a, int[] b, int f)
    {
        if (f == 0)
        {
            return b.Sum();
        }

        return null;
    }

    private int GetResult(int[] a, int[] b, int f)
    {
        var list = Enumerable.Range(0, a.Length).Select(i => new {A = a[i], B = b[i], Dif = a[i] - b[i]}).ToArray();
        Array.Sort(list, (x, y) => y.Dif - x.Dif);
        int resultA = list.Take(f).Sum(x => x.A);
        int resultB = list.Skip(f).Sum(x => x.B);
        return resultA + resultB;
    }
}
