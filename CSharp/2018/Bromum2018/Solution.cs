using System.Collections.Generic;

class Solution
{
    public int solution(int N, int Q, int[] B, int[] C)
    {
        if (Q > C.Length)
        {
            return -1;
        }

        var data = new Dictionary<int, int>[N];
        for (int i = 0; i < C.Length; i++)
        {
            var dic = data[B[i]] == null ? (data[B[i]] = new Dictionary<int, int>()) : data[B[i]];
            if (dic.ContainsKey(C[i]))
            {
                dic[C[i]]++;
            }
            else
            {
                dic[C[i]] = 1;
            }
            if (dic[C[i]] == Q)
            {
                return i;
            }
        }

        return -1;
    }
}