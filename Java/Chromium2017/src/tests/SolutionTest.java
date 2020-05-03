package tests;

import algo.*;
import org.junit.*;

public class SolutionTest {

    @Test
    public void test1() {
        testInternal(7, new int[]{13, 2, 5});
    }

    @Test
    public void test2() {
        testInternal(23, new int[]{4, 6, 2, 1, 5});
    }

    private void testInternal(int expectedResult, int[] h) {
        var actualResult = new Solution().solution(h);
        Assert.assertEquals(expectedResult, actualResult);
    }
}