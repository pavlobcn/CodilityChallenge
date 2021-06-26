public class Solution {
    public int solution(int[] A)
    {
        if (A.length == 0) {
            return 0;
        }
        var leftSides = new long[A.length];
        var rightSides = new long[A.length];
        for (int i = 0; i < A.length; i++)
        {
            int leftSide = i - A[i];
            if (leftSide >= 0) {
                leftSides[leftSide]++;
            }
            int rightSide = i + A[i];
            if (A[i] < A.length && rightSide < A.length) {
                rightSides[rightSide]++;
            }
        }
        var leftSideCount = new long[A.length];
        leftSideCount[A.length - 1] = leftSides[A.length - 1];
        for (int i = A.length - 2; i >= 0; i--) {
            leftSideCount[i] = leftSideCount[i + 1] + leftSides[i];
        }
        var rightSideCount = new long[A.length];
        rightSideCount[0] = rightSides[0];
        for (int i = 1; i < A.length; i++) {
            rightSideCount[i] = rightSideCount[i - 1] + rightSides[i];
        }
        int result = 0;
        for (int i = 0; i < A.length; i++) {
            int leftSide = i - A[i];
            if (leftSide > 0) {
                result -= rightSideCount[leftSide - 1];
            }
            int rightSide = i + A[i];
            if (A[i] < A.length && rightSide < A.length - 1) {
                result -= leftSideCount[rightSide + 1];
            }
            result += A.length - 1;
            if (result / 2 > 10000000) {
                return -1;
            }
        }
        return result / 2;
    }
}
