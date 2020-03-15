package algo;

import java.util.HashMap;
import java.util.Map;
import java.util.concurrent.atomic.AtomicInteger;

public class Solution {
    int[] a;
    int k;
    int result = 0;
    int startIndex = 0;
    int endIndex = 0;
    Map<Integer,Integer> numberCountMap = new HashMap<>();

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
        int entryAmount = numberCountMap.containsKey(a[endIndex]) ? numberCountMap.get(a[endIndex]) + 1 : 1;
        numberCountMap.put(a[endIndex], entryAmount);

        endIndex++;
    }

    private boolean validate() {
        int numberWithTheHighestEntryAmount = getNumberWithTheHighestEntryAmount();
        int differentNumberCount = getDifferentNumberCount(numberWithTheHighestEntryAmount);
        if (differentNumberCount > k)
        {
            return false;
        }

        return true;
    }

    private int getNumberWithTheHighestEntryAmount() {
        AtomicInteger numberWithTheHighestEntryAmount = new AtomicInteger(-1);
        AtomicInteger highestEntryAmount = new AtomicInteger();
        numberCountMap.forEach((key,value) -> {
            if (value > highestEntryAmount.get()) {
                highestEntryAmount.set(value);
                numberWithTheHighestEntryAmount.set(key);
            }
        });
        return numberWithTheHighestEntryAmount.get();
    }

    private int getDifferentNumberCount(int numberWithTheHighestEntryAmount) {
        return endIndex - startIndex - numberCountMap.get(numberWithTheHighestEntryAmount);
    }

    private void adjust() {
        if (numberCountMap.get(a[startIndex]) == 1)
        {
            numberCountMap.remove(a[startIndex]);
        }
        else {
            numberCountMap.put(a[startIndex], numberCountMap.get(a[startIndex]) - 1);
        }

        startIndex++;

        if (!validate())
        {
            adjust();
        }
    }
}
