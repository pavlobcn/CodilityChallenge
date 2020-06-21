import org.junit.Test;
import org.junit.Assert;

public class SolutionTest {

    @Test
    public void test1() {
        testInternal(10, new int[]{3, 1, 4});
    }

    @Test
    public void test2() {
        testInternal(17, new int[]{5, 3, 2, 4});
    }

    @Test
    public void test3() {
        testInternal(19, new int[]{5, 3, 5, 2, 1});
    }

    @Test
    public void test4() {
        testInternal(35, new int[]{7, 7, 3, 7, 7});
    }

    @Test
    public void test5() {
        testInternal(30, new int[]{1, 1, 7, 6, 6, 6});
    }

    private void testInternal(int expectedResult, int[] h) {
        var actualResult = new Solution().solution(h);
        Assert.assertEquals(expectedResult, actualResult);
    }
}