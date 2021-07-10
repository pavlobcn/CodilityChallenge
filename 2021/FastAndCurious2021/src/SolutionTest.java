import org.junit.Assert;
import org.junit.Test;

public class SolutionTest {

    @Test
    public void test1() {
        testInternal(7, new int[]{1, 5, 9, 12});
    }

    @Test
    public void test2() {
        testInternal(0, new int[]{5, 15});
    }

    @Test
    public void test3() {
        testInternal(9, new int[]{2, 6, 7, 8, 12});
    }

    private void testInternal(int expectedResult, int[] a) {
        var actualResult = new Solution().solution(a);
        Assert.assertEquals(expectedResult, actualResult);
    }
}