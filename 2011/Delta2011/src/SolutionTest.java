import org.junit.Assert;
import org.junit.Test;

public class SolutionTest {

    @Test
    public void test1() {
        testInternal(0, new int[]{1, 5, 2, -2});
    }

    @Test
    public void test2() {
        testInternal(0, new int[]{3, 3, 3, 4, 5});
    }

    @Test
    public void test3() {
        testInternal(0, new int[]{-50, -50, -50, -50, 4, 4, 4, 4, 18, 18, 18, 18, 99, 99, 99, 99, 100, 100, 100, 100});
    }

    private void testInternal(int expectedResult, int[] a) {
        var actualResult = new Solution().solution(a);
        Assert.assertEquals(expectedResult, actualResult);
    }
}