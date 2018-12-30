using System;
using System.Collections.Generic;
using System.Linq;

class Solution
{
    public int solution(int[] A)
    {
        List<Number> numbers = GetNumbers(A);
        int fastResult = GetFastResult(numbers);
        if (fastResult >= 0)
        {
            return fastResult;
        }

        int result = GetResult(numbers);
        return result;
    }

    private int GetResult(List<Number> numbers)
    {
        var groups = new List<Group> {new Group(numbers[0], numbers[1], numbers[2])};
        for (int i = 3; i < numbers.Count; i++)
        {
            var newGroups = new List<Group>();
            foreach (Group group in groups)
            {
                foreach (Group newGroup in group.GetCandidateGroups(numbers[i]))
                {
                    newGroups.Add(newGroup);
                }
            }

            groups = Merge(groups, newGroups);
        }

        for (int i = 0; i < numbers.Count; i++)
        {
            var number = numbers[i];
            var newGroups = new List<Group>();
            foreach (Group group in groups)
            {
                if (group.Number1 != number && group.Number2 != number && group.Number3 != number)
                {
                    foreach (Group newGroup in group.GetCandidateGroups(number))
                    {
                        newGroups.Add(newGroup);
                    }
                }
            }

            groups = Merge(groups, newGroups);
        }

        return groups.First().TrailingZeroCount;
    }

    private List<Group> Merge(List<Group> groups, List<Group> newGroups)
    {
        newGroups.AddRange(groups);
        int maxTrailingZeros = newGroups.Max(g => g.TrailingZeroCount);
        newGroups = newGroups.Where(g => g.TrailingZeroCount == maxTrailingZeros).ToList();
        int maxA = -1;
        int maxB = -1;
        foreach (Group newGroup in newGroups)
        {
            maxA = Math.Max(maxA, newGroup.Sum.A);
            maxB = Math.Max(maxB, newGroup.Sum.B);
        }

        return newGroups.Where(g => g.Sum.A == maxA || g.Sum.B == maxB).ToList();
    }

    private int GetFastResult(List<Number> numbers)
    {
        Number first = numbers.First();
        bool isIdenticalNumbers = numbers.All(x => x.A == first.A && x.B == first.B);
        if (isIdenticalNumbers)
        {
            return 3 * Math.Min(first.A, first.B);
        }

        bool isAllAZeros = numbers.All(x => x.A == 0);
        if (isAllAZeros)
        {
            return 0;
        }

        bool isAllBZeros = numbers.All(x => x.B == 0);
        if (isAllBZeros)
        {
            return 0;
        }

        return -1;
    }

    private List<Number> GetNumbers(int[] a)
    {
        return a.Select(x => new Number(GetPow2(x), GetPow5(x))).ToList();
    }

    private int GetPow(long x, int @base)
    {
        int result = 0;
        while (x % @base == 0)
        {
            result++;
            x = x / @base;
        }
        return result;
    }

    private int GetPow2(int x)
    {
        return GetPow(x, 2);
    }

    private int GetPow5(int x)
    {
        return GetPow(x, 5);
    }
}

public class Number
{
    public int A { get; set; }
    public int B { get; set; }

    public Number(int a, int b)
    {
        A = a;
        B = b;
    }

    public override string ToString()
    {
        return $"{A},{B}";
    }
}

public class Group
{
    public Number Number1 { get; set; }
    public Number Number2 { get; set; }
    public Number Number3 { get; set; }
    private Number _sum;
    private int? _trailingZeroCount;

    public Group(Number number1, Number number2, Number number3)
    {
        Number1 = number1;
        Number2 = number2;
        Number3 = number3;
    }

    public Number Sum
    {
        get
        {
            if (_sum == null)
            {
                _sum = new Number(Number1.A + Number2.A + Number3.A, Number1.B + Number2.B + Number3.B);
            }

            return _sum;
        }
    }

    public int TrailingZeroCount
    {
        get
        {
            if (!_trailingZeroCount.HasValue)
            {
                _trailingZeroCount = Math.Min(Sum.A, Sum.B);
            }

            return _trailingZeroCount.Value;
        }
    }

    public IEnumerable<Group> GetCandidateGroups(Number number)
    {
        if (number.A != Number3.A || number.B != Number3.B)
        {
            yield return new Group(Number1, Number2, number);
        }

        if (number.A != Number2.A || number.B != Number2.B)
        {
            yield return new Group(Number1, Number3, number);
            
        }

        if (number.A != Number1.A || number.B != Number1.B)
        {
            yield return new Group(Number2, Number3, number);
        }
    }
}
