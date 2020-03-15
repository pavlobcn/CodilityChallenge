package algo;

import java.util.HashMap;
import java.util.Map;

public class Solution {
    int[] a;
    int k;
    int result = 0;
    int startIndex = 0;
    int endIndex = 0;
    Map<Integer,Integer> numberCountMap = new HashMap<>();
    int numberWithTheHighestEntryAmount = -1;
    int highestEntryAmount;

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
        if (!validate(true)) {
            adjust();

            while (!validate(false)) {
                adjust();
            }
        }

        result = Math.max(result, endIndex - startIndex);
    }

    private void addNextElement() {
        int entryAmount = numberCountMap.containsKey(a[endIndex]) ? numberCountMap.get(a[endIndex]) + 1 : 1;
        numberCountMap.put(a[endIndex], entryAmount);

        endIndex++;
    }

    private boolean validate(boolean moveForward) {
        int numberWithTheHighestEntryAmount = getNumberWithTheHighestEntryAmount(moveForward);
        int differentNumberCount = getDifferentNumberCount(numberWithTheHighestEntryAmount);
        if (differentNumberCount > k)
        {
            return false;
        }

        return true;
    }

    private int getNumberWithTheHighestEntryAmount(boolean moveForward) {
        if (moveForward && a[endIndex - 1] == numberWithTheHighestEntryAmount)
        {
            return numberWithTheHighestEntryAmount;
        }

        if (!moveForward && a[startIndex - 1] != numberWithTheHighestEntryAmount)
        {
            return numberWithTheHighestEntryAmount;
        }

        highestEntryAmount = 0;
        numberCountMap.forEach((key,value) -> {
            if (value > highestEntryAmount) {
                highestEntryAmount = value;
                numberWithTheHighestEntryAmount = key;
            }
        });

        return numberWithTheHighestEntryAmount;
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
    }
}
