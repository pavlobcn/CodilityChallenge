import org.junit.Test;
import org.junit.Assert;

public class SolutionTest {

    @Test
    public void test1() {
        testInternal(3, 5, 3, new int[]{1, 1, 4, 1, 4}, new int[]{5, 2, 5, 5, 4}, new int[]{1, 2, 2, 3, 3});
    }

    @Test
    public void test2() {
        testInternal(2, 6, 4, new int[]{1, 2, 1, 1}, new int[]{3, 3, 6, 6}, new int[]{1, 2, 3, 4});
    }

    @Test
    public void test3() {
        testInternal(1, 3, 2, new int[]{1, 3, 3, 1, 1}, new int[]{2, 3, 3, 1, 2}, new int[]{1, 2, 1, 2, 2});
    }

    @Test
    public void test4() {
        testInternal(3, 5, 2, new int[]{1, 1, 2}, new int[]{5, 5, 3}, new int[]{1, 2, 1});
    }

    @Test
    public void test5() {
        testInternal(0, 1, 2, new int[]{1, 1}, new int[]{1, 1}, new int[]{2, 1});
    }

    /*
    @Test
    public void test6() {
        testInternal(4, 5, 2, new int[]{1, 2, 3, 4, 5, 1, 2, 4, 5, 3, 2}, new int[]{1, 2, 3, 4, 5, 1, 2, 4, 5, 3, 5}, new int[]{1, 1, 1, 1, 1, 2, 2, 2, 2, 1, 2});
    }
     */

    private void testInternal(int expectedResult, int n, int k, int[] a, int[] b, int[] c) {
        var actualResult = new Solution().solution(n, k, a, b, c);
        Assert.assertEquals(expectedResult, actualResult);
    }
}