using System;
using System.Collections.Generic;
using System.Linq;

class Solution
{
    private Node[][] matrix;
    private Node[][][] pathesMatrix;

    public int solution(int[][] A)
    {
        matrix = GetMatrix(A);
        pathesMatrix = matrix.Select(row => row.Select(c => (Node[]) null).ToArray()).ToArray();

        if (IsFastResultForOneTrailingZero())
        {
            return 1;
        }

        if (IsFastResultForEqualElements())
        {
            return Math.Min(matrix[0][0].Pow2 , matrix[0][0].Pow5) * (2 * matrix.Length - 1);
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
        if (pathesMatrix[startingRow][startingColumn] != null)
        {
            return pathesMatrix[startingRow][startingColumn];
        }

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

        Node[] result;
        switch (pathes.Count)
        {
                case 0:
                    result = new []{ startNode };
                    break;
                default:
                    result = Min(pathes.Select(x => Sum(startNode, x)));
                    break;
        }

        pathesMatrix[startingRow][startingColumn] = result;
        return result;
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

    private Node[] Min(IEnumerable<Node> candidates)
    {
        Node minPow2 = null;
        Node minPow5 = null;
        Node minPow2AndPow5 = null;
        bool initialIteration = true;
        foreach (Node candidate in candidates)
        {
            if (initialIteration)
            {
                minPow2 = candidate;
                minPow5 = candidate;
                minPow2AndPow5 = candidate;
                initialIteration = false;
                continue;
            }
            switch (ComparePow2(candidate, minPow2))
            {
                case -1:
                    minPow2 = candidate;
                    break;
                case 0:
                    if (ComparePow5(candidate, minPow2) < 0)
                    {
                        minPow2 = candidate;
                    }
                    break;
            }
            switch (ComparePow5(candidate, minPow5))
            {
                case -1:
                    minPow5 = candidate;
                    break;
                case 0:
                    if (ComparePow2(candidate, minPow5) < 0)
                    {
                        minPow5 = candidate;
                    }
                    break;
            }
            switch (ComparePow2AndPow5(candidate, minPow2AndPow5))
            {
                case -1:
                    minPow2AndPow5 = candidate;
                    break;
            }

        }
        return new[] {minPow2, minPow5, minPow2AndPow5}.Distinct().ToArray();
    }

    private int ComparePow2(Node node1, Node node2)
    {
        int val1 = node1?.Pow2 ?? 1;
        int val2 = node2?.Pow2 ?? 1;
        return Math.Sign(val1.CompareTo(val2));
    }

    private int ComparePow5(Node node1, Node node2)
    {
        int val1 = node1?.Pow5 ?? 1;
        int val2 = node2?.Pow5 ?? 1;
        return Math.Sign(val1.CompareTo(val2));
    }

    private int ComparePow2AndPow5(Node node1, Node node2)
    {
        int val1 = node1 == null ? 1 : Math.Min(node1.Pow2, node1.Pow5);
        int val2 = node2 == null ? 1 : Math.Min(node2.Pow2, node2.Pow5);
        return Math.Sign(val1.CompareTo(val2));
    }

    private int Compare(Node node1, Node node2, Func<Node, int> valFunc)
    {
        int val1 = node1 == null ? 1 : valFunc(node1);
        int val2 = node2 == null ? 1 : valFunc(node2);
        return Math.Sign(val1.CompareTo(val2));
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
