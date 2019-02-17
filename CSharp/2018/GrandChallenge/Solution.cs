using System;
using System.Collections.Generic;
using System.Linq;

class Solution
{
    public int solution(string S)
    {
        List<Group> groups = GetGroups(S);
        if (groups.Count == 0)
        {
            return 0;
        }

        int maxLength = GetMaxBalancedLength(groups);
        return maxLength;
    }

    private int GetMaxBalancedLength(List<Group> groups)
    {
        int maxLength = 0;
        groups.Sort((g1, g2) => g1.Length / 2 - g2.Length / 2);
        for (int i = groups.Count - 1; i >= 0; i--)
        {
            Group group = groups[i];
            if (maxLength / 2 >= group.Length / 2)
            {
                break;
            }

            int newMaxLength = group.GetBalancedLength();
            maxLength = Math.Max(maxLength, newMaxLength);
        }
        return maxLength;
    }

    private List<Group> GetGroups(string s)
    {
        const char defaultChar = '$';
        char c1 = defaultChar;
        char c2 = defaultChar;
        int start = -1;
        int start1 = -1;
        int start2 = -1;
        var groups = new List<Group>();
        for (int i = 0; i < s.Length; i++)
        {
            var c = s[i];
            if (i == 0)
            {
                c1 = c;
                start1 = i;
                start = start1;
                continue;
            }

            if (c2 == defaultChar && c != c1)
            {
                c2 = c;
                start2 = i;
                continue;
            }

            if (c == c1)
            {
                if (start1 < start2)
                {
                    start1 = i;
                }
            }
            else if (c == c2)
            {
                if (start2 < start1)
                {
                    start2 = i;
                }
            }
            else
            {
                groups.Add(new Group(s, start, i - start, c1));
                if (start1 < start2)
                {
                    // ababbbbc
                    c1 = c2;
                    start1 = start2;
                }
                else
                {
                    // ababaaac
                }

                c2 = c;
                start2 = i;
                start = start1;
            }
        }

        if (c2 != defaultChar)
        {
            groups.Add(new Group(s, start, s.Length - start, c1));
        }

        return groups;
    }
}

public partial class Group
{
    private readonly string _s;
    private readonly int _startIndex;
    private readonly char _c1;

    public Group(string s, int startIndex, int length, char c1)
    {
        _s = s;
        _startIndex = startIndex;
        Length = length;
        _c1 = c1;
    }

    public int Length { get; }

    public int GetBalancedLength()
    {
        return new BalancedLength(_c1).Solution(_s.Skip(_startIndex).Take(Length).ToArray());
    }
}

public class BalancedLength : MaxLengthOfSubArray<char, int>
{
    private readonly char _c;

    public BalancedLength(char c)
    {
        _c = c;
    }

    protected override int GetStateDif(int prevState, char element)
    {
        return prevState + (element == _c ? 1 : -1);
    }
}

public abstract class MaxLengthOfSubArray<TElement, TState>
{
    public int Solution(TElement[] array)
    {
        TState[] states = GetStates(array);
        IList<List<int>> groups = GetGroups(states);
        int result = groups.Max(GetGroupMaximum);
        return result;
    }

    private TState[] GetStates(TElement[] array)
    {
        var states = new TState[array.Length + 1];
        for (int i = 0; i < array.Length; i++)
        {
            states[i + 1] = GetStateDif(states[i], array[i]);
        }
        return states;
    }

    protected abstract TState GetStateDif(TState prevState, TElement element);

    private IList<List<int>> GetGroups(TState[] states)
    {
        var positions = new Tuple<TState, int>[states.Length];
        for (int i = 0; i < states.Length; i++)
        {
            positions[i] = new Tuple<TState, int>(states[i], i);
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