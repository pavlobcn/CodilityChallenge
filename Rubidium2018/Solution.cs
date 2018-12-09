using System;
using System.Collections.Generic;

class Solution
{
    public int solution(int[] X, int[] Y)
    {
        var points = new List<Point>();
        for (int i = 0; i < X.Length; i++)
        {
            points.Add(new Point(X[i], Y[i]));
        }

        int distance = int.MaxValue;
        foreach (var pointA in points)
        {
            foreach (var pointB in points)
            {
                if (pointA == pointB)
                {
                    continue;
                }

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
}

public class Point
{
    public int X { get; set; }
    public int Y { get; set; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
}