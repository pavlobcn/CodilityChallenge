package algo;

public class Solution {
    int result = 0;
    int[][] edges;

    public int solution(int[] T) {
        prepareEdges(T);
        countEdgesSum();
        calculateResult();
        return result;
    }

    private void countEdgesSum() {
        for (int j = 1; j < edges.length; j++) {
            for (int i = j - 2; i >= 0; i--) {
                edges[i][j] = edges[i][j] + edges[i + 1][j];
            }
        }

        for (int i = 0; i < edges.length - 1; i++) {
            for (int j = i + 1; j < edges.length; j++) {
                edges[i][j] = edges[i][j] + edges[i][j - 1];
            }
        }
    }

    private void prepareEdges(int[] t) {
        edges = new int[t.length][t.length];
        for (int i = 0; i < t.length; i++) {
            if (t[i] < i)
            {
                edges[t[i]][i] = 1;
            }
            else if (t[i] > i)
            {
                edges[i][t[i]] = 1;
            }
        }
    }

    private void calculateResult() {
        for (int i = 0; i < edges.length; i++) {
            for (int j = i; j < edges.length; j++) {
                if (edges[i][j] == j - i)
                {
                    result++;
                }
            }
        }
    }
}