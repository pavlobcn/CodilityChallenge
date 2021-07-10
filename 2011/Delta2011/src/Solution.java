import java.util.Arrays;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.stream.Collectors;

public class Solution {
    private static final int POSITIVE = 1;
    private static final int NEGATIVE = -1;

    public int solution(int[] A) {
        if (isAllEntriesEven(A)) {
            return 0;
        }
        List<Point> points = getPoints(A);
        Summary summary = setInitialSign(points);
        while (true) {
            ChangeInfo changeInfo = getChangeInfo(points, summary);
            if (changeInfo.changeSign) {
                changeSign(points, summary, changeInfo.changeSignIndex);
                continue;
            }
            if (changeInfo.swap) {
                changeSign(points, summary, changeInfo.swapIndex1);
                changeSign(points, summary, changeInfo.swapIndex2);
                continue;
            }
            break;
        }
        if (summary.getResult() == 8) {
            throw new RuntimeException(Arrays.toString(A));
        }
        return summary.getResult();
    }

    private boolean isAllEntriesEven(int[] a) {
        int[] entries = new int[101];
        for (int i : a) {
            entries[Math.abs(i)]++;
        }
        return Arrays.stream(entries).allMatch(x -> x % 2 == 0);
    }

    private void changeSign(List<Point> points, Summary summary, int changeSignIndex) {
        Point point = points.get(changeSignIndex);
        summary.positiveSum -= point.sign * point.value;
        point.sign *= NEGATIVE;
    }

    private ChangeInfo getChangeInfo(List<Point> points, Summary summary) {
        int currentResult = summary.getResult();
        var changeInfo = new ChangeInfo();
        for (int i = 0; i < points.size(); i++) {
            Point point = points.get(i);
            int newPositiveSum = summary.positiveSum - point.sign * point.value;
            int newResult = summary.getResult(newPositiveSum);
            if (newResult < currentResult) {
                changeInfo.changeSign = true;
                changeInfo.changeSignIndex = i;
                currentResult = newResult;
            }
        }
        if (changeInfo.changeSign) {
            return changeInfo;
        }

        Map<Integer, Integer> positiveMap = getMap(points, POSITIVE);
        Map<Integer, Integer> negativeMap = getMap(points, NEGATIVE);
        for (Map.Entry<Integer, Integer> positiveEntry : positiveMap.entrySet()) {
            for (Map.Entry<Integer, Integer> negativeEntry : negativeMap.entrySet()) {
                Point positivePoint = points.get(positiveEntry.getValue());
                Point negativePoint = points.get(negativeEntry.getValue());
                int newPositiveSum = summary.positiveSum - positivePoint.sign * positivePoint.value;
                newPositiveSum = newPositiveSum - negativePoint.sign * negativePoint.value;
                int newResult = summary.getResult(newPositiveSum);
                if (newResult < currentResult) {
                    changeInfo.swap = true;
                    changeInfo.swapIndex1 = positiveEntry.getValue();
                    changeInfo.swapIndex2 = negativeEntry.getValue();
                    currentResult = newResult;
                }
            }
        }
        return changeInfo;
    }

    private Map<Integer, Integer> getMap(List<Point> points, int sign) {
        Map<Integer, Integer> map = new HashMap<>(100);
        for (int i = 0; i < points.size(); i++) {
            Point point = points.get(i);
            if (point.sign == sign && !map.containsValue(point.value)) {
                map.put(point.value, i);
            }
        }
        return map;
    }

    private Summary setInitialSign(List<Point> points) {
        int sum = points.stream().map(x -> x.value).reduce(0, Integer::sum);
        int positiveSum = 0;
        for (Point point : points) {
            if (positiveSum * 2 < sum) {
                point.sign = POSITIVE;
                positiveSum += point.value;
            } else {
                point.sign = NEGATIVE;
            }
        }
        return new Summary(sum, positiveSum);
    }

    private static List<Point> getPoints(int[] a) {
        List<Point> points = Arrays.stream(a).map(Math::abs).sorted().mapToObj(x -> new Point(Math.abs(x))).collect(Collectors.toList());
        for (int i = 0; i < points.size(); i++) {
            points.get(i).position = i;
        }
        return points;
    }
}

class ChangeInfo {
    public boolean changeSign;
    public int changeSignIndex;
    public boolean swap;
    public int swapIndex1;
    public int swapIndex2;
}

class Summary {
    public int sum;
    public int positiveSum;

    public Summary(int sum, int positiveSum) {
        this.sum = sum;
        this.positiveSum = positiveSum;
    }

    public int getResult() {
        return getResult(positiveSum);
    }

    public int getResult(int positiveSum) {
        return Math.abs(positiveSum * 2 - sum);
    }
}

class Point {
    public int value;
    public int position;
    public int sign;

    public Point(int value) {
        this.value = value;
    }
}