import org.junit.Test;
import org.junit.Assert;

public class SolutionTest {

    @Test
    public void test1() {
        testInternal("997952", new int[][]{{9,9,7}, {9,7,2}, {6,9,5}, {9,1,2}});
    }

    private void testInternal(String expectedResult, int[][] a) {
        var actualResult = new Solution().solution(a);
        Assert.assertEquals(expectedResult, actualResult);
    }
}