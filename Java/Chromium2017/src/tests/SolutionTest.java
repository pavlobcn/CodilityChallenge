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
    public void testRandomPerformance1() {
        testRandomPerformance(BASE_PERFORMANCE_ELEMENT_COUNT);
    }

    @Test
    public void testRandomPerformance2() {
        testRandomPerformance(2 * BASE_PERFORMANCE_ELEMENT_COUNT);
    }

    @Test
    public void testRandomPerformance3() {
        testRandomPerformance(4 * BASE_PERFORMANCE_ELEMENT_COUNT);
    }

    @Test
    public void testRandomPerformance4() {
        testRandomPerformance(8 * BASE_PERFORMANCE_ELEMENT_COUNT);
    }

    @Test
    public void testPyramidPerformance1() {
        testPyramidPerformance(BASE_PERFORMANCE_ELEMENT_COUNT);
    }

    @Test
    public void testPyramidPerformance2() {
        testPyramidPerformance(2 * BASE_PERFORMANCE_ELEMENT_COUNT);
    }

    @Test
    public void testPyramidPerformance3() {
        testPyramidPerformance(4 * BASE_PERFORMANCE_ELEMENT_COUNT);
    }

    @Test
    public void testPyramidPerformance4() {
        testPyramidPerformance(8 * BASE_PERFORMANCE_ELEMENT_COUNT);
    }

    private void testRandomPerformance(int size) {
        int[] h = getRandomData(size);
        new Solution().solution(h);
    }

    public void testPyramidPerformance(int size) {
        int[] h = getPyramidData(size);
        new Solution().solution(h);
    }

    private int[] getRandomData(int size) {
        int[] h = new int[size];
        for (int i = 0; i < h.length; i++) {
            h[i] = i;
        }
        Collections.shuffle(Arrays.asList(h));
        return h;
    }

    private int[] getPyramidData(int size) {
        int[] h = new int[size];
        int middle = size / 2;
        for (int i = 0; i < h.length; i++) {
            if (i < middle) {
                h[i] = 2 * (middle - i) - 1;
            }
            else if (i >= middle) {
                h[i] = 2 * (i - middle);
            }
        }
        return h;
    }

    private void testInternal(int expectedResult, int[] h) {
        var actualResult = new Solution().solution(h);
        Assert.assertEquals(expectedResult, actualResult);
    }
}