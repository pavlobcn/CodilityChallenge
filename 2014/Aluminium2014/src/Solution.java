public class Solution {
    public int solution(int[] A) {
        int[] sumToLeft = new int[A.length + 1];
        int[] maxToLeft = new int[A.length + 1];
        int[] sumToLeftMinusMaxToLeft = new int[A.length + 1];
        int[] minSumToLeft = new int[A.length + 1];
        int[] minSumToLeftMinusMaxToLeft = new int[A.length + 1];
        for (int i = 0; i < A.length; i++) {
            sumToLeft[i + 1] = sumToLeft[i] + A[i];
            maxToLeft[i + 1] = Math.max(i == 0 ? A[i] : maxToLeft[i], A[i]);
            minSumToLeft[i + 1] = Math.min(minSumToLeft[i], sumToLeft[i + 1]);
            sumToLeftMinusMaxToLeft[i + 1] = sumToLeft[i + 1] - maxToLeft[i + 1];
            minSumToLeftMinusMaxToLeft[i + 1] = Math.min(sumToLeftMinusMaxToLeft[i + 1], minSumToLeftMinusMaxToLeft[i]);
        }
        int[] sumToRight = new int[A.length + 1];
        int[] maxToRight = new int[A.length + 1];
        int[] sumToRightMinusMaxToRight = new int[A.length + 1];
        int[] minSumToRight = new int[A.length + 1];
        int[] minSumToRightMinusMaxToRight = new int[A.length + 1];
        for (int i = A.length - 1; i >= 0; i--) {
            sumToRight[i] = sumToRight[i + 1] + A[i];
            maxToRight[i] = Math.max(i == A.length - 1 ? A[i] : maxToRight[i + 1], A[i]);
            minSumToRight[i] = Math.min(minSumToRight[i + 1], sumToRight[i]);
            sumToRightMinusMaxToRight[i] = sumToRight[i] - maxToRight[i];
            minSumToRightMinusMaxToRight[i] = Math.min(sumToRightMinusMaxToRight[i], minSumToRightMinusMaxToRight[i + 1]);
        }
        int total = sumToLeft[sumToLeft.length - 1];
        int result = A[0];
        for (int i = 0; i < A.length; i++) {
            int noChange = total - minSumToLeft[i] - minSumToRight[i + 1];
            int changeToLeft = i > 0
                    ? total - minSumToLeftMinusMaxToLeft[i] - minSumToRight[i + 1] - A[i]
                    : result;
            int changeToRight = i < A.length - 2
                    ? total - minSumToLeft[i] - minSumToRightMinusMaxToRight[i + 1] - A[i]
                    : result;
            result = Math.max(Math.max(result, noChange), Math.max(changeToLeft, changeToRight));
        }
        return result;
    }
}