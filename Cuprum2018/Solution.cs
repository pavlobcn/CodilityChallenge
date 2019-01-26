using System;
using System.Collections.Generic;
using System.Linq;

class Solution
{
    public int solution(string S)
    {
        ulong[] states = GetStates(S);
        IList<List<int>> groups = GetGroups(states);
        int result = groups.Max(GetGroupMaximum);
        return result;
    }

    private ulong[] GetStates(string s)
    {
        var states = new ulong[s.Length + 1];
        for (int i = 0; i < s.Length; i++)
        {
            states[i + 1] = states[i] ^ GetStateDif(s[i]);
        }
        return states;
    }

    private ulong GetStateDif(char c)
    {
        if (c >= 'a' && c <= 'z')
        {
            return 1UL << (c - 'a');
        }
        return (1UL << 26) << (c - '0');
    }

    private IList<List<int>> GetGroups(ulong[] states)
    {
        var positions = new Tuple<ulong, int>[states.Length];
        for (int i = 0; i < states.Length; i++)
        {
            positions[i] = new Tuple<ulong, int>(states[i], i);
        }

        return positions.GroupBy(x => x.Item1, x => x.Item2).Select(x => x.ToList()).ToList();
    }

    private int GetGroupMaximum(IList<int> positions)
    {
        if (positions.Count == 1)
        {
            return 0;
        }

        int min = positions.Min();
        int max = positions.Max();
        return max - min;
    }
}
