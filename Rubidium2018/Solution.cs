using System;
using System.Collections.Generic;
using System.Linq;

class Solution
{
    public const int MaxN = 100000;

    private List<PointGroup> _orderedByXPoints;
    private List<PointGroup> _orderedByYPoints;
    private List<Point> _points;

    public int solution(int[] X, int[] Y)
    {
        PrepareDataStructure(X, Y);

        int fastResult = GetFastResult();
        if (fastResult <= 1)
        {
            return 0;
        }

        int distance = CalculateDistance(fastResult);
        return distance / 2;
    }

    private int GetFastResult()
    {
        int minYDistance = _orderedByXPoints.Select(g => g.Points.Skip(1).Select((p, i) => p.Y - g.Points[i].Y).MinOrDefault()).Min();
        if (minYDistance <= 1)
        {
            return 0;
        }
        int minXDistance = _orderedByYPoints.Select(g => g.Points.Skip(1).Select((p, i) => p.X - g.Points[i].X).MinOrDefault()).Min();
        if (minXDistance <= 1)
        {
            return 0;
        }

        return Math.Min(minYDistance, minXDistance);
    }

    private int CalculateDistance(int fastResult)
    {
        var resultState = new ResultState {Value = fastResult};
        foreach (var pointA in _points)
        {
            foreach (Point pointB in GetClosePoints(pointA, resultState))
            {
                int newDistance = pointA.Distance(pointB);
                if (newDistance < resultState.Value)
                {
                    resultState.Value = newDistance;

                    if (resultState.Value <= 1)
                    {
                        return 0;
                    }
                }
            }
        }

        return resultState.Value;
    }

    private IEnumerable<Point> GetClosePoints(Point point, ResultState resultState)
    {
        int currentLeftIndex = point.XGroupIndex;
        int currentRightIndex = point.XGroupIndex + 1;
        int currentBottomIndex = point.YGroupIndex;
        int currentTopIndex = point.YGroupIndex + 1;
        while (
            currentLeftIndex >= 0 ||
            currentRightIndex < _orderedByXPoints.Count ||
            currentBottomIndex >= 0 ||
            currentTopIndex < _orderedByYPoints.Count
               )
        {
            if (currentLeftIndex >= 0)
            {
                PointGroup group = _orderedByXPoints[currentLeftIndex];
                if (point.X - group.Key < resultState.Value)
                {
                    foreach (Point verticalPoint in group.GetVerticalPoints(point))
                    {
                        yield return verticalPoint;
                    }
                    currentLeftIndex--;
                }
                else
                {
                    currentLeftIndex = -1;
                }
            }

            if (currentRightIndex < _orderedByXPoints.Count)
            {
                PointGroup group = _orderedByXPoints[currentRightIndex];
                if (group.Key - point.X < resultState.Value)
                {
                    foreach (Point verticalPoint in group.GetVerticalPoints(point))
                    {
                        yield return verticalPoint;
                    }
                    currentRightIndex++;
                }
                else
                {
                    currentRightIndex = _orderedByXPoints.Count;
                }
            }

            if (currentBottomIndex >= 0)
            {
                PointGroup group = _orderedByYPoints[currentBottomIndex];
                if (point.Y - group.Key < resultState.Value)
                {
                    foreach (Point horizontalPoint in group.GetHorizontalPoints(point))
                    {
                        yield return horizontalPoint;
                    }
                    currentBottomIndex--;
                }
                else
                {
                    currentBottomIndex = -1;
                }
            }

            if (currentTopIndex < _orderedByYPoints.Count)
            {
                PointGroup group = _orderedByYPoints[currentTopIndex];
                if (group.Key - point.Y < resultState.Value)
                {
                    foreach (Point horizontalPoint in group.GetHorizontalPoints(point))
                    {
                        yield return horizontalPoint;
                    }
                    currentTopIndex++;
                }
                else
                {
                    currentTopIndex = _orderedByYPoints.Count;
                }
            }
        }
    }

    private void PrepareDataStructure(int[] x, int[] y)
    {
        _points = new List<Point>();
        for (int i = 0; i < x.Length; i++)
        {
            _points.Add(new Point(x[i], y[i]));
        }

        _orderedByXPoints = _points.GroupBy(p => p.X, p => p, (key, pp) => new PointGroup(key, pp.ToList())).ToList();
        _orderedByXPoints.Sort((g1, g2) => g1.Key - g2.Key);
        for (int i = 0; i < _orderedByXPoints.Count; i++)
        {
            _orderedByXPoints[i].Points.Sort((p1, p2) => p1.Y - p2.Y);
            for (int j = 0; j < _orderedByXPoints[i].Points.Count; j++)
            {
                Point point = _orderedByXPoints[i].Points[j];
                point.XGroupIndex = i;
            }
        }

        _orderedByYPoints = _points.GroupBy(p => p.Y, p => p, (key, pp) => new PointGroup(key, pp.ToList())).ToList();
        _orderedByYPoints.Sort((g1, g2) => g1.Key - g2.Key);
        for (int i = 0; i < _orderedByYPoints.Count; i++)
        {
            _orderedByYPoints[i].Points.Sort((p1, p2) => p1.X - p2.X);
            for (int j = 0; j < _orderedByYPoints[i].Points.Count; j++)
            {
                Point point = _orderedByYPoints[i].Points[j];
                point.YGroupIndex = i;
            }
        }
    }
}

public class ResultState
{
    public int Value { get; set; }
}

public partial class Point
{
    public int X { get; set; }
    public int Y { get; set; }

    public int XGroupIndex { get; set; }

    public int YGroupIndex { get; set; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int Distance(Point point)
    {
        return Math.Max(Math.Abs(point.X - X), Math.Abs(point.Y - Y));
    }
}

public partial class PointGroup
{
    public int Key { get; set; }
    public List<Point> Points { get; private set; }

    public PointGroup(int key, List<Point> points)
    {
        Key = key;
        Points = points;
    }

    public IEnumerable<Point> GetVerticalPoints(Point point)
    {
        int minBound = 0;
        int maxBound = Points.Count - 1;
        while (true)
        {
            if (point.Y < Points[minBound].Y)
            {
                yield return Points[minBound];
                yield break;
            }

            if (point.Y > Points[maxBound].Y)
            {
                yield return Points[maxBound];
                yield break;
            }

            int midIndex = (minBound + maxBound) / 2;
            Point midPoint = Points[midIndex];
            if (midPoint.Y < point.Y)
            {
                minBound = midIndex + 1;
                continue;
            }

            if (midPoint.Y > point.Y)
            {
                maxBound = midIndex - 1;
                continue;
            }

            // midPoint.Y == point.Y
            if (midPoint == point)
            {
                if (midIndex > 0)
                {
                    yield return Points[midIndex - 1];
                }
                if (midIndex < Points.Count - 2)
                {
                    yield return Points[midIndex + 1];
                }
            }
            else
            {
                yield return midPoint;
            }
            yield break;
        }
    }

    public IEnumerable<Point> GetHorizontalPoints(Point point)
    {
        int minBound = 0;
        int maxBound = Points.Count - 1;
        while (true)
        {
            if (point.X < Points[minBound].X)
            {
                yield return Points[minBound];
                yield break;
            }

            if (point.X > Points[maxBound].X)
            {
                yield return Points[maxBound];
                yield break;
            }

            int midIndex = (minBound + maxBound) / 2;
            Point midPoint = Points[midIndex];
            if (midPoint.X < point.X)
            {
                minBound = midIndex + 1;
                continue;
            }

            if (midPoint.X > point.X)
            {
                maxBound = midIndex - 1;
                continue;
            }

            // midPoint.X == point.X
            if (midPoint == point)
            {
                if (midIndex > 0)
                {
                    yield return Points[midIndex - 1];
                }
                if (midIndex < Points.Count - 2)
                {
                    yield return Points[midIndex + 1];
                }
            }
            else
            {
                yield return midPoint;
            }
            yield break;
        }
    }
}

public static class EnumerableExtensions
{
    public static int MinOrDefault(this IEnumerable<int> source)
    {
        if (source.Any())
        {
            return source.Min();
        }

        return int.MaxValue;
    }
}