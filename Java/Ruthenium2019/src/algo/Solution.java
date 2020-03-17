package algo;

import java.util.*;

public class Solution {
    int[] a;
    int k;
    int result = 0;
    int startIndex = 0;
    int endIndex = 0;

    int[] numberCounts;
    ArrayList<Integer> counts;

    public int solution(int[] A, int K) {
        a  = A;
        k = K;
        numberCounts = new int[1000001];
        counts = new ArrayList();
        counts.add(A.length);

        for (int i = 0; i < A.length; i++) {
            ExtendSequence();
        }
        return result;
    }

    private void ExtendSequence() {
        addNextElement();
        while (!validate()) {
            adjust();
        }

        result = Math.max(result, endIndex - startIndex);
    }

    private void addNextElement() {
        int newElement = a[endIndex];
        numberCounts[newElement]++;
        counts.set(numberCounts[newElement] - 1, counts.get(numberCounts[newElement] - 1) - 1);
        if (counts.size() - 1 < numberCounts[newElement])
        {
            counts.add(0);
        }
        counts.set(counts.size() - 1, counts.get(counts.size() - 1) + 1);

        endIndex++;
    }

    private void adjust() {
        int elementToRemove = a[startIndex];
        numberCounts[elementToRemove]--;
        counts.set(numberCounts[elementToRemove] + 1, counts.get(numberCounts[elementToRemove] + 1) - 1);
        if (counts.get(counts.size() - 1) == 0)
        {
            counts.remove(counts.size() - 1);
        }
        counts.set(numberCounts[elementToRemove], counts.get(numberCounts[elementToRemove]) + 1);

        startIndex++;
    }

    private boolean validate() {
        int numberAmountWithTheHighestEntryAmount = getNumberAmountWithTheHighestEntryAmount();
        int differentNumberCount = getDifferentNumberCount(numberAmountWithTheHighestEntryAmount);
        if (differentNumberCount > k)
        {
            return false;
        }

        return true;
    }

    private int getNumberAmountWithTheHighestEntryAmount() {
        return counts.size() - 1;
    }

    private int getDifferentNumberCount(int numberAmountWithTheHighestEntryAmount) {
        return endIndex - startIndex - numberAmountWithTheHighestEntryAmount;
    }
}
