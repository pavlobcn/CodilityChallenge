package algo;

import java.util.*;

public class Solution {
    private final int MODULE_BASE = 1000000007;

    public int solution(int[] H) {
        ArrayList<Point> points = getPoints(H);
        sort(points);
        int result = 0;
        for (int startPointIndex = 0; startPointIndex < points.size(); startPointIndex++) {
            int pathCount = getPathCount(points, startPointIndex);
            result = (result + pathCount ) % MODULE_BASE;
        }
        return result % MODULE_BASE;
    }

    private int getPathCount(ArrayList<Point> points, int startPointIndex) {
        if (startPointIndex == points.size() - 1)
        {
            return 1;
        }

        int pathCount = 1;
        int otherSidePathCount = 0;
        int sidePathCount = 1;
        Point startPoint = points.get(startPointIndex);
        boolean isPreviousOnTheRightSide = points.get(startPointIndex + 1).position > startPoint.position;
        for (int i = startPointIndex + 2; i < points.size(); i++) {
            boolean isCurrentOnTheRightSide = points.get(i).position > startPoint.position;
            if (isCurrentOnTheRightSide != isPreviousOnTheRightSide) {
                // swap sides
                int tmp = otherSidePathCount;
                otherSidePathCount = sidePathCount;
                sidePathCount = tmp;
            }

            int pathCountToCurrentPoint = 1 + otherSidePathCount;
            sidePathCount = (sidePathCount + pathCountToCurrentPoint) % MODULE_BASE;
            pathCount = (pathCount + pathCountToCurrentPoint) % MODULE_BASE;
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
        public Integer height;
        public Integer position;

        public Point(int height, int position) {
            this.height = height;
            this.position = position;
        }
    }
}
