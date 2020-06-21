public class Solution {
    public int solution(int[] H) {
        int[] leftBannerSquares = getBannerSquares(H);
        int[] rightBannerSquares = reverse(getBannerSquares(reverse(H)));
        int result = getMinimumSquare(leftBannerSquares, rightBannerSquares);
        return result;
    }

    private int getMinimumSquare(int[] leftBannerSquares, int[] rightBannerSquares) {
        int minimumSquare = Integer.MAX_VALUE;
        for (int i = 0; i < leftBannerSquares.length; i++)
        {
            minimumSquare = Math.min(minimumSquare, leftBannerSquares[i] + rightBannerSquares[i]);
        }
        return minimumSquare;
    }

    private int[] getBannerSquares(int[] h) {
        int maxHeight = 0;
        int[] bannerSquares = new int[h.length + 1];
        bannerSquares[0] = 0;
        for (int i = 0; i < h.length; i++)
        {
            maxHeight = Math.max(maxHeight, h[i]);
            bannerSquares[i + 1] = maxHeight * (i + 1);
        }
        return bannerSquares;
    }

    private int[] reverse(int[] array)
    {
        int[] reversedArray = new int[array.length];
        for (int i = 0; i < array.length; i++)
        {
            reversedArray[i] = array[array.length - 1 - i];
        }
        return reversedArray;
    }
}
