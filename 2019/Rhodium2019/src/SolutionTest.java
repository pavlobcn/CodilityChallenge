import org.junit.Test;
import org.junit.Assert;

public class SolutionTest {

    @Test
    public void test1() {
        testInternal(12, new int[]{2, 0, 2, 2, 1, 0});
    }

    @Test
    public void test2() {
        testInternal(9, new int[]{5, 5, 0, 5, 5, 5});
    }

    @Test
    public void test3() {
        testInternal(14, new int[]{0, 3, 1, 0, 0, 3, 5});
    }

    @Test
    public void testLineOfLength1() {
        testInternal(3, new int[]{1, 1});
    }

    @Test
    public void testLineOfLength2() {
        testInternal(6, new int[]{1, 2, 2});
    }

    @Test
    public void testLineWithStartInTheMiddle() {
        testInternal(5, new int[]{2, 2, 2});
    }

    private void testInternal(int expectedResult, int[] t) {
        var actualResult = new Solution().solution(t);
        Assert.assertEquals(expectedResult, actualResult);
    }
}