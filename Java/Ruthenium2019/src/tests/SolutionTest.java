package tests;

import algo.Solution;
import org.junit.Test;
import org.junit.Assert;

public class SolutionTest {

    @Test
    public void test1() {
        testInternal(5, new int[]{1, 1, 3, 4, 3, 3, 4}, 2);
    }

    @Test
    public void test2() {
        testInternal(2, new int[]{4, 5, 5, 4, 2, 2, 4}, 0);
    }

    @Test
    public void test3() {
        testInternal(4, new int[]{1, 3, 3, 2}, 2);
    }

    private void testInternal(int expectedResult, int[] a, int k) {
        var actualResult = new Solution().solution(a, k);
        Assert.assertEquals(expectedResult, actualResult);
    }
}