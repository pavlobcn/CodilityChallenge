import java.util.*;

public class Solution {
    private static final int MODULO_BASE = 1_000_000_007;

    public int solution(int[] H) {
        Point[] points = getOrderedPoints(H);
        Point[] originalPoints = getOriginalPoints(points);
        visitAllPoints(points, originalPoints);
        return getResult(points);
    }

    private void visitAllPoints(Point[] points, Point[] originalPoints) {
        Point prevPoint = points[0];
        for (int i = 1; i < points.length; i++) {
            Point nextPoint = points[i];
            int prevPosition = prevPoint.position;
            int nextPosition = nextPoint.position;
            int counterStep = prevPosition < nextPosition ? 1 : -1;
            if (Math.abs(nextPosition - prevPosition) < i) {
                // skip prevPosition
                prevPosition = prevPosition + counterStep;
                while (prevPosition != nextPosition) {
                    originalPoints[prevPosition].move(nextPoint);
                    prevPosition = prevPosition + counterStep;
                }
            }
            else {
                for (int j = 0; j < i; j++) {
                    Point lowerPoint = points[j];
                    if (lowerPoint.position > prevPosition && lowerPoint.position < nextPosition ||
                            lowerPoint.position > nextPosition && lowerPoint.position < prevPosition) {
                        lowerPoint.move(nextPoint);
                    }
                }
            }
            prevPoint = nextPoint;
        }
    }

    private int getResult(Point[] points) {
        int result = 0;
        for (Point point : points) {
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
        private int height;
        private int position;
        private int sortedPosition;
        private int handledHeight;
        private long sidePathCount;
        private long otherSidePathCount;

        private Point(int height, int position) {
            this.height = height;
            this.position = position;
        }

        private int getPathCount() {
            return (int)((otherSidePathCount + sidePathCount + 1) % Solution.MODULO_BASE);
        }

        private void move(Point nextPoint) {
            if (height > nextPoint.height) {
                return;
            }

            int groupSize = nextPoint.sortedPosition - sortedPosition - handledHeight - 1;
            handledHeight += groupSize;
            long pathCountToGroup = (1 + otherSidePathCount) * groupSize;
            long tmp = otherSidePathCount;
            otherSidePathCount = (sidePathCount + pathCountToGroup) % Solution.MODULO_BASE;
            sidePathCount = tmp;
        }

        private void finish(int lastPosition) {
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
