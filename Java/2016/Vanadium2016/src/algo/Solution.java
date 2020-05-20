package algo;

public class Solution {
    private static final int MODULO_BASE = 2 * 1_000_000_007;
    private int[] charCheckers;

    public int solution(String S) {
        initCharCheckers();
        long[] states = getStates(S);
        long evenResults = getEvenResults(states);
        long oddResults = getOddResults(states);
        return (int) ((evenResults + oddResults) % MODULO_BASE / 2);
    }

    private long getOddResults(long[] states) {
        long results = 0;
        for (int i = 0; i < states.length; i++) {
            long currentCount = states[i];
            if (currentCount == 0) {
                continue;
            }
            for (int charChecker : charCheckers) {
                long count = states[i ^ charChecker];
                results = (results + currentCount * count) % MODULO_BASE;
            }
        }
        return results;
    }

    private long getEvenResults(long[] states) {
        long results = 0;
        for (long count : states) {
            if (count == 0) {
                continue;
            }
            results = (results + count * (count - 1)) % MODULO_BASE;
        }
        return results;
    }

    private long[] getStates(String s) {
        var states = new long[1025];
        var previousState = 0;
        states[previousState] = 1;
        for (int i = 0; i < s.length(); i++) {
            int character = Integer.parseInt(s.substring(i, i + 1));
            int state = previousState ^ charCheckers[character];
            states[state]++;
            previousState = state;
        }
        return states;
    }

    private void initCharCheckers() {
        charCheckers = new int[10];
        charCheckers[0] = 1;
        for (int i = 1; i < 10; i++) {
            charCheckers[i] = 2 * charCheckers[i - 1];
        }
    }
}
