package algo;

import java.util.HashMap;
import java.util.Map;

public class Solution {
    int[] a;
    int k;
    int result = 0;
    int startIndex = 0;
    int endIndex = 0;

    public int solution(int[] A, int K) {
        a  = A;
        k = K;

        for (int i = 0; i < A.length; i++) {
            ExtendSequence();
        }
        return result;
    }

    private void ExtendSequence() {
        addNextElement();
        if (!validate()) {
            adjust();
        }

        result = Math.max(result, endIndex - startIndex);
    }

    private void addNextElement() {
        endIndex++;
    }

    private boolean validate() {
        int numberWithTheHighestEntryAmount = getNumberWithTheHighestEntryAmount();
        int differentNumberCount = getDifferentNumberCount(a, numberWithTheHighestEntryAmount);
        if (differentNumberCount > k)
        {
            return false;
        }

        return true;
    }

    private int getNumberWithTheHighestEntryAmount() {
        Map<Integer,Integer> numberCountMap = new HashMap<>();
        int numberWithTheHighestEntryAmount = -1;
        int highestEntryAmount = 0;
        for (int i = startIndex; i < endIndex; i++) {
            int entryAmount = numberCountMap.containsKey(a[i]) ? numberCountMap.get(a[i]) + 1 : 1;
            numberCountMap.put(a[i], entryAmount);
            if (entryAmount > highestEntryAmount)
            {
                highestEntryAmount = entryAmount;
                numberWithTheHighestEntryAmount = a[i];
            }
        }
        return numberWithTheHighestEntryAmount;
    }

    private int getDifferentNumberCount(int[] a, int numberWithTheHighestEntryAmount) {
        int differentNumberCount = 0;
        for (int i = startIndex; i < endIndex; i++)
        {
            if (a[i] != numberWithTheHighestEntryAmount)
            {
                differentNumberCount++;
            }
        }
        return differentNumberCount;
    }

    private void adjust() {
        startIndex++;
        if (!validate())
        {
            adjust();
        }
    }
}
