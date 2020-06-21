import java.util.ArrayList;
import java.util.Objects;

public class Solution {
    public String solution(int[][] A)
    {
        StringBuffer stringBuffer = new StringBuffer();
        stringBuffer.append(A[0][0]);
        ArrayList<Node> visitedDiagonalNodes = new ArrayList();
        visitedDiagonalNodes.add(new Node(0,0));
        for (int i = 1; i < A.length + A[0].length - 1; i++)
        {
            ArrayList<Node> nextNodes = getNextNodes(visitedDiagonalNodes, A);
            Node nextNode = nextNodes.get(0);
            stringBuffer.append(A[nextNode.x][nextNode.y]);
            visitedDiagonalNodes = nextNodes;
        }
        return stringBuffer.toString();
    }

    private ArrayList<Node> getNextNodes(ArrayList<Node> visitedDiagonalNodes, int[][] a) {
        ArrayList<Node>[] nextNodes = new ArrayList[10];
        for (int i = 0; i < nextNodes.length; i++)
        {
            nextNodes[i] = new ArrayList();
        }
        int maxNextNumber = 0;
        for (int i = 0; i < visitedDiagonalNodes.size(); i++)
        {
            if (i > 0 && visitedDiagonalNodes.get(i).equals(visitedDiagonalNodes.get(i - 1)))
            {
                continue;
            }
            Node node = visitedDiagonalNodes.get(i);
            if (node.x + 1 < a.length)
            {
                maxNextNumber = Math.max(maxNextNumber, a[node.x + 1][node.y]);
                nextNodes[a[node.x + 1][node.y]].add(new Node(node.x + 1, node.y));
            }
            if (node.y + 1 < a[0].length)
            {
                maxNextNumber = Math.max(maxNextNumber, a[node.x][node.y + 1]);
                nextNodes[a[node.x][node.y + 1]].add(new Node(node.x, node.y + 1));
            }
        }
        return nextNodes[maxNextNumber];
    }

    public class Node
    {
        public int x;
        public int y;

        public Node(int x, int y) {
            this.x = x;
            this.y = y;
        }

        @Override
        public boolean equals(Object o) {
            if (this == o) return true;
            if (o == null || getClass() != o.getClass()) return false;
            Node node = (Node) o;
            return x == node.x &&
                    y == node.y;
        }

        @Override
        public int hashCode() {
            return Objects.hash(x, y);
        }
    }
}
