using System.Collections.Generic;
using System.Linq;

class Solution
{
    private const long Mod = 1000000007;

    public int solution(int[] A)
    {
        if (A.Length < 3)
        {
            return 0;
        }
        int[] distinctArray;
        int[] distinctArrayReversed;
        GetDistinctArrays(A, out distinctArray, out distinctArrayReversed);
        List<PositionInfo>[] positionInfos = GetPositionInfo(A, distinctArray, distinctArrayReversed);
        long result = GetResult(positionInfos);
        return (int)(result % Mod);
    }

    private long GetResult(List<PositionInfo>[] positionInfos)
    {
        return positionInfos.Where(x => x != null).Sum(x => GetJoinedResult(x));
    }

    private long GetJoinedResult(List<PositionInfo> list)
    {
        var seed = new { PreviosValue = 0L, Total = 0L };
        return list.Aggregate(seed, (a, i) => new { PreviosValue = (long)i.DistinctCount, Total = (a.Total + (i.DistinctCount - a.PreviosValue) * i.DistinctReversedCount) % Mod }).Total;
    }

    private List<PositionInfo>[] GetPositionInfo(int[] a, int[] distinctArray, int[] distinctArrayReversed)
    {
        var positionInfos = new List<PositionInfo>[distinctArray.Length];
        for (int i = 1; i < a.Length - 1; i++)
        {
            var list = positionInfos[a[i] - 1];
            var positionInfo = new PositionInfo(distinctArray[i - 1], distinctArrayReversed[i + 1]);
            if (list == null)
            {
                positionInfos[a[i] - 1] = new List<PositionInfo> { positionInfo };
            }
            else
            {
                list.Add(positionInfo);
            }
        }
        return positionInfos;
    }

    private void GetDistinctArrays(int[] a, out int[] distinctArray, out int[] distinctArrayReversed)
    {
        var marks = new bool[a.Length];
        distinctArray = new int[a.Length];
        for (int i = 0; i < a.Length; i++)
        {
            bool exists = marks[a[i] - 1];
            int prevValue = i == 0 ? 0 : distinctArray[i - 1];
            if (exists)
            {
                distinctArray[i] = prevValue;
            }
            else
            {
                distinctArray[i] = prevValue + 1;
                marks[a[i] - 1] = true;
            }
        }

        marks = new bool[a.Length];
        distinctArrayReversed = new int[a.Length];
        for (int i = a.Length - 1; i >= 0; i--)
        {
            bool exists = marks[a[i] - 1];
            int prevValue = i == a.Length - 1 ? 0 : distinctArrayReversed[i + 1];
            if (exists)
            {
                distinctArrayReversed[i] = prevValue;
            }
            else
            {
                distinctArrayReversed[i] = prevValue + 1;
                marks[a[i] - 1] = true;
            }
        }
    }

    private class PositionInfo
    {
        public int DistinctCount { get; }
        public int DistinctReversedCount { get; }

        public PositionInfo(int distinctCount, int distinctReversedCount)
        {
            DistinctCount = distinctCount;
            DistinctReversedCount = distinctReversedCount;
        }
    }
}
