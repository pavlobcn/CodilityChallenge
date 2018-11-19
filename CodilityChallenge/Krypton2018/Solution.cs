using System;
using System.Collections.Generic;
using System.Linq;

class Solution
{
    private Node[][] matrix;
    private List<Node>[][] pathsMatrix;

    public int solution(int[][] A)
    {
        matrix = GetMatrix(A);
        pathsMatrix = matrix.Select(row => row.Select(c => (List<Node>) null).ToArray()).ToArray();

        if (IsFastResultForOneTrailingZero())
        {
            return 1;
        }

        if (IsFastResultForEqualElements())
        {
            return Math.Min(matrix[0][0].Pow2 , matrix[0][0].Pow5) * (2 * matrix.Length - 1);
        }

        List<Node> pathNodes = GetPath(0, 0);
        return GetTrailingZeroCount(pathNodes);
    }

    private bool IsFastResultForEqualElements()
    {
        var startValue = matrix[0][0];
        return matrix.SelectMany(x => x).All(x => x != Node.Zero && x.Pow2 == startValue.Pow2 && x.Pow5 == startValue.Pow5);
    }

    private bool IsFastResultForOneTrailingZero()
    {
        // Check reverse diagonals
        bool zeroReverseDiagonalExists =
            Enumerable.Range(0, matrix.Length * 2 - 1).Reverse()
                .Any(i => i < matrix.Length
                    ? Enumerable.Range(0, i + 1).All(row => matrix[row][i - row] == Node.Zero)
                    : Enumerable.Range(i - matrix.Length, matrix.Length * 2 - i - 1).All(row => matrix[row + 1][i - row - 1] == Node.Zero));
        if (zeroReverseDiagonalExists)
        {
            return true;
        }

        return false;
    }

    private List<Node> GetPath(int startingRow, int startingColumn)
    {
        if (pathsMatrix[startingRow][startingColumn] != null)
        {
            return pathsMatrix[startingRow][startingColumn];
        }

        Node startNode = matrix[startingRow][startingColumn];
        var paths = new List<Node>();
        if (startingRow + 1 < matrix.Length)
        {
            List<Node> nextRowPaths = GetPath(startingRow + 1, startingColumn);
            paths = nextRowPaths;
        }
        if (startingColumn + 1 < matrix.Length)
        {
            List<Node> nextColumnsPaths = GetPath(startingRow, startingColumn + 1);
            paths.AddRange(nextColumnsPaths);
        }

        List<Node> result;
        switch (paths.Count)
        {
                case 0:
                    result = new List<Node> { startNode };
                    break;
                default:
                    result = Min(paths.Select(x => Sum(startNode, x)));
                    break;
        }

        pathsMatrix[startingRow][startingColumn] = result;
        return result;
    }

    private Node[][] GetMatrix(int[][] initialMatrix)
    {
        var newMatrix = new Node[initialMatrix.Length][];
        for (int i = 0; i < initialMatrix.Length; i++)
        {
            Node[] line = new Node[initialMatrix.Length];
            newMatrix[i] = line;
            for (int j = 0; j < initialMatrix.Length; j++)
            {
                line[j] = GetNode(initialMatrix[i][j]);
            }
        }
        return newMatrix;
    }

    private Node GetNode(int initialValue)
    {
        if (initialValue == 0)
        {
            return Node.Zero;
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

    private static int GetTrailingZeroCount(IEnumerable<Node> nodes)
    {
        return nodes.Min(GetTrailingZeroCount);
    }

    private static int GetTrailingZeroCount(Node node)
    {
        return Math.Min(node.Pow2, node.Pow5);
    }

    private List<Node> Min(IEnumerable<Node> candidates)
    {
        Node minPow2 = null;
        Node minPow5 = null;
        bool initialIteration = true;
        foreach (Node candidate in candidates)
        {
            if (initialIteration)
            {
                minPow2 = candidate;
                minPow5 = candidate;
                initialIteration = false;
                continue;
            }

            int comparePow2Result = ComparePow2(candidate, minPow2);
            if (comparePow2Result < 0)
            {
                minPow2 = candidate;
            }
            else if (comparePow2Result == 0)
            {
                if (ComparePow5(candidate, minPow2) < 0)
                {
                    minPow2 = candidate;
                }
            }

            int comparePow5Result = ComparePow5(candidate, minPow5);
            if (comparePow5Result < 0)
            {
                minPow5 = candidate;
            }
            else if (comparePow5Result == 0)
            {
                if (ComparePow2(candidate, minPow5) < 0)
                {
                    minPow5 = candidate;
                }
            }
        }
        return minPow2 == minPow5 ? new List<Node>{minPow2} : new List<Node> { minPow2, minPow5 };
    }

    private int ComparePow2(Node node1, Node node2)
    {
        return node1.Pow2.CompareTo(node2.Pow2);
    }

    private int ComparePow5(Node node1, Node node2)
    {
        return node1.Pow5.CompareTo(node2.Pow5);
    }

    private static Node Sum(Node node1, Node node2)
    {
        if (node1 == Node.Zero || node2 == Node.Zero)
        {
            return Node.Zero;
        }
        return new Node(node1.Pow2 + node2.Pow2, node1.Pow5 + node2.Pow5);
    }

    private class Node
    {
        public static readonly Node Zero = new Node(1, 1);

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
