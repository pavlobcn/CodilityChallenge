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

        var seed = new {L = S.Take(_k).Count(c => c == L), Result = _s.Length};
        var aggregate = Enumerable.Range(0, _s.Length - _k + 1)
                .Aggregate(seed, (acc, i) =>
                    {
                        int newResult = GetSingleResult(i, acc.L);
                        return new {L = GetNewWeight(acc.L, i, L), Result = Math.Min(acc.Result, newResult)};
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

            if (_s[i - 1] == M)
            {
                Line line = _lines.GetLine(i - 1);
                int splitCount = GetSplitCount(line.Length);
                _totalSplitCount = _totalSplitCount + splitCount;
            }

            _splitCounts[i] = _totalSplitCount;
        }

        if (_s[_s.Length - 1] == M)
        {
            int splitCount = GetSplitCount(_lines.GetLine(_s.Length - 1).Length);
            _totalSplitCount = _totalSplitCount + splitCount;
        }
    }

    private int GetFastResult()
    {
        if (_k == 0)
        {
            return _s.Count(c => c == M);
        }

        if (_s.Length == _k)
        {
            return _s.Count(c => c == L);
        }

        if (_s.All(c => c == M))
        {
            return (int)Math.Floor((double)_s.Length / (_k + 1));
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
        int leftBound = i - 1;
        if (i > 0 && _s[leftBound] == M)
        {
            result++;
            var line1 = _lines.GetLine(leftBound);
            leftBound = line1.Start - 1;
            result += GetSplitCount(i - line1.Start - 1);
        }

        int rightBound = i + _k;
        if (i + _k < _s.Length && _s[rightBound] == M)
        {
            result++;
            var line2 = _lines.GetLine(rightBound);
            rightBound = line2.Start + line2.Length;
            result += GetSplitCount(line2.Length + line2.Start - (i + _k) - 1);
        }

        result += GetSplitCountByBounds(leftBound, rightBound);

        return result;
    }

    private int GetSplitCountByBounds(int leftBound, int rightBound)
    {
        int leftWeight = 0;
        if (leftBound >= 0)
        {
            leftWeight = _splitCounts[leftBound];
        }

        int rightWeight = _totalSplitCount;
        if (rightBound < _s.Length)
        {
            rightWeight = _splitCounts[rightBound];
        }

        return _totalSplitCount - (rightWeight - leftWeight);
    }

    private int GetNewWeight(int oldWeight, int oldPosition, char c)
    {
        int shift = 0;
        if (_s[oldPosition] == c)
        {
            shift--;
        }

        if (oldPosition + _k < _s.Length && _s[oldPosition + _k] == c)
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

            return new Lines(lines);
        }

        public Line GetLine(int position)
        {
            return _lines[position];
        }
    }
}
