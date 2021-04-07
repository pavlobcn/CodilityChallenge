import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.stream.IntStream;

public class Solution {
    public int solution(int N, int K, int[] A, int[] B, int[] C) {
        int[] positions = getPositions(A, B);
        Node root = getTree(positions);
        applyLayers(root, A, B, C);
        return root.getCount(K);
    }

    private void applyLayers(Node root, int[] a, int[] b, int[] c) {
        for (int i = 0; i < a.length; i++)
        {
            applyLayer(root, a[i], b[i], c[i]);
        }
    }

    private void applyLayer(Node node, int start, int end, int color) {
        if (end < node.start || start > node.end)
        {
            return;
        }
        if (node.children.size() > 0) {
            for (int i = node.children.size() - 1; i >= 0; i--) {
                Node child = node.children.get(i);
                applyLayer(child, start, end, color);
                if (child.spoiled) {
                    node.children.remove(i);
                }
            }
            if (node.children.size() == 0) {
                node.spoiled = true;
            }
        }
        else {
            node.color++;
            if (node.color != color) {
                node.spoiled = true;
            }
        }
    }

    private Node getTree(int[] positions) {
        return getTree(positions, 0, positions.length - 1);
    }

    private Node getTree(int[] positions, int startIndex, int endIndex) {
        var node = new Node(positions[startIndex], positions[endIndex] - 1);
        int midIndex = (startIndex + endIndex) / 2;
        if (midIndex > startIndex) {
            node.addChild(getTree(positions, startIndex, midIndex));
            if (midIndex < endIndex) {
                node.addChild(getTree(positions, midIndex, endIndex));
            }
        }
        return node;
    }

    private int[] getPositions(int[] a, int[] b) {
        return IntStream.concat(Arrays.stream(a), Arrays.stream(b).map(x -> x + 1)).distinct().sorted().toArray();
    }

    private static class Node {
        private final int start;
        private final int end;
        private int color;
        private boolean spoiled;
        private final List<Node> children = new ArrayList<>();

        public Node(int start, int end) {
            this.start = start;
            this.end = end;
        }

        public int getCount(int color) {
            return children
                    .stream()
                    .map(child -> child.getCount(color))
                    .reduce(!this.spoiled && this.color == color ? end - start + 1 : 0, Integer::sum);
        }

        public void addChild(Node child) {
            children.add(child);
        }

        @Override
        public String toString() {
            return String.format("start: %d, end: %d, child count: %d", start, end, children.size());
        }
    }
}
