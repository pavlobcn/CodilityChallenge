package algo;

import java.util.*;

public class Solution {
    private final int MODULE_BASE = 1000000007;

    public int solution(int[] H) {
        List<Point> points = getPoints(H);
        sort(points);
        long result = 0;
        for (int startPointIndex = 0; startPointIndex < points.size(); startPointIndex++) {
            long pathCount = getPathCount(points, startPointIndex);
            result += pathCount;
        }
        return (int) (result % MODULE_BASE);
    }

    private long getPathCount(List<Point> points, int startPointIndex) {
        List<Integer> groupSizes = getGroupSizes(points, startPointIndex);
        long pathCount = 0;
        long otherSidePathCount = 0;
        long sidePathCount = 0;
        for (int i = 0; i < groupSizes.size(); i++) {
            // process group and increment pathCount
            int groupSize = groupSizes.get(i);
            long pathCountToGroup = groupSize * (1 + otherSidePathCount);
            sidePathCount += pathCountToGroup;
            pathCount += pathCountToGroup;

            // swap sides
            long tmp = otherSidePathCount;
            otherSidePathCount = sidePathCount;
            sidePathCount = tmp;
        }
        return pathCount + 1; // 1 - path with single start point
    }

    private List<Integer> getGroupSizes(List<Point> points, int startPointIndex) {
        var sizes = new ArrayList<Integer>();
        Point startPoint = points.get(startPointIndex);
        for (int i = startPointIndex + 1; i < points.size(); i++) {
            if (sizes.size() == 0) {
                sizes.add(1);
            } else {
                boolean sameSide =
                        (points.get(i).position - startPoint.position) *
                                (points.get(i - 1).position - startPoint.position) > 0;
                if (sameSide) {
                    sizes.set(sizes.size() - 1, sizes.get(sizes.size() - 1) + 1);
                } else {
                    sizes.add(1);
                }
            }
        }
        return sizes;
    }

    private void sort(List<Point> points) {
        Collections.sort(points, (Point p1, Point p2) -> p1.height - p2.height);
    }

    private List<Point> getPoints(int[] h) {
        List<Point> points = new ArrayList<Point>();
        for (int i = 0; i < h.length; i++) {
            points.add(new Point(h[i], i));
        }
        return points;
    }

    private class Point {
        public int height;
        public int position;

        public Point(int height, int position) {
            this.height = height;
            this.position = position;
        }
    }
}
