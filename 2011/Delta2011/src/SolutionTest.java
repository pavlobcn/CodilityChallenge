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

    private void testInternal(int expectedResult, int[] a) {
        var actualResult = new Solution().solution(a);
        Assert.assertEquals(expectedResult, actualResult);
    }
}