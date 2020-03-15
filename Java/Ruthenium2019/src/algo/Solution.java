package algo;

import java.util.*;

public class Solution {
    int[] a;
    int k;
    int result = 0;
    int startIndex = 0;
    int endIndex = 0;

    // key - number, value - entries of this number in current sequence
    Map<Integer,Integer> numberCountMap = new HashMap<>();
    // key - entries of the number in the sequence, value - numbers with 'key' entries in current sequence
    Map<Integer, HashSet<Integer>> entriesMap = new HashMap();

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
        while (!validate()) {
            adjust();
        }

        result = Math.max(result, endIndex - startIndex);
    }

    private void addNextElement() {
        int newElement = a[endIndex];
        int currentEntryAmount = numberCountMap.getOrDefault(newElement, 0);
        int newEntryAmount = currentEntryAmount + 1;
        numberCountMap.put(newElement, newEntryAmount);

        if (currentEntryAmount > 0)
        {
            var numbersWithEntryCount = entriesMap.get(currentEntryAmount);
            if (numbersWithEntryCount.size() == 1) {
                entriesMap.remove(currentEntryAmount);
            }
            else {
                numbersWithEntryCount.remove(newElement);
            }
        }

        var numbersWithEntryCount = entriesMap.containsKey(newEntryAmount) ? entriesMap.get(newEntryAmount) : new HashSet();
        numbersWithEntryCount.add(newElement);
        entriesMap.put(newEntryAmount, numbersWithEntryCount);

        endIndex++;
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
        return Collections.max(entriesMap.keySet());
    }

    private int getDifferentNumberCount(int numberAmountWithTheHighestEntryAmount) {
        return endIndex - startIndex - numberAmountWithTheHighestEntryAmount;
    }

    private void adjust() {
        int elementToRemove = a[startIndex];
        int numberCount = numberCountMap.get(elementToRemove);

        var numbersWithEntryCount = entriesMap.get(numberCount);
        if (numbersWithEntryCount.size() == 1)
        {
            entriesMap.remove(numberCount);
        }
        else
        {
            numbersWithEntryCount.remove(numberCount);
        }
        numbersWithEntryCount = entriesMap.containsKey(numberCount - 1) ? entriesMap.get(numberCount - 1) : new HashSet();
        numbersWithEntryCount.add(elementToRemove);
        entriesMap.put(numberCount - 1, numbersWithEntryCount);

        if (numberCountMap.get(elementToRemove) == 1) {
            numberCountMap.remove(elementToRemove);
        }
        else {
            numberCountMap.put(elementToRemove, numberCountMap.get(elementToRemove) - 1);
        }

        startIndex++;
    }
}
