import java.util.*;

public class Solution {
    private static final int MODULO_BASE = 1_000_000_007;

    public int solution(int[] H) {
        Point[] points = getOrderedPoints(H);
        visitAllPoints(points);
        return getResult(points);
    }

    private void visitAllPoints(Point[] points) {
        Point prevPoint = points[0];
        for (int i = 1; i < points.length; i++) {
            Point nextPoint = points[i];
            int prevPosition = prevPoint.position;
            int nextPosition = nextPoint.position;
            if (prevPosition < nextPosition) {
                while (true) {
                    if (prevPoint.right == null) {
                        prevPoint.right = nextPoint;
                        nextPoint.left = prevPoint;
                        break;
                    }
                    if (prevPoint.right.position > nextPoint.position) {
                        prevPoint.right.left = nextPoint;
                        nextPoint.right = prevPoint.right;
                        nextPoint.left = prevPoint;
                        prevPoint.right = nextPoint;
                        break;
                    }
                    prevPoint.right.move(nextPoint);
                    prevPoint = prevPoint.right;
                }
            }
            else if (prevPosition > nextPosition) {
                while (true) {
                    if (prevPoint.left == null) {
                        prevPoint.left = nextPoint;
                        nextPoint.right = prevPoint;
                        break;
                    }
                    if (prevPoint.left.position < nextPoint.position) {
                        prevPoint.left.right = nextPoint;
                        nextPoint.left = prevPoint.left;
                        nextPoint.right = prevPoint;
                        prevPoint.left = nextPoint;
                        break;
                    }
                    prevPoint.left.move(nextPoint);
                    prevPoint = prevPoint.left;
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

    private static class Point {
        private int height;
        private int position;
        private int sortedPosition;
        private int handledHeight;
        private long sidePathCount;
        private long otherSidePathCount;
        public Point left;
        public Point right;

        private Point(int height, int position) {
            this.height = height;
            this.position = position;
        }

        private int getPathCount() {
            return (int)((otherSidePathCount + sidePathCount + 1) % Solution.MODULO_BASE);
        }

        private void move(Point nextPoint) {
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
