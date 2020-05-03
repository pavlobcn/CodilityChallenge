package algo;

import java.lang.reflect.Array;
import java.util.*;

public class Solution {
    private final int MODULE_BASE = 1000000007;

    public int solution(int[] H) {
        long startTime1 = System.currentTimeMillis();
        ArrayList<Point> points = getPoints(H);
        long startTime2 = System.currentTimeMillis();
        sort(points);
        long startTime3 = System.currentTimeMillis();
        long result = 0;
        for (int startPointIndex = 0; startPointIndex < points.size(); startPointIndex++) {
            long pathCount = getPathCount(points, startPointIndex);
            result += pathCount;
        }
        long startTime4 = System.currentTimeMillis();
        System.out.println("1: " + String.valueOf(startTime2 - startTime1));
        System.out.println("2: " + String.valueOf(startTime3 - startTime2));
        System.out.println("3: " + String.valueOf(startTime4 - startTime3));
        return (int) (result % MODULE_BASE);
    }

    private long getPathCount(ArrayList<Point> points, int startPointIndex) {
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

    private List<Integer> getGroupSizes(ArrayList<Point> points, int startPointIndex) {
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
        public int height;
        public int position;

        public Point(int height, int position) {
            this.height = height;
            this.position = position;
        }
    }
}
