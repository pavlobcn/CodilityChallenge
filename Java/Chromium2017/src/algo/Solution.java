package algo;

import java.util.*;

public class Solution {
    private final int MODULE_BASE = 1000000007;

    public int solution(int[] H) {
        ArrayList<Point> points = getPoints(H);
        sort(points);
        long result = 0;
        for (int startPointIndex = 0; startPointIndex < points.size(); startPointIndex++) {
            long pathCount = getPathCount(points, startPointIndex);
            result = (result + pathCount ) % MODULE_BASE;
        }
        return (int)(result % MODULE_BASE);
    }

    private long getPathCount(ArrayList<Point> points, int startPointIndex) {
        if (startPointIndex == points.size() - 1)
        {
            return 1;
        }

        long pathCount = 1;
        long otherSidePathCount = 0;
        long sidePathCount = 1;
        Point startPoint = points.get(startPointIndex);
        boolean isPreviousOnTheRightSide = points.get(startPointIndex + 1).position > startPoint.position;
        for (int i = startPointIndex + 2; i < points.size(); i++) {
            boolean isCurrentOnTheRightSide = points.get(i).position > startPoint.position;
            if (isCurrentOnTheRightSide != isPreviousOnTheRightSide) {
                // swap sides
                long tmp = otherSidePathCount;
                otherSidePathCount = sidePathCount;
                sidePathCount = tmp;
            }

            long pathCountToCurrentPoint = 1 + otherSidePathCount;
            sidePathCount = (sidePathCount + pathCountToCurrentPoint);
            pathCount = (pathCount + pathCountToCurrentPoint);
            isPreviousOnTheRightSide = isCurrentOnTheRightSide;
        }
        return pathCount + 1; // 1 - path with single start point
    }

    private void sort(ArrayList<Point> points) {
        points.sort(Comparator.comparingInt((Point p) -> p.height));
    }

    private ArrayList<Point> getPoints(int[] h) {
        ArrayList<Point> points = new ArrayList<>();
        for (int i = 0; i < h.length; i++) {
            points.add(new Point(h[i], i));
        }
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
