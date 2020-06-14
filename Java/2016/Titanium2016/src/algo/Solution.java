package algo;

import java.util.Stack;

public class Solution {
    private static final char OPENING = '(';
    private static final char CLOSING = ')';

    public int solution(String S, int K) {
        char[] sequence = removeValidBrackets(S);
        return getLongestSequenceLength(sequence, K);
    }

    private char[] removeValidBrackets(String s) {
        char[] validBrackets = new char[s.length()];
        Stack<Integer> stack = new Stack<>();
        for (int i = 0; i < s.length(); i++) {
            char c = s.charAt(i);
            if (c == OPENING) {
                stack.add(i);
            } else {
                if (stack.empty())
                {
                    validBrackets[i] = c;
                } else {
                    stack.pop();
                }
            }
        }
        for (int position : stack) {
            validBrackets[position] = OPENING;
        }
        return validBrackets;
    }

    private int getLongestSequenceLength(char[] sequence, int k) {
        int invalidBracesCount = 0;
        for (char c : sequence) {
            if (c == OPENING || c == CLOSING) {
                invalidBracesCount++;
            }
        }
        if (invalidBracesCount == 0) {
            return sequence.length;
        }
        int[] bracketsToTheLeft = new int[invalidBracesCount + 1];
        int position = 1;
        for (int i = 0; i < sequence.length; i++) {
            if (sequence[i] == OPENING || sequence[i] == CLOSING) {
                bracketsToTheLeft[position] = i + 1;
                position++;
            }
        }
        int[] bracketsToTheRight = new int[invalidBracesCount + 1];
        bracketsToTheRight[bracketsToTheRight.length - 1] = sequence.length;
        position = bracketsToTheRight.length - 2;
        for (int i = sequence.length - 1; i >= 0; i--) {
            if (sequence[i] == OPENING || sequence[i] == CLOSING) {
                bracketsToTheRight[position] = i;
                position--;
            }
        }
        return getLongestSequenceByReversedCounts(bracketsToTheLeft, bracketsToTheRight, k);
    }

    private int getLongestSequenceByReversedCounts(int[] bracketsToTheLeft, int[] bracketsToTheRight, int k) {
        int max = 0;
        for (int i = 0; i < bracketsToTheLeft.length; i++) {
            int j = i + 2 * k;
            if (j >= bracketsToTheRight.length) {
                break;
            }
            int length = bracketsToTheRight[j] - bracketsToTheLeft[i];
            if (length > max) {
                max = length;
            }
        }
        if (max == 0 && k > 0) {
            max = bracketsToTheLeft[bracketsToTheLeft.length - 1] - bracketsToTheLeft[0];
            if (max % 2 == 1) {
                max = max -1;
            }
        }
        return max;
    }
}
