using System;
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
        return initialNode;
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

    public static int GetTrailingZeroCount(Node node)
    {
        return node == null ? 1 : Math.Min(node.Pow2, node.Pow5);
    }

    public class Node
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
