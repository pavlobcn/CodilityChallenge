using System;
using System.Linq;

class Solution
{
    private const int BaseMod = 1000000007;

    public int solution(int[] A, int[] B)
    {
        if (!SumsAreEquals(A, B))
        {
            return -1;
        }

        if (A.Length == 1)
        {
            return 0;
        }

        long a = A[0] - B[0];
        long b = A[1] - B[1];
        int i = 2;
        long result = 0;

        while (i < A.Length)
        {
            long c = A[i] - B[i];

            if (a * b < 0)
            {
                long resultAdd = Math.Min(Math.Abs(a), Math.Abs(b));
                result += resultAdd;
                if (Math.Abs(a) > Math.Abs(b))
                {
                    a += b;
                    b = 0;
                }
                else
                {
                    b += a;
                    a = 0;
                }
            }
            result += Math.Abs(a);
            c += a;
            a = b;
            b = c;

            i++;
        }

        result += Math.Abs(a);

        return (int)(result % BaseMod);
    }

    private bool SumsAreEquals(int[] a, int[] b)
    {
        return a.Select(x => (long) x).Sum() == b.Select(x => (long) x).Sum();
    }
}