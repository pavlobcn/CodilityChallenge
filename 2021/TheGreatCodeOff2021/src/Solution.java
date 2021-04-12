import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.stream.IntStream;

public class Solution {
    private final static int NoColor = -1;
    public int solution(int N, int K, int[] A, int[] B, int[] C) {
        int[] positions = getPositions(A, B);
        Node root = getTree(positions);
        applyLayers(root, A, B, C);
        root.setParentColor(NoColor);
        return root.getCount(K);
    }

    private void applyLayers(Node root, int[] a, int[] b, int[] c) {
        for (int i = 0; i < a.length; i++)
        {
            applyLayer(root, a[i], b[i], c[i], NoColor);
        }
    }

    private void applyLayer(Node node, int start, int end, int color, int parentColor) {
        if (parentColor != NoColor) {
            node.color = parentColor;
        }
        if (end < node.start || start > node.end)
        {
            return;
        }
        if (start <= node.start && end >= node.end) {
            if (node.color != NoColor) {
                if (node.color+ 1 == color) {
                    node.color = color;
                    for (Node child : node.children) {
                        child.color = color;
                    }
                } else {
                    node.spoiled = true;
                }
                return;
            }
        }
        for (int i = node.children.size() - 1; i >= 0; i--) {
            Node child = node.children.get(i);
            applyLayer(child, start, end, color, node.color);
            if (child.spoiled) {
                node.children.remove(i);
            }
        }
        switch (node.children.size())
        {
            case 0:
                node.spoiled = true;
                break;
            case 1:
                Node child = node.children.get(0);
                node.start = child.start;
                node.end = child.end;
                node.color = child.color;
                node.children.clear();
                for (Node subChild : child.children) {
                    node.addChild(subChild);
                }
                break;
            case 2:
                node.color =
                        node.children.get(0).color == node.children.get(1).color
                                ? node.children.get(0).color
                                : NoColor;
                break;
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
        private int start;
        private int end;
        private int color;
        private boolean spoiled;
        private final List<Node> children = new ArrayList<>();

        public Node(int start, int end) {
            this.start = start;
            this.end = end;
        }

        public int getCount(int color) {
            if (this.spoiled) {
                return 0;
            }
            int result = this.color == color && children.size() == 0 ? end - start + 1 : 0;
            for (Node child : children) {
                result += child.getCount(color);
            }
            return result;
        }

        public void addChild(Node child) {
            children.add(child);
        }

        @Override
        public String toString() {
            return String.format("start: %d, end: %d, child count: %d, color: %d", start, end, children.size(), color);
        }

        public void setParentColor(int parentColor) {
            if (parentColor != NoColor) {
                color = parentColor;
                for (Node child : children) {
                    child.setParentColor(parentColor);
                }
            } else {
                for (Node child : children) {
                    child.setParentColor(color);
                }
            }
        }
    }
}
