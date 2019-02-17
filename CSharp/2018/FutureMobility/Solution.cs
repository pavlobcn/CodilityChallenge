using System;
using System.Linq;

class Solution
{
    private const long BaseMod = 1000000007;

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
            result += Math.Abs(a);
            long c = A[i] - B[i];

            if (a > 0)
            {
                if (b >= 0)
                {
                    c += a;

                    a = b;
                    b = c;
                }
                else
                {
                    if (a + b > 0)
                    {
                        b += a + c;
                        a = 0;
                    }
                    else
                    {
                        a += b;
                        b = c;
                    }
                }
            }
            else
            {
                if (b <= 0)
                {
                    c += a;

                    a = b;
                    b = c;
                }
                else
                {
                    if (a + b < 0)
                    {
                        b += a + c;
                        a = 0;
                    }
                    else
                    {
                        a += b;
                        b = c;
                    }
                }
            }

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