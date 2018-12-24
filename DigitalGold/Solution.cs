using System;

class Solution
{
    public int solution(int N, int M, int[] X, int[] Y)
    {
        bool fastCheckForZero = FastCheckForZero(X, Y);
        if (fastCheckForZero)
        {
            return 0;
        }

        int result = GetSingleResult(X) + GetSingleResult(Y);
        return result;
    }

    private int GetSingleResult(int[] a)
    {
        Array.Sort(a);
        int left = a[a.Length / 2 - 1];
        int right = a[a.Length / 2];
        return right - left;
    }

    private bool FastCheckForZero(int[] x, int[] y)
    {
        return x.Length % 2 != 0;
    }
}