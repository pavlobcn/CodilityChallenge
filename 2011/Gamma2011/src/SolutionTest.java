import org.junit.Test;
import org.junit.Assert;

public class SolutionTest {

    @Test
    public void test1() {
        testInternal(6, "baababa");
    }

    @Test
    public void test2() {
        testInternal(7, "caabbbaa");
    }

    @Test
    public void test3() {
        testInternal(24, "abababbbabbbaba");
    }

    private void testInternal(int expectedResult, String s) {
        var actualResult = new Solution().solution(s);
        Assert.assertEquals(expectedResult, actualResult);
    }
}