import org.junit.*;

public class SolutionTest {
    @Test
    public void test1() {
        testInternal(9, new int[]{3, 2, -6, 3, 1});
    }

    @Test
    public void test2() {
        testInternal(-10000, new int[]{-10000});
    }

    @Test
    public void test3() {
        testInternal(-10000, new int[]{-10000, -10000});
    }

    @Test
    public void test4() {
        testInternal(4, new int[]{2, -2, -2, 2});
    }

    @Test
    public void test5() {
        testInternal(-2, new int[]{-2, -2, -2});
    }

    @Test
    public void test6() {
        testInternal(-2, new int[]{-2, -2, -2, -2});
    }

    @Test
    public void test7() {
        testInternal(8, new int[]{2, 2, 2, 2});
    }

    @Test
    public void test8() {
        testInternal(10, new int[]{2, 2, 2, 2, 2});
    }

    private void testInternal(int expectedResult, int[] a) {
        var actualResult = new Solution().solution(a);
        Assert.assertEquals(expectedResult, actualResult);
    }
}