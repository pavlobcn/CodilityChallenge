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
        int pathCount = 0;
        int otherSidePathCount = 0;
        int sidePathCount = 0;
        boolean isPreviousOnTheRightSide = false;
        Point startPoint = points.get(startPointIndex);
        for (int i = startPointIndex + 1; i < points.size(); i++) {
            boolean isCurrentOnTheRightSide = points.get(i).position > startPoint.position;
            if (i == startPointIndex + 1)
            {
                isPreviousOnTheRightSide = !isCurrentOnTheRightSide;
            }
            boolean sameSide = isCurrentOnTheRightSide == isPreviousOnTheRightSide;
            if (!sameSide) {
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
        Collections.sort(points, (Point p1, Point p2) -> p1.height - p2.height);
    }

    private ArrayList<Point> getPoints(int[] h) {
        ArrayList<Point> points = new ArrayList<Point>();
        for (int i = 0; i < h.length; i++) {
            points.add(new Point(h[i], i));
        }
        return points;
    }

    private class Point {
        public Integer height;
        public Integer position;

        public Point(int height, int position) {
            this.height = height;
            this.position = position;
        }
    }
}
