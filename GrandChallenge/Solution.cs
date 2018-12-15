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

public class Point
{
    public int Value { get; set; }
    public int Index { get; set; }

    public Point(int value, int index)
    {
        Value = value;
        Index = index;
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
        List<Point> points = GetPoints();
        points.Sort((p1, p2) => p1.Value - p2.Value);
        Point previousPoint = points[0];
        int minIndex = previousPoint.Index;
        int maxIndex = previousPoint.Index;
        int balanceLength = 0;
        for (int i = 1; i < points.Count; i++)
        {
            Point currentPoint = points[i];
            if (currentPoint.Value == previousPoint.Value)
            {
                minIndex = Math.Min(minIndex, currentPoint.Index);
                maxIndex = Math.Max(maxIndex, currentPoint.Index);
            }
            else
            {
                balanceLength = Math.Max(balanceLength, maxIndex - minIndex);

                previousPoint = currentPoint;
                minIndex = currentPoint.Index;
                maxIndex = currentPoint.Index;
            }
        }
        return balanceLength;
    }

    private List<Point> GetPoints()
    {
        int value = 0;
        return (new[] {new Point(value, 0)}).Union(_s.Skip(_startIndex).Take(_length).Select((c, i) =>
        {
            var point = new Point(value + (c == _c1 ? 1 : -1), i + 1);
            value = point.Value;
            return point;
        })).ToList();
    }
}