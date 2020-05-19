package algo;

import java.util.*;

public class Solution {
    private static final int MODULO_BASE = 2 * 1_000_000_007;
    private int[] charCheckers;

    public int solution(String S) {
        initCharCheckers();
        Map<Integer, Integer> states = getStates(S);
        long evenResults = getEvenResults(states);
        long oddResults = getOddResults(states);
        return (int) ((evenResults + oddResults) % MODULO_BASE / 2);
    }

    private long getOddResults(Map<Integer, Integer> states) {
        long results = 0;
        for (Map.Entry<Integer, Integer> entry : states.entrySet()) {
            for (int charChecker : charCheckers) {
                int count = states.getOrDefault(entry.getKey() ^ charChecker, 0);
                results = (results + entry.getValue() * count) % MODULO_BASE;
            }
        }
        return results;
    }

    private long getEvenResults(Map<Integer, Integer> states) {
        long results = 0;
        for (Integer count : states.values()) {
            results = (results + count * (count - 1)) % MODULO_BASE;
        }
        return results;
    }

    private Map<Integer, Integer> getStates(String s) {
        var states = new Hashtable<Integer, Integer>();
        var previousState = 0;
        states.put(previousState, 1);
        for (int i = 0; i < s.length(); i++) {
            int character = Integer.parseInt(s.substring(i, i + 1));
            int state = previousState ^ charCheckers[character];
            int count = states.getOrDefault(state, 0);
            states.put(state, count + 1);
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
