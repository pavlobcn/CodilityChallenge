using System;
using System.Collections.Generic;
using System.Linq;

class Solution
{
    public int solution(int[][] A)
    {
        Node[][] matrix = GetMatrix(A);
        Node[] initialNodes = { matrix[0][0]};
        Node[] pathNodes = GetPath(initialNodes, 0, 0, matrix);
        return GetTrailingZeroCount(pathNodes);
    }

    private Node[] GetPath(Node[] initialNodes, int row, int column, Node[][] matrix)
    {
        if (initialNodes.All(x => x == null))
        {
            return new Node[]{null};
        }

        var pathes = new List<Node[]>();
        if (row + 1 < matrix.Length)
        {
            Node[] lowerInitialNodes = Sum(initialNodes, GetPath(new[] { matrix[row + 1][column]}, row + 1, column, matrix));
            pathes.Add(lowerInitialNodes);
        }
        if (column + 1 < matrix.Length)
        {
            Node[] rightInitialNodes = Sum(initialNodes, GetPath(new []{matrix[row][column + 1]}, row, column + 1, matrix));
            pathes.Add(rightInitialNodes);
        }
        switch (pathes.Count)
        {
                case 0:
                    return initialNodes;
                case 1:
                    return pathes.Single();
                default:
                    return Min(pathes.SelectMany(p => p).ToArray());
        }
    }

    private Node[][] GetMatrix(int[][] initialMatrix)
    {
        return initialMatrix.Select(row => row.Select(GetNode).ToArray()).ToArray();
    }

    private Node GetNode(int initialValue)
    {
        if (initialValue == 0)
        {
            // special value
            return null;
        }

        int pow2 = GetPow(2, initialValue);
        int pow5 = GetPow(5, initialValue);
        return new Node(pow2, pow5);
    }

    private int GetPow(int baseValue, int initialValue)
    {
        int result = 0;
        while (initialValue % baseValue == 0)
        {
            result++;
            initialValue /= baseValue;
        }
        return result;
    }

    private static int GetTrailingZeroCount(Node[] nodes)
    {
        return nodes.Min(GetTrailingZeroCount);
    }

    private static int GetTrailingZeroCount(Node node)
    {
        return node == null ? 1 : Math.Min(node.Pow2, node.Pow5);
    }

    private static Node[] Sum(Node[] nodes1, Node[] nodes2)
    {
        Node[] candidates = nodes1.SelectMany(n1 => nodes2.Select(n2 => Sum(n1, n2))).ToArray();
        return Min(candidates);
    }

    private static Node[] Min(Node[] candidates)
    {
        throw new NotImplementedException();
    }

    private static Node Sum(Node node1, Node node2)
    {
        if (node1 == null || node2 == null)
        {
            return null;
        }
        return new Node(node1.Pow2 + node2.Pow2, node1.Pow5 + node2.Pow5);
    }

    private class Node
    {
        public Node(int pow2, int pow5)
        {
            Pow2 = pow2;
            Pow5 = pow5;
        }

        public int Pow2 { get; }
        public int Pow5 { get; }

        public override string ToString()
        {
            return $"{Pow2}, {Pow5}";
        }
    }
}
