public class Solution {
    public int solution(String S)
    {
        int result = 0;
        int[] chars = S.chars().toArray();
        int[] doubleCharCountToLeft = getDoubleCharCountToLeft(chars);
        int[] doubleCharCountToRight = reverse(getDoubleCharCountToLeft(reverse(chars)));
        for (int i = 0; i < chars.length; i++)
        {
            // Odd length
            for (int j = 1; i - j >= 0 &&  i + j < chars.length;) {
                if (chars[i - j] == chars[i + j]) {
                    if (i - j - 1 >= 0 && i + j + 1 < chars.length && chars[i - j - 1] == chars[i + j + 1]) {
                        var minSequenceCount = Math.min(doubleCharCountToLeft[i - j], doubleCharCountToRight[i + j]);
                        minSequenceCount = Math.max(minSequenceCount, 1);
                        result += minSequenceCount * 2;
                        j += minSequenceCount * 2;
                    }
                    else {

                        result++;
                        j++;
                    }
                }
                else
                {
                    break;
                }
            }
            // Even length
            for (int j = 1; i - j >= 0 &&  i + j - 1 < chars.length;) {
                if (chars[i - j] == chars[i + j - 1]) {
                    if (i - j - 1 >= 0 && i + j < chars.length && chars[i - j - 1] == chars[i + j]) {
                        var minSequenceCount = Math.min(doubleCharCountToLeft[i - j], doubleCharCountToRight[i + j - 1]);
                        minSequenceCount = Math.max(minSequenceCount, 1);
                        result += minSequenceCount * 2;
                        j += minSequenceCount * 2;
                    }
                    else {
                        result++;
                        j++;
                    }
                    if (result > 100000000) {
                        return -1;
                    }
                }
                else
                {
                    break;
                }
            }
        }
        return result;
    }

    private int[] getDoubleCharCountToLeft(int[] chars) {
        int[] doubleCharCountToLeft = new int[chars.length];
        for (int i = 3; i < chars.length; i++) {
            if (chars[i - 2] == chars[i] && chars[i - 3] == chars[i - 1])
            {
                doubleCharCountToLeft[i] = doubleCharCountToLeft[i - 2] + 1;
            }
        }
        return doubleCharCountToLeft;
    }

    private static int[] reverse(int[] array) {
        var result = new int[array.length];
        for (int i = 0; i < array.length; i++) {
            result[array.length - i - 1] = array[i];
        }
        return result;
    }
}
