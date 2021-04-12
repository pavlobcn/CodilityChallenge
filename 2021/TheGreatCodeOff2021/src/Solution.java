import java.util.Arrays;
import java.util.stream.IntStream;

public class Solution {
    private final static int DIFFERENT_COLORS = -1;
    public int solution(int N, int K, int[] A, int[] B, int[] C) {
        int[] positions = getPositions(A, B);
        Node root = getTree(positions);
        applyLayers(root, A, B, C);
        root.setParentColor(DIFFERENT_COLORS);
        return root.getCount(K);
    }

    private void applyLayers(Node root, int[] a, int[] b, int[] c) {
        for (int i = 0; i < a.length; i++)
        {
            applyLayer(root, a[i], b[i], c[i], DIFFERENT_COLORS);
        }
    }

    private void applyLayer(Node node, int start, int end, int color, int parentColor) {
        if (parentColor != DIFFERENT_COLORS) {
            node.color = parentColor;
        }
        if (end < node.start || start > node.end)
        {
            return;
        }
        if (start <= node.start && end >= node.end) {
            if (node.color != DIFFERENT_COLORS) {
                if (node.color+ 1 == color) {
                    node.color = color;
                    if (node.left != null)
                    {
                        node.left.color = color;
                    }
                    if (node.right != null)
                    {
                        node.right.color = color;
                    }
                } else {
                    node.spoiled = true;
                }
                return;
            }
        }
        if (node.left != null) {
            applyLayer(node.left, start, end, color, node.color);
            if (node.left.spoiled) {
                node.left = null;
            }
        }
        if (node.right != null) {
            applyLayer(node.right, start, end, color, node.color);
            if (node.right.spoiled) {
                node.right = null;
            }
        }
        switch (node.getChildrenSize())
        {
            case 0:
                node.spoiled = true;
                break;
            case 1:
                Node child = node.left != null ? node.left : node.right;
                node.start = child.start;
                node.end = child.end;
                node.color = child.color;
                node.left = child.left;
                node.right = child.right;
                break;
            case 2:
                node.color = node.left.color == node.right.color ? node.left.color : DIFFERENT_COLORS;
                node.start = Math.min(node.left.start, node.right.start);
                node.end = Math.max(node.left.end, node.right.end);
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
            node.left = getTree(positions, startIndex, midIndex);
            if (midIndex < endIndex) {
                node.right = getTree(positions, midIndex, endIndex);
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
        private Node left;
        private Node right;

        public Node(int start, int end) {
            this.start = start;
            this.end = end;
        }

        public int getCount(int color) {
            if (this.spoiled) {
                return 0;
            }
            int result = this.color == color && getChildrenSize() == 0 ? end - start + 1 : 0;
            result += left != null ? left.getCount(color) : 0;
            result += right != null ? right.getCount(color) : 0;
            return result;
        }

        @Override
        public String toString() {
            return String.format("start: %d, end: %d, child count: %d, color: %d", start, end, getChildrenSize(), color);
        }

        public void setParentColor(int parentColor) {
            if (parentColor != DIFFERENT_COLORS) {
                color = parentColor;
            }
            if (left != null)
            {
                left.setParentColor(color);
            }
            if (right != null)
            {
                right.setParentColor(color);
            }
        }

        private int getChildrenSize() {
            int result = 0;
            if (left != null) {
                result++;
            }
            if (right != null) {
                result++;
            }
            return result;
        }
    }
}
