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

        return groups.Max(g => g.GetBalancedLength());
    }

    private List<Group> GetGroups(string s)
    {
        const char defaultChar = '$';
        char c1 = defaultChar;
        char c2 = defaultChar;
        int start1 = -1;
        int start2 = -1;
        int last1 = -1;
        int last2 = -1;
        var groups = new List<Group>();
        for (int i = 0; i < s.Length; i++)
        {
            var c = s[i];
            if (i == 0)
            {
                c1 = c;
                start1 = i;
                last1 = i;
                continue;
            }

            if (c2 == defaultChar && c != c1)
            {
                c2 = c;
                start2 = i;
                last2 = i;
                continue;
            }

            if (c == c1)
            {
                if (last1 < last2)
                {
                    last1 = i;
                }
            }
            else if (c == c2)
            {
                if (last2 < last1)
                {
                    last2 = i;
                }
            }
            else
            {
                groups.Add(new Group(s, start1, i - start1, c1));
                if (last1 < last2)
                {
                    // ababbbbc
                    c1 = c2;
                    start1 = start2;
                    last1 = last2;
                }
                else
                {
                    // ababaaac
                    start1 = last1;
                }

                c2 = c;
                start2 = i;
                last2 = i;
            }
        }

        if (c2 != defaultChar)
        {
            groups.Add(new Group(s, start1, s.Length - start1, c1));
        }

        return groups;
    }
}

public partial class Group
{
    private readonly string _s;
    private readonly int _startIndex;
    private readonly int _length;
    private readonly char _c1;

    public Group(string s, int startIndex, int length, char c1)
    {
        _s = s;
        _startIndex = startIndex;
        _length = length;
        _c1 = c1;
    }

    public int GetBalancedLength()
    {
        var max1 = GetBalancedLength(_s.Skip(_startIndex).Take(_length));
        if (max1 / 2 < _length / 2)
        {
            var max2 = GetBalancedLength(_s.Skip(_startIndex).Take(_length).Reverse());
            return Math.Max(max1, max2);
        }

        return max1;
    }

    private int GetBalancedLength(IEnumerable<char> chars)
    {
        var list = new LinkedList<Node>(chars.Select(c => new Node(c == _c1 ? 1 : -1, 0)));
        LinkedListNode<Node> current = list.First;
        while (current != null && current.Next != null)
        {
            bool joined = Join2Nodes(current);
            if (!joined)
            {
                joined = Join3Nodes(current);
            }

            if (joined)
            {
                if (current.Previous != null)
                {
                    current = current.Previous;
                }

                continue;
            }

            current = current.Next;
        }

        return list.Max(i => i.Length);
    }

    private bool Join2Nodes(LinkedListNode<Node> current)
    {
        if (current.Value.Weight * current.Next.Value.Weight < 0)
        {
            int absCurrent = Math.Abs(current.Value.Weight);
            int absNext = Math.Abs(current.Next.Value.Weight);
            if (absCurrent < absNext)
            {
                var newCurrent = new Node(0, 2 * Math.Min(absCurrent, absNext));
                var newNext = new Node(current.Value.Weight + current.Next.Value.Weight, 0);
                current.Value = newCurrent;
                current.Next.Value = newNext;
            }
            else if (absCurrent > absNext)
            {
                var newCurrent = new Node(current.Value.Weight + current.Next.Value.Weight, 0);
                var newNext = new Node(0, 2 * Math.Min(absCurrent, absNext));
                current.Value = newCurrent;
                current.Next.Value = newNext;
            }
            else
            {
                current.Value = new Node(0, absCurrent + absNext);
                current.List.Remove(current.Next);
            }

            return true;
        }

        if (current.Value.Weight * current.Next.Value.Weight > 0)
        {
            current.Value = new Node(current.Value.Weight + current.Next.Value.Weight, 0);
            current.List.Remove(current.Next);
            return true;
        }

        if (current.Value.Length > 0 && current.Next.Value.Length > 0)
        {
            current.Value = new Node(0, current.Value.Length + current.Next.Value.Length);
            current.List.Remove(current.Next);
            return true;
        }

        return false;
    }

    private bool Join3Nodes(LinkedListNode<Node> current)
    {
        if (current.Next.Next == null)
        {
            return false;
        }

        if (current.Value.Length > 0)
        {
            return false;
        }

        if (current.Value.Weight * current.Next.Next.Value.Weight > 0)
        {
            return false;
        }

        int absCurrent = Math.Abs(current.Value.Weight);
        int absNext = Math.Abs(current.Next.Next.Value.Weight);
        if (absCurrent < absNext)
        {
            var newCurrent = new Node(0, current.Next.Value.Length + 2 * absCurrent);
            var newNext = new Node(current.Value.Weight + current.Next.Next.Value.Weight, 0);
            current.Value = newCurrent;
            current.Next.Value = newNext;
            current.List.Remove(current.Next.Next);
        }
        else if (absCurrent > absNext)
        {
            var newCurrent = new Node(current.Value.Weight + current.Next.Next.Value.Weight, 0);
            var newNext = new Node(0, current.Next.Value.Length + 2 * absNext);
            current.Value = newCurrent;
            current.Next.Value = newNext;
            current.List.Remove(current.Next.Next);
        }
        else
        {
            var newCurrent = new Node(0, current.Next.Value.Length + absCurrent + absNext);
            current.Value = newCurrent;
            current.List.Remove(current.Next.Next);
            current.List.Remove(current.Next);
        }

        return true;
    }
}

public partial class Node
{
    public int Weight { get; set; }
    public int Length { get; set; }

    public Node(int weight, int length)
    {
        Weight = weight;
        Length = length;
    }
}
