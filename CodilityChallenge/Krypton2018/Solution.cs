using System;
using System.Collections.Generic;
using System.Linq;

class Solution
{
    private Node[][] matrix;
    //private Node[][][] pathesMatrix;

    public int solution(int[][] A)
    {
        matrix = GetMatrix(A);
        //pathesMatrix = matrix.Select(row => row.Select(c => (Node[]) null).ToArray()).ToArray();
        if (IsFastResultForOneTrailingZero())
        {
            return 1;
        }

        if (IsFastResultForEqualElements())
        {
            return Math.Min(matrix[0][0].Pow2 , matrix[0][0].Pow2) * (2 * matrix.Length - 1);
        }

        Node[] pathNodes = GetPath(0, 0);
        return GetTrailingZeroCount(pathNodes);
    }

    private bool IsFastResultForEqualElements()
    {
        var startValue = matrix[0][0];
        return matrix.SelectMany(x => x).All(x => x != null && x.Pow2 == startValue.Pow2 && x.Pow5 == startValue.Pow5);
    }

    private bool IsFastResultForOneTrailingZero()
    {
        // Check reverse diagonals
        bool zeroRevevrseDiagonalExists =
            Enumerable.Range(0, matrix.Length * 2 - 1).Reverse()
                .Any(i => i < matrix.Length
                    ? Enumerable.Range(0, i + 1).All(row => matrix[row][i - row] == null)
                    : Enumerable.Range(i - matrix.Length, matrix.Length * 2 - i - 1).All(row => matrix[row + 1][i - row - 1] == null));
        if (zeroRevevrseDiagonalExists)
        {
            return true;
        }

        return false;
    }

    private Node[] GetPath(int startingRow, int startingColumn)
    {
        Node startNode = matrix[startingRow][startingColumn];
        var pathes = new List<Node>();
        if (startingRow + 1 < matrix.Length)
        {
            Node[] nextRowPathes = GetPath(startingRow + 1, startingColumn);
            pathes.AddRange(nextRowPathes);
        }
        if (startingColumn + 1 < matrix.Length)
        {
            Node[] nextColumnsPathes = GetPath(startingRow, startingColumn + 1);
            pathes.AddRange(nextColumnsPathes);
        }
        switch (pathes.Count)
        {
                case 0:
                    return new []{ startNode };
                default:
                    return Min(pathes.Select(x => Sum(startNode, x)).ToArray());
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

    private static Node[] Min(Node[] candidates)
    {
        bool existsNull = candidates.Any(x => x == null);
        candidates = candidates.Where(x => x != null).ToArray();
        if (!candidates.Any())
        {
            return new Node[] {null};
        }
        int minPow2 = candidates.Min(x => x.Pow2);
        int minPow5 = candidates.Min(x => x.Pow5);
        int minPow2AndPow2 = candidates.Min(x => Math.Min(x.Pow2, x.Pow5));
        candidates = candidates
            .Where(x => x.Pow2 == minPow2 || x.Pow5 == minPow5 || Math.Min(x.Pow2, x.Pow5) == minPow2AndPow2).ToArray();
        return existsNull ? candidates.Union(new Node[] {null}).ToArray() : candidates;
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
