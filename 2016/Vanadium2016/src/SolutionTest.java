import org.junit.*;

public class SolutionTest {
    @Test
    public void test1() {
        testInternal(11, "02002");
    }

   private void testInternal(int expectedResult, String s) {
        var actualResult = new Solution().solution(s);
        Assert.assertEquals(expectedResult, actualResult);
    }
}