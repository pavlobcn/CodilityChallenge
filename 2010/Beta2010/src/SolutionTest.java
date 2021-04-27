import org.junit.Test;
import org.junit.Assert;

public class SolutionTest {

    @Test
    public void test1() {
        testInternal(11, new int[]{1, 5, 2, 1, 4, 0});
    }

    @Test
    public void test2() {
        testInternal(0, new int[]{});
    }

    @Test
    public void test3() {
        testInternal(2, new int[]{1, 2147483647, 0});
    }

    private void testInternal(int expectedResult, int[] a) {
        var actualResult = new Solution().solution(a);
        Assert.assertEquals(expectedResult, actualResult);
    }
}