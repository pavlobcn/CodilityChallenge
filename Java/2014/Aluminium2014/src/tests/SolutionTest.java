package tests;

import algo.*;
import org.junit.*;

public class SolutionTest {
    @Test
    public void test1() {
        testInternal(9, new int[]{3, 2, -6, 3, 1});
    }

    private void testInternal(int expectedResult, int[] a) {
        var actualResult = new Solution().solution(a);
        Assert.assertEquals(expectedResult, actualResult);
    }
}