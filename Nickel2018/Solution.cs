using System;
using System.Collections.Generic;
using System.Linq;

class Solution
{
    private const int MaxResult = 1000000000;

    public int solution(bool[] P)
    {
        long result;
        long total = P.LongLength * (P.LongLength + 1) / 2;
        if (P.All(x => x))
        {
            result = total;

        }
        else if (P.All(x => !x))
        {
            return 0;
        }
        else
        {
            List<Group> groups = BuildGroups(P);
            long groupsSum = groups.Sum(g => GetGroupValue(g));
            result = total - groupsSum;
        }

        return (int)Math.Min(MaxResult, result);
    }

    private List<Group> BuildGroups(bool[] p)
    {
        var groups = new List<Group>();
        for (int i = 0; i < p.Length; i++)
        {
            if (p[i])
            {
                continue;
            }

            if (i == 0 || p[i - 1])
            {
                groups.Add(new Group{Length = 1});
            }
            else
            {
                groups.Last().Length++;
            }
        }

        return groups;
    }

    private long GetGroupValue(Group group)
    {
        long result = group.Length * (group.Length + 1) / 2;
        return result;
    }

    private class Group
    {
        public long Length { get; set; }
    }
}
