package tests;

import algo.*;
import org.junit.*;

import java.util.Arrays;
import java.util.Collections;

public class SolutionTest {
    private static final int BASE_PERFORMANCE_ELEMENT_COUNT = 10000;
    @Test
    public void test1() {
        testInternal(7, new int[]{13, 2, 5});
    }

    @Test
    public void test2() {
        testInternal(23, new int[]{4, 6, 2, 1, 5});
    }

    @Test
    public void testPerformance1() {
        testPerformance(BASE_PERFORMANCE_ELEMENT_COUNT);
    }

    @Test
    public void testPerformance2() {
        testPerformance(2 * BASE_PERFORMANCE_ELEMENT_COUNT);
    }

    @Test
    public void testPerformance3() {
        testPerformance(4 * BASE_PERFORMANCE_ELEMENT_COUNT);
    }

    @Test
    public void testPerformance4() {
        testPerformance(8 * BASE_PERFORMANCE_ELEMENT_COUNT);
    }

    public void testPerformance(int size) {
        int[] h = new int[size];
        for (int i = 0; i < h.length; i++) {
            h[i] = i;
        }
        Collections.shuffle(Arrays.asList(h));
        new Solution().solution(h);
    }

    private void testInternal(int expectedResult, int[] h) {
        var actualResult = new Solution().solution(h);
        Assert.assertEquals(expectedResult, actualResult);
    }
}