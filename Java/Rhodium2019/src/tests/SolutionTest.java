package tests;

import algo.Solution;
import org.junit.Test;
import org.junit.Assert;

public class SolutionTest {

    @Test
    public void test1() {
        testInternal(12, new int[]{2, 0, 2, 2, 1, 0});
    }

    private void testInternal(int expectedResult, int[] t) {
        var actualResult = new Solution().solution(t);
        Assert.assertEquals(expectedResult, actualResult);
    }
}