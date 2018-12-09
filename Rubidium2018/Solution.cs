using System;
using System.Collections.Generic;
using System.Linq;

class Solution
{
    public int solution(int[] X, int[] Y)
    {
        var points = new List<Point>();
        for (int i = 0; i < X.Length; i++)
        {
            points.Add(new Point(X[i], Y[i]));
        }

        var orderedPoints = points.OrderBy(p => p.Y).ToList();
        for (int i = 0; i < orderedPoints.Count; i++)
        {
            orderedPoints[i].Index = i;
        }
        int distance = int.MaxValue;
        foreach (var pointA in points)
        {
            foreach (var pointB in GetPoints(orderedPoints, pointA))
            {
                int newDistance = Math.Max(Math.Abs(pointA.X - pointB.X), Math.Abs(pointA.Y - pointB.Y));
                if (newDistance < distance)
                {
                    distance = newDistance;
                }

                if (distance <= 1)
                {
                    return 0;
                }
            }
        }

        return distance / 2;
    }

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
}

public class Point
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Index { get; set; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
}