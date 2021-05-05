import org.junit.Test;
import org.junit.Assert;

public class SolutionTest {

    @Test
    public void test1() {
        testInternal(3, new int[]{2, 3, 3, 4}, 3, 1);
    }

    @Test
    public void test2() {
        testInternal(4, new int[]{1, 4, 5, 5}, 6, 4);
    }

    @Test
    public void test3() {
        testInternal(4, new int[]{5, 2, 5, 2}, 8, 1);
    }

    @Test
    public void test4() {
        testInternal(2, new int[]{1, 5, 5}, 2, 4);
    }

    private void testInternal(int expectedResult, int[] a, int l, int r) {
        var actualResult = new Solution().solution(a, l, r);
        Assert.assertEquals(expectedResult, actualResult);
    }
}