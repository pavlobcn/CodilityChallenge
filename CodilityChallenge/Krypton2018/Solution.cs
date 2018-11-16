using System;
using System.Collections.Generic;
using System.Linq;

class Solution
{
    public int solution(int[][] A)
    {
        Node[][] matrix = GetMatrix(A);
        Node initialNode = matrix[0][0];
        Node pathNode = GetPath(initialNode, 0, 0, matrix);
        return GetTrailingZeroCount(pathNode);
    }

    private Node GetPath(Node initialNode, int row, int column, Node[][] matrix)
    {
        if (initialNode == null)
        {
            return null;
        }

        var pathes = new List<Node>();
        if (row + 1 < matrix.Length)
        {
            Node lowerInitialNode = Sum(initialNode, GetPath(matrix[row + 1][column], row + 1, column, matrix));
            if (lowerInitialNode == null)
            {
                return null;
            }
            pathes.Add(lowerInitialNode);
        }
        if (column + 1 < matrix.Length)
        {
            Node rightInitialNode = Sum(initialNode, GetPath(matrix[row][column + 1], row, column + 1, matrix));
            if (rightInitialNode == null)
            {
                return null;
            }
            pathes.Add(rightInitialNode);
        }
        switch (pathes.Count)
        {
                case 0:
                    return initialNode;
                case 1:
                    return pathes.Single();
                default:
                    var trailingZeroCount0 = GetTrailingZeroCount(pathes[0]);
                    var trailingZeroCount1 = GetTrailingZeroCount(pathes[1]);
                    return trailingZeroCount0 < trailingZeroCount1 ? pathes[0] : pathes[1];
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

    private static int GetTrailingZeroCount(Node node)
    {
        return node == null ? 1 : Math.Min(node.Pow2, node.Pow5);
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

        public int Pow2 { get; private set; }
        public int Pow5 { get; private set; }

        public override string ToString()
        {
            return $"{Pow2}, {Pow5}";
        }
    }
}
