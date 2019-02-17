using System;
using System.Collections.Generic;
using System.Linq;

class Solution
{
    public int solution(string S, int K)
    {
        if (IsNoAnswer(S, K))
        {
            return -1;
        }

        if (IsZero(S, K))
        {
            return 0;
        }

        int result = GetResult(S, K);

        return result;
    }

    private int GetResult(string s, int k)
    {
        List<Info> left = GetInfos(s.ToCharArray());
        List<Info> right = GetInfos(s.Reverse().ToArray());
        int result = s.Length;
        for (int i = 0; i <= k; i++)
        {
            List<Info> leftChars;
            List<Info> rightChars;
            if (i == 0)
            {
                rightChars = right.Take(k).ToList();
                leftChars = TakeWithCondition(left, 1, info => rightChars.Any(x => x.C == info.C));
                leftChars.RemoveAt(leftChars.Count - 1);
            }
            else
            {
                leftChars = left.Take(i).ToList();
                rightChars = TakeWithCondition(right, k - i + 1, info => leftChars.Any(x => x.C == info.C));
                rightChars.RemoveAt(rightChars.Count - 1);
            }
            int newResult = s.Length - leftChars.Concat(rightChars).Sum(x => x.Length);
            result = Math.Min(result, newResult);
            if (result == 1)
            {
                return result;
            }
        }

        return result;
    }

    private List<Info> TakeWithCondition(List<Info> source, int count, Func<Info, bool> countSkipCondition)
    {
        var result = new List<Info>();
        int added = 0;
        for (int i = 0; i < source.Count; i++)
        {
            if (!countSkipCondition(source[i]))
            {
                added++;
            }
            result.Add(source[i]);
            if (added == count)
            {
                break;
            }
        }
        return result;
    }

    private class Info
    {
        public char C { get; }
        public int Length { get; }

        public Info(char c, int length)
        {
            C = c;
            Length = length;
        }
    }

    private List<Info> GetInfos(char[] s)
    {
        var infos = new List<Info>();
        bool[] chars = new bool[26];
        int length = 0;
        char c = Char.MinValue;
        for (int i = 0; i <= s.Length; i++)
        {
            if (i == 0)
            {
                length = 1;
                c = s.First();
                chars[c - 'a'] = true;
                continue;
            }

            char current;
            bool isNewChar;
            if (i == s.Length)
            {
                current = char.MinValue;
                isNewChar = true;
            }
            else
            {
                current = s[i];
                isNewChar = !chars[current - 'a'];
            }

            if (isNewChar)
            {
                infos.Add(new Info(c, length));
                length = 1;
                c = current;
                if (i != s.Length)
                {
                    chars[current - 'a'] = true;
                }
            }
            else
            {
                length++;
            }
        }

        return infos;
    }

    private bool IsNoAnswer(string s, int k)
    {
        if (k == 0)
        {
            return true;
        }

        bool[] chars = new bool[26];
        foreach (char c in s)
        {
            chars[c - 'a'] = true;
        }

        bool isNoAnswer = chars.Count(x => x) < k;
        return isNoAnswer;
    }

    private bool IsZero(string s, int k)
    {
        bool[] chars = new bool[26];
        foreach (char c in s)
        {
            chars[c - 'a'] = true;
        }

        bool isZero = chars.Count(x => x) == k;
        return isZero;
    }
}
