import java.util.Arrays;

public class Solution {
    public int solution(int[] A) {
        Arrays.sort(A);
        int result = 0;
        for (int i = A.length - 1; i >= 0; i--) {
            result = Math.abs(Math.abs(A[i]) - result);
        }
        return result;
    }
}