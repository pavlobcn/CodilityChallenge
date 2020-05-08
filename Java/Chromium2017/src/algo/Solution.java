package algo;

import java.util.*;

public class Solution {
    private final int MODULE_BASE = 1000000007;

    public int solution(int[] H) {
        int[] points = getPoints(H);
        long result = 0;
        for (int startPointIndex = 0; startPointIndex < points.length; startPointIndex++) {
            long pathCount = getPathCount(points, startPointIndex);
            result = (result + pathCount ) % MODULE_BASE;
        }
        return (int)result;
    }

    private long getPathCount(int[] points, int startPointIndex) {
        if (startPointIndex == points.length - 1)
        {
            return 1;
        }

        long otherSidePathCount = 0;
        long sidePathCount = 1;
        int startPoint = points[startPointIndex];
        boolean isPreviousOnTheRightSide = points[startPointIndex + 1] > startPoint;
        for (int i = startPointIndex + 2; i < points.length; i++) {
            boolean isCurrentOnTheRightSide = points[i] > startPoint;
            if (isCurrentOnTheRightSide != isPreviousOnTheRightSide) {
                otherSidePathCount = (otherSidePathCount + sidePathCount + 1) % MODULE_BASE;
                isPreviousOnTheRightSide = !isCurrentOnTheRightSide;
            }
            else {
                sidePathCount = (sidePathCount + otherSidePathCount + 1) % MODULE_BASE;
                isPreviousOnTheRightSide = isCurrentOnTheRightSide;
            }
        }
        return sidePathCount + otherSidePathCount + 1; // 1 - path with single start point
    }

    private int[] getPoints(int[] h) {
        ArrayList<Point> points = new ArrayList<>();
        for (int i = 0; i < h.length; i++) {
            points.add(new Point(h[i], i));
        }
        points.sort(Comparator.comparingInt((Point p) -> p.height));
        int[] pointsOrderedByPosition = new int[h.length];
        for (int i = 0; i < h.length; i++) {
            pointsOrderedByPosition[i] = points.get(i).position;
        }
        return pointsOrderedByPosition;
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
