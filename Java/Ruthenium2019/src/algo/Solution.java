package algo;

import java.util.ArrayList;
import java.util.Collections;

public class Solution {
    public int solution(int[] A, int K) {
        ArrayList<Integer> amounts = getAmountsToTheRight(A, K);
        int max = getMaximum(amounts);
        return max;
    }

    private ArrayList<Integer> getAmountsToTheRight(int[] a, int k) {
        ArrayList<Integer> amounts = new ArrayList();
        for (int i = 0; i < a.length; i++)
        {
            int amount = getSequenceToTheRightLength(i, a, k);
            amounts.add(amount);
        }
        return amounts;
    }

    private int getSequenceToTheRightLength(int startIndex, int[] a, int k) {
        int sequenceLength = a.length - startIndex;
        int baseNumber = a[startIndex];
        int differentNumbersCount = 0;
        for (int i = startIndex + 1; i < a.length; i++)
        {
            if (a[i] == baseNumber)
            {
                continue;
            }

            differentNumbersCount++;
            if (differentNumbersCount == k + 1)
            {
                sequenceLength = i - startIndex;
                break;
            }
        }
        if (differentNumbersCount < k) {
            // adjust by left side
            sequenceLength += Math.min(k - differentNumbersCount, startIndex);
        }
        return sequenceLength;
    }

    private int getMaximum(ArrayList<Integer> amounts) {
        return Collections.max(amounts);
    }
}
