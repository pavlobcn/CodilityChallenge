using System;
using System.Collections.Generic;
using System.Linq;

class Solution
{
    private List<PointGroup> _orderedByXPoints;
    private List<PointGroup> _orderedByYPoints
        ;

    private List<Point> points;
    public const int MaxN = 100000;
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
            return minYDistance;
        }
        int minXDistance = _orderedByYPoints.Select(g => g.Points.Skip(1).Select((p, i) => p.X - g.Points[i].X).MinOrDefault()).Min();
        if (minXDistance <= 1)
        {
            return minXDistance;
        }

        return Math.Min(minYDistance, minXDistance);
    }

    private int CalculateDistance(int fastResult)
    {
        int distance = fastResult;
        foreach (var pointA in points)
        {
        }

        return distance;
    }

    private void PrepareDataStructure(int[] X, int[] Y)
    {
        points = new List<Point>();
        for (int i = 0; i < X.Length; i++)
        {
            points.Add(new Point(X[i], Y[i]));
        }

        _orderedByXPoints = points.GroupBy(p => p.X, p => p, (key, pp) => new PointGroup(key, pp.ToList())).ToList();
        _orderedByXPoints.Sort((g1, g2) => g1.Key - g2.Key);
        for (int i = 0; i < _orderedByXPoints.Count; i++)
        {
            _orderedByXPoints[i].Points.Sort((p1, p2) => p1.Y - p2.Y);
            for (int j = 0; j < _orderedByXPoints[i].Points.Count; j++)
            {
                Point point = _orderedByXPoints[i].Points[j];
                point.XGroupIndex = i;
                point.YIndex = j;
            }
        }

        _orderedByYPoints = points.GroupBy(p => p.Y, p => p, (key, pp) => new PointGroup(key, pp.ToList())).ToList();
        _orderedByYPoints.Sort((g1, g2) => g1.Key - g2.Key);
        for (int i = 0; i < _orderedByYPoints.Count; i++)
        {
            _orderedByYPoints[i].Points.Sort((p1, p2) => p1.X - p2.X);
            for (int j = 0; j < _orderedByYPoints[i].Points.Count; j++)
            {
                Point point = _orderedByYPoints[i].Points[j];
                point.YGroupIndex = i;
                point.XIndex = j;
            }
        }
    }

    /*
    private IEnumerable<Point> GetPoints(List<Point> points, Point pointA)
    {
        int leftIndex = pointA.Index - 1;
        int rightIndex = pointA.Index + 1;
        while (leftIndex >= 0 || rightIndex < points.Count)
        {
            if (leftIndex < 0)
            {
                yield return points[rightIndex];
                rightIndex++;
                continue;
            }
            if (rightIndex == points.Count)
            {
                yield return points[leftIndex];
                leftIndex--;
                continue;
            }

            int yOfLeft = points[leftIndex].Y;
            int yOfRight = points[rightIndex].Y;
            if (pointA.Y - yOfLeft < yOfRight - pointA.Y)
            {
                yield return points[leftIndex];
                leftIndex--;
            }
            else
            {
                yield return points[rightIndex];
                rightIndex++;
            }
        }
    }
    */
}

public class Point
{
    public int X { get; set; }
    public int Y { get; set; }

    public int XGroupIndex { get; set; }

    public int YIndex { get; set; }

    public int YGroupIndex { get; set; }

    public int XIndex { get; set; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public class PointGroup
{
    public int Key { get; set; }
    public List<Point> Points { get; private set; }

    public PointGroup(int key, List<Point> points)
    {
        Key = key;
        Points = points;
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