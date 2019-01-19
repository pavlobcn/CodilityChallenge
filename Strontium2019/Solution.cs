using System;
using System.Collections.Generic;
using System.Linq;

class Solution
{
    public int solution(string[] words)
    {
        var letters = GetLetters(words);
        int result = GetResult(letters);
        return result;
    }

    private int GetResult(Dictionary<char, List<LetterInfo>> letters)
    {
        return letters.Values.Max(GetLength);
    }

    private int GetLength(List<LetterInfo> letterInfos)
    {
        int midMax = letterInfos.Max(x => x.Mid);
        int totalSum = letterInfos.Sum(x => x.Total);
        List<LetterInfo> startMaximums = GetTwoMaximums(letterInfos, x => x.Start);
        List<LetterInfo> endMaximums = GetTwoMaximums(letterInfos, x => x.End);
        return Math.Max(midMax, MaxByStartAndEnd(startMaximums, endMaximums) + totalSum);
    }

    private int MaxByStartAndEnd(List<LetterInfo> startMaximums, List<LetterInfo> endMaximums)
    {
        return startMaximums.Join(endMaximums, x => 0, y => 0,
            (startMaximum, endMaximum) => startMaximum == endMaximum
                ? Math.Max(startMaximum.Start, endMaximum.End)
                : startMaximum.Start + endMaximum.End).Max();
    }

    private List<LetterInfo> GetTwoMaximums(List<LetterInfo> letterInfos, Func<LetterInfo, int> valueSelector)
    {
        if (letterInfos.Count == 0)
        {
            return new List<LetterInfo>();
        }
        else if (letterInfos.Count == 1)
        {
            return letterInfos;
        }

        int max1 = letterInfos.Max(valueSelector);
        LetterInfo letterInfo1 = letterInfos.First(x => valueSelector(x) == max1);
        int max2  = letterInfos.Where(x => x != letterInfo1).Max(valueSelector);
        LetterInfo letterInfo2 = letterInfos.Where(x => x != letterInfo1).First(x => valueSelector(x) == max2);
        return new List<LetterInfo>{letterInfo1, letterInfo2};
    }

    private Dictionary<char, List<LetterInfo>> GetLetters(string[] words)
    {
        var allInfo = new Dictionary<char, List<LetterInfo>>();
        foreach (string word in words)
        {
            var wordInfo = GetWordInfo(word);
            foreach (var letterInfo in wordInfo)
            {
                List<LetterInfo> item;
                if (!allInfo.TryGetValue(letterInfo.Key, out item))
                {
                    item = new List<LetterInfo>();
                    allInfo[letterInfo.Key] = item;
                }

                item.Add(letterInfo.Value);
            }
        }

        return allInfo;
    }

    private Dictionary<char, LetterInfo> GetWordInfo(string word)
    {
        var info = new Dictionary<char, LetterInfo>();
        const char noValue = char.MinValue;
        char prevChar = noValue;
        int groupStartIndex = -1;
        for (int i = 0; i < word.Length; i++)
        {
            char c = word[i];
            if (prevChar == noValue)
            {
                prevChar = c;
                groupStartIndex = 0;
                continue;
            }

            if (c != prevChar)
            {
                var letterInfo = new LetterInfo();
                if (groupStartIndex == 0)
                {
                    letterInfo.Start = i;
                }
                else
                {
                    letterInfo.Mid = i - groupStartIndex;
                }

                AddLetterInfo(info, letterInfo, prevChar);

                prevChar = c;
                groupStartIndex = i;
            }
        }

        if (info.Count == 0)
        {
            return new Dictionary<char, LetterInfo> {{word[0], new LetterInfo {Total = word.Length}}};
        }

        var endLetterInfo = new LetterInfo{End = word.Length - groupStartIndex};
        AddLetterInfo(info, endLetterInfo, word[word.Length - 1]);

        return info;
    }

    private void AddLetterInfo(Dictionary<char, LetterInfo> info, LetterInfo letterInfo, char c)
    {
        LetterInfo currentInfo;
        if (!info.TryGetValue(c, out currentInfo))
        {
            info[c] = letterInfo;
            return;
        }

        currentInfo.Mid = Math.Max(currentInfo.Mid, letterInfo.Mid);
        currentInfo.End = letterInfo.End;
    }

    public class LetterInfo
    {
        public int Start { get; set; }
        public int Mid { get; set; }
        public int End { get; set; }
        public int Total { get; set; }

        public override string ToString()
        {
            return $"Start: {Start}, Mid: {Mid}, End: {End}, Total: {Total}";
        }
    }
}
