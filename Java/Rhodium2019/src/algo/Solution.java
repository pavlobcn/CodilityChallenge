package algo;

import java.util.ArrayList;
import java.util.HashSet;

public class Solution {
    int result = 0;
    Node[] nodes;

    public int solution(int[] T) {
        nodes = prepareTree(T);
        for (int i = 0; i < nodes.length; i++) {
            findPaths(i, new HashSet());
        }
        return result;
    }

    private void findPaths(int current, HashSet<Integer> edges) {
        result++;

        for (int i = 0; i < nodes[current].edges.size(); i++) {
            edges.add(nodes[current].edges.get(i));
        }

        if (edges.contains(current + 1))
        {
            findPaths(current + 1, edges);
        }
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
