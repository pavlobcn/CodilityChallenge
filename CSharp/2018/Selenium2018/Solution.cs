using System;
using System.Linq;

class Solution
{
    private const int ModuleHandle = 1000000007;

    public int solution(int[] X, int[] Y)
    {
        return new[] {X, Y}.Sum(CountMovements) % ModuleHandle;
    }

    private int CountMovements(int[] array)
    {
        Array.Sort(array);
        int result = 0;
        for (int i = 0; i < array.Length; i++)
        {
            int current = array[i] - 1;
            result = (Math.Abs(current - i) + result) % ModuleHandle;
        }
        return result;
    }
}