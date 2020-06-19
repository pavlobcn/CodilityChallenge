package tests;

import algo.*;
import org.junit.*;

public class SolutionTest {
    @Test
    public void test1() {
        testInternal(6, ")()()(", 3);
    }

    @Test
    public void test2() {
        testInternal(4, ")))(((", 2);
    }

    @Test
    public void test3() {
        testInternal(0, ")))(((", 0);
    }

    @Test
    public void test4() {
        testInternal(2, "()", 1);
    }

    @Test
    public void test5() {
        testInternal(0, "(", 1);
    }

    @Test
    public void test6() {
        testInternal(0, ")(", 1);
    }

    @Test
    public void test7() {
        testInternal(4, "()(()", 1);
    }

   private void testInternal(int expectedResult, String s, int k) {
        var actualResult = new Solution().solution(s, k);
        Assert.assertEquals(expectedResult, actualResult);
    }
}