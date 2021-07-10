import java.util.Optional;
import java.util.stream.Stream;

public class Solution {
    private static final long REMINDER = 1_000_000_007;

    public int solution(int[] A) {
        long[] leftRoads = getLeftRoads(A);
        long[] rightRoads = getRightRoads(A);
        Optional<Long> min = Stream
                .iterate(0, x -> x + 1).limit(A.length - 1)
                .map(i -> leftRoads[i] + rightRoads[i + 1])
                .min(Long::compareTo);
        return min.map(x -> (int) (x % REMINDER)).orElse(0);
    }

    private long[] getLeftRoads(int[] a) {
        var leftRoads = new long[a.length];
        for (int i = 1; i < a.length; i++) {
            leftRoads[i] = leftRoads[i - 1] + i * ((long) a[i] - a[i - 1]);
        }
        return leftRoads;
    }

    private long[] getRightRoads(int[] a) {
        var rightRoads = new long[a.length];
        for (int i = a.length - 2; i >= 0; i--) {
            rightRoads[i] = rightRoads[i + 1] + a[a.length - 1] - a[i];
        }
        return rightRoads;
    }
}