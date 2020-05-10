package algo;

import java.util.*;

public class Solution {
    private static final int MODULO_BASE = 1_000_000_007;

    public int solution(int[] H) {
        Point[] points = getOrderedPoints(H);
        Point[] originalPoints = getOriginalPoints(points);
        int result = 0;
        Point prevPoint = points[0];
        for (int i = 1; i < points.length; i++) {
            Point nextPoint = points[i];
            int prevPosition = prevPoint.position;
            int nextPosition = nextPoint.position;
            int counterStep = prevPosition < nextPosition ? 1 : -1;
            // skip prevPosition
            prevPosition = prevPosition + counterStep;
            while (prevPosition != nextPosition)
            {
                originalPoints[prevPosition].move(nextPoint);
                prevPosition = prevPosition + counterStep;
            }
            prevPoint = nextPoint;
        }
        for (Point point : points)
        {
            point.finish(points.length);
            result = (result + point.getPathCount()) % MODULO_BASE;
        }
        return result;
    }

    private Point[] getOrderedPoints(int[] h) {
        Point[] points = new Point[h.length];
        for (int i = 0; i < h.length; i++) {
            points[i] = new Point(h[i], i);
        }
        Arrays.sort(points, Comparator.comparingInt((Point p) -> p.height));
        for (int i = 0; i < h.length; i++) {
            points[i].sortedPosition = i;
        }
        return points;
    }

    private Point[] getOriginalPoints(Point[] orderedPoints) {
        Point[] points = new Point[orderedPoints.length];
        for (Point point : orderedPoints) {
            points[point.position] = point;
        }
        return points;
    }

    private static class Point {
        public int height;
        public int position;
        public int sortedPosition;
        private int handledHeight;
        private long sidePathCount;
        private long otherSidePathCount;

        public Point(int height, int position) {
            this.height = height;
            this.position = position;
        }

        public int getPathCount() {
            return (int)((otherSidePathCount + sidePathCount + 1) % Solution.MODULO_BASE);
        }

        public void move(Point nextPoint) {
            if (height > nextPoint.height) {
                return;
            }

            int groupSize = nextPoint.sortedPosition - sortedPosition - handledHeight - 1;
            long pathCountToGroup = (1 + otherSidePathCount) * groupSize;
            sidePathCount += pathCountToGroup;
            handledHeight += groupSize;

            // swap sides
            long tmp = otherSidePathCount;
            otherSidePathCount = sidePathCount;
            sidePathCount = tmp;
        }

        public void finish(int lastPosition) {
            Point finishPoint = new Point(Integer.MAX_VALUE, lastPosition);
            finishPoint.sortedPosition = lastPosition;
            move(finishPoint);
        }

        @Override
        public String toString() {
            return "Point{" +
                    "height=" + height +
                    ", position=" + position +
                    ", sortedPosition=" + sortedPosition +
                    ", handledHeight=" + handledHeight +
                    ", sidePathCount=" + sidePathCount +
                    ", otherSidePathCount=" + otherSidePathCount +
                    ", pathCount=" + getPathCount() +
                    '}';
        }
    }
}
