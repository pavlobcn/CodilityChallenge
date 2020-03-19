package algo;

import java.util.ArrayList;

public class Solution {
    int result = 0;
    boolean[][] missingPaths;
    boolean[][] suspectedIncomingConnections;
    boolean[][] suspectedOutcomingConnections;
    Node[] nodes;

    public int solution(int[] T) {
        nodes = prepareTree(T);
        missingPaths = new boolean[T.length][T.length];
        suspectedIncomingConnections = new boolean[T.length][T.length];
        suspectedOutcomingConnections = new boolean[T.length][T.length];
        for (int i = 0; i < nodes.length; i++) {
            checkIncoming(i);
            checkOutcoming(i);
        }
        checkSuspectedConnections();
        calculateResult();
        return result;
    }

    private void checkSuspectedConnections() {
        for (int i = 0; i < missingPaths.length; i++) {
            for (int j = i + 1; j < missingPaths.length; j++) {
                if (suspectedIncomingConnections[i][j] && missingPaths[i][j - 1])
                {
                    removePaths(i, j);
                }
                if (suspectedOutcomingConnections[i][j] && missingPaths[i + 1][j])
                {
                    removePaths(i, j);
                }
            }
        }
    }

    private void calculateResult() {
        for (int i = 0; i < missingPaths.length; i++) {
            for (int j = i; j < missingPaths.length; j++) {
                if (!missingPaths[i][j])
                {
                    result++;
                }
            }
        }
    }

    private void checkIncoming(int i) {
        int maxIncoming = -1;
        for (int j = 0; j < nodes[i].edges.size(); j++) {
            if (nodes[i].edges.get(j) < i)
            {
                maxIncoming = Math.max(maxIncoming, nodes[i].edges.get(j));
            }
        }

        if (maxIncoming + 1 < i && maxIncoming > -1)
        {
            suspectedIncomingConnections[maxIncoming][i] = true;
        }

        for (int j = maxIncoming + 1; j < i; j++) {
            removePaths(j, i);
        }
    }

    private void checkOutcoming(int i) {
        int minOutcoming = nodes.length;
        for (int j = 0; j < nodes[i].edges.size(); j++) {
            if (nodes[i].edges.get(j) > i)
            {
                minOutcoming = Math.min(minOutcoming, nodes[i].edges.get(j));
            }
        }

        if (minOutcoming > i + 1 && minOutcoming < nodes.length)
        {
            suspectedOutcomingConnections[i][minOutcoming] = true;
        }

        for (int j = i + 1; j < minOutcoming; j++) {
            removePaths(i, j);
        }
    }

    private void removePaths(int start, int end)
    {
        missingPaths[start][end] = true;
    }

    private Node[] prepareTree(int[] t) {
        Node[] nodes = new Node[t.length];
        for (int i = 0; i < t.length; i++) {
            nodes[i] = new Node();
        }
        for (int i = 0; i < t.length; i++) {
            nodes[i].edges.add(t[i]);
            nodes[t[i]].edges.add(i);
        }
        return nodes;
    }

    private class Node {
        public ArrayList<Integer> edges = new ArrayList();
    }
}