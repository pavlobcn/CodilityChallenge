using System;
using System.Linq;

class Solution
{
    public const char L = 'L';
    public const char M = 'M';
    private string _s;
    private int _k;
    private Lines _lines;
    private int[] _splitCounts;
    private int _totalSplitCount;
    private int _edgeSplitCount;

    public int solution(string S, int K)
    {
        _s = S;
        _k = K;

        int fastResult = GetFastResult();
        if (fastResult > -1)
        {
            return fastResult;
        }

        _lines = Lines.GetLines(S);
        GetSplitCounts();

        var seed = new {L = S.Take(_k).Count(c => c == L), M = S.Take(_k).Count(c => c == M), Result = _s.Length};
        var aggregate = Enumerable.Range(0, _s.Length)
                .Aggregate(seed, (acc, i) =>
                    {
                        int newResult = GetSingleResult(i, acc.L);
                        return new {L = GetNewWeight(acc.L, i, L), M = GetNewWeight(acc.M, i, M), Result = Math.Min(acc.Result, newResult)};
                    }
                );

        return aggregate.Result;
    }

    private void GetSplitCounts()
    {
        _splitCounts = new int[_s.Length];
        _totalSplitCount = 0;
        for (int i = 0; i < _s.Length; i++)
        {
            if (_s[i] == M || i == 0)
            {
                continue;
            }

            if (_s[GetNormalIndex(i - 1)] == M)
            {
                Line line = _lines.GetLine(GetNormalIndex(i - 1));
                if (line.Start == 0 || line.Start > i)
                {
                    continue;
                }
                int splitCount = GetSplitCount(line.Length);
                _totalSplitCount = _totalSplitCount + splitCount;
            }
            _splitCounts[i] = _totalSplitCount;
        }

        if (_s[0] == M)
        {
            _edgeSplitCount = GetSplitCount(_lines.GetLine(0).Length);
        }
        else if (_s[_s.Length - 1] == M)
        {
            _edgeSplitCount = GetSplitCount(_lines.GetLine(_s.Length - 1).Length);
        }

        _totalSplitCount += _edgeSplitCount;
    }

    private int GetNormalIndex(int index)
    {
        return (_s.Length + index) % _s.Length;
    }

    private int GetFastResult()
    {
        if (_k == 0)
        {
            return _s.Count(c => c == M);
        }

        if (_s.All(c => c == M))
        {
            return (int)Math.Ceiling((double)_s.Length / (_k + 1));
        }

        if (_s.All(c => c == L))
        {
            return _k;
        }

        return -1;
    }

    private int GetSplitCount(int lineLength)
    {
        return lineLength / (_k + 1);
    }

    private int GetSingleResult(int i, int l)
    {
        int result = 0;
        result += l;
        int leftBound = GetNormalIndex(i - 1);
        Line line1 = null;
        if (_s[leftBound] == M)
        {
            result++;
            line1 = _lines.GetLine(leftBound);
            leftBound = GetNormalIndex(line1.Start - 1);
            result += GetSplitCount(GetNormalIndex(i - line1.Start - 1));
        }

        Line line2 = null;
        int rightBound = GetNormalIndex(i + _k);
        bool isCycle = false; ;
        if (_s[rightBound] == M)
        {
            result++;
            line2 = _lines.GetLine(rightBound);
            isCycle = IsCycle(line1, line2, i);
            if (isCycle)
            {

            }
            else
            {
                rightBound = GetNormalIndex(line2.Start + line2.Length);
                result += GetSplitCount(GetNormalIndex(line2.Length + line2.Start - (i + _k) - 1));
            }
        }

        if (!isCycle)
        {
            result += GetSplitCountByBounds(leftBound, rightBound);
        }

        return result;
    }

    private bool IsCycle(Line line1, Line line2, int position)
    {
        if (line1 != line2)
        {
            return false;
        }

        if (position < line1.Start)
        {
            position = position + _s.Length;
        }

        if (position >= line1.Start &&
            position <= line1.Start + line1.Length - 1 &&
            position + _k - 1 >= line1.Start && position + _k - 1 <= line1.Start + line1.Length - 1)
        {
            return false;
        }

        return true;
    }

    private int GetSplitCountByBounds(int leftBound, int rightBound)
    {
        if (leftBound < rightBound)
        {
            return _totalSplitCount - (_splitCounts[rightBound] - _splitCounts[leftBound]);
        }

        return _totalSplitCount - (_splitCounts[rightBound] + _edgeSplitCount - _splitCounts[leftBound]);
    }

    private int GetNewWeight(int oldWeight, int oldPosition, char c)
    {
        int shift = 0;
        if (_s[oldPosition] == c)
        {
            shift--;
        }

        if (_s[GetNormalIndex(oldPosition + _k)] == c)
        {
            shift++;
        }

        return oldWeight + shift;
    }

    public class Line
    {
        public int Start { get; set; }
        public int Length { get; set; }

        public override string ToString()
        {
            return $"Start: {Start}, Length: {Length}";
        }
    }

    public class Lines
    {
        private readonly Line[] _lines;

        private Lines(Line[] lines)
        {
            _lines = lines;
        }

        public static Lines GetLines(string s)
        {
            var lines = new Line[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                Line line = null;
                char c = s[i];
                if (c == M)
                {
                    if (i != 0 && s[i - 1] == M)
                    {
                        line = lines[i - 1];
                        line.Length++;
                    }
                    else
                    {
                        line = new Line { Start = i, Length = 1 };
                    }
                }

                lines[i] = line;
            }

            Line firstLine = lines[0];
            Line lineToSubstitute = lines[lines.Length - 1];
            if (firstLine != null && lineToSubstitute != null)
            {
                firstLine.Start = lineToSubstitute.Start;
                firstLine.Length += lineToSubstitute.Length;
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i] == lineToSubstitute)
                    {
                        lines[i] = firstLine;
                    }
                }
            }
            return new Lines(lines);
        }

        public Line GetLine(int position)
        {
            return _lines[position];
        }
    }
}
