package algo;

import java.util.ArrayList;
import java.util.List;

public class Solution {
    public String solution(int[][] A)
    {
        Node[][] pathNodes = new Node[A.length][A[0].length];
        Node rootNode = goBreadth(0, 0, A, pathNodes);
        String result = getString(rootNode);
        return result;
    }

    private String getString(Node rootNode) {
        StringBuffer stringBuffer = new StringBuffer();
        while (rootNode != null)
        {
            stringBuffer.append(rootNode.value);
            rootNode = rootNode.next;
        }

        return stringBuffer.toString();
    }

    private Node goBreadth(int i, int j, int[][] a, Node[][] pathNodes) {
        if (pathNodes[i][j] != null) {
            return pathNodes[i][j];
        }
        Node resultNode;
        if (i == a.length - 1 && j == a[0].length - 1) {
            resultNode = new Node(a[i][j], null);
        }
        else {

            List<Node> substrings = new ArrayList();
            if (i < a.length - 1) {
                substrings.add(new Node(a[i][j], goBreadth(i + 1, j, a, pathNodes)));
            }

            if (j < a[0].length - 1) {
                substrings.add(new Node((a[i][j]), goBreadth(i, j + 1, a, pathNodes)));
            }

            if (substrings.size() == 2)
            {
                Node substring0 = substrings.get(0).next;
                Node substring1 = substrings.get(1).next;
                resultNode = substrings.get(0);
                while (substring0 != substring1)
                {
                    if (substring0.value < substring1.value)
                    {
                        resultNode = substrings.get(1);
                        break;
                    }
                    if (substring0.value > substring1.value)
                    {
                        break;
                    }
                    substring0 = substring0.next;
                    substring1 = substring1.next;
                }
            }
            else
            {
                resultNode = substrings.get(0);
            }
        }

        pathNodes[i][j] = resultNode;
        return resultNode;
    }

    public static class Node
    {
        public int value;
        public Node next;

        public Node(int value, Node next) {
            this.value = value;
            this.next = next;
        }
    }
}
