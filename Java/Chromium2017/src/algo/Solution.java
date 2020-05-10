package algo;

import java.util.*;

public class Solution {
    private final int MODULO_BASE = 1_000_000_007;

    public int solution(int[] H) {
        Point[] points = getPoints(H);
        int[] fastAnswers = getFastAnswers(points);
        int result = 0;
        for (int startPointIndex = 0; startPointIndex < points.length; startPointIndex++) {
            int pathCount = fastAnswers[startPointIndex];
            if (pathCount == 0) {
                pathCount = getPathCount(points, startPointIndex);
            }
            result = (result + pathCount ) % MODULO_BASE;
        }
        return result;
    }

    private int[] getFastAnswers(Point[] points) {
        int[] fastAnswers = new int[points.length];
        int max = points[points.length - 1].position;
        int min = points[points.length - 1].position;
        for (int i = points.length - 2; i >= 0 ; i--) {
            if (points[i].position < min || points[i].position > max)
            {
                fastAnswers[i] = points.length - i ;
            }
            min = Math.min(min, points[i].position);
            max = Math.max(max, points[i].position);
        }
        return fastAnswers;
    }

    private int getPathCount(Point[] points, int startPointIndex) {
        if (startPointIndex == points.length - 1)
        {
            return 1;
        }

        int otherSidePathCount = 0;
        int sidePathCount = 1;
        Point startPoint = points[startPointIndex];
        boolean isPreviousOnTheRightSide = points[startPointIndex + 1].position > startPoint.position;
        for (int i = startPointIndex + 2; i < points.length; i++) {
            boolean isCurrentOnTheRightSide = points[i].position > startPoint.position;
            if (isCurrentOnTheRightSide != isPreviousOnTheRightSide) {
                otherSidePathCount = (otherSidePathCount + sidePathCount + 1) % MODULO_BASE;
                isPreviousOnTheRightSide = !isCurrentOnTheRightSide;
            }
            else {
                sidePathCount = (sidePathCount + otherSidePathCount + 1) % MODULO_BASE;
                isPreviousOnTheRightSide = isCurrentOnTheRightSide;
            }
        }
        return (sidePathCount + otherSidePathCount + 1) % MODULO_BASE; // 1 - path with single start point
    }

    private Point[] getPoints(int[] h) {
        Point[] points = new Point[h.length];
        for (int i = 0; i < h.length; i++) {
            points[i] = new Point(h[i], i);
        }
        Arrays.sort(points, Comparator.comparingInt((Point p) -> p.height));
        return points;
    }

    private static class Point {
        public int height;
        public int position;

        public Point(int height, int position) {
            this.height = height;
            this.position = position;
        }
    }
}
