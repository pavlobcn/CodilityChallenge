import org.junit.Assert;
import org.junit.Test;

public class SolutionTest {

    @Test
    public void test1() {
        testInternal("adcede", "decade", 4);
    }

    @Test
    public void test2() {
        testInternal("babbbbb", "bbbabbb", 2);
    }

    @Test
    public void test3() {
        testInternal("aaaaabrcdbr", "abracadabra", 15);
    }

    private void testInternal(String expectedResult, String s, int k) {
        var actualResult = new Solution().solution(s, k);
        Assert.assertEquals(expectedResult, actualResult);
    }
}