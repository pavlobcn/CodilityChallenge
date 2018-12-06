using System.Collections.Generic;
using System.Linq;
using System.Text;

class Solution
{
    private const int MaxLength = 600000;
    public const string NoAnswer = "NO";
    public const char Space = ' ';

    public string solution(string S)
    {
        string oneLetterPalindrom = GetOneLetterPalindrom(S);
        if (oneLetterPalindrom != NoAnswer)
        {
            return oneLetterPalindrom;
        }

        Node root1;
        Node root2;
        GetTree(S, out root1, out root2);

        string result = NoAnswer;

        var symmetricGroup = new SymmetricGroup(new SentenceTreeNode(root1, null), new SentenceTreeNode(root2, null));
        List<SymmetricGroup> symmetricGroups = new List<SymmetricGroup> { symmetricGroup };
        int iteration = 0;
        while (iteration < MaxLength)
        {
            symmetricGroups = symmetricGroups.SelectMany(x => x.Join(root1, root2)).ToList();

            if (!symmetricGroups.Any())
            {
                return NoAnswer;
            }

            result = GetPalindrom(symmetricGroups);
            if (result != NoAnswer)
            {
                return result;
            }

            iteration++;
        }
        return result;
    }

    private static string GetOneLetterPalindrom(string s)
    {
        var len = s.Length;
        if (len == 1)
        {
            return s;
        }

        if (s[1] == Space)
        {
            return s[0].ToString();
        }

        if (s[len - 2] == Space)
        {
            return s[len - 1].ToString();
        }

        for (int i = 3; i < len - 3; i++)
        {
            if (s[i - 1] == Space)
            {
                if (s[i + 1] == Space)
                {
                    return s[i].ToString();
                }

                i++;
            }

        }

        return NoAnswer;
    }

    private static void GetTree(string sentence, out Node root1, out Node root2)
    {
        var words = sentence.Split(Space).Select(Word.Parse).ToArray();
        root1 = new Node(Node.RootChar);
        root2 = new Node(Node.RootChar);
        foreach (Word word in words)
        {
            // Get tree for origin words
            root1.ProcessWord(root1, word, 0);
            // Get tree for reversed words
            root2.ProcessWord(root2, word.Reverse(), 0);
        }
    }

    private string GetPalindrom(List<SymmetricGroup> symmetricGroups)
    {
        string palindrom = symmetricGroups.Select(x => x.GetPalindrom()).FirstOrDefault(x => x != NoAnswer);
        if (string.IsNullOrEmpty(palindrom))
        {
            return NoAnswer;
        }

        return palindrom;
    }
}

public partial class SymmetricGroup
{
    public SymmetricGroup(SentenceTreeNode sentence, SentenceTreeNode reverseSentence)
    {
        Sentence = sentence;
        ReverseSentence = reverseSentence;
    }

    public SentenceTreeNode Sentence { get; }
    public SentenceTreeNode ReverseSentence { get; }

    public IEnumerable<SymmetricGroup> Join(Node root1, Node root2)
    {
        return Sentence.GetNextNodes(root1)
            .Join(ReverseSentence.GetNextNodes(root2), x => x.Key, y => y.Key,
                (x, y) => new SymmetricGroup(
                    new SentenceTreeNode(x.Value, Sentence),
                    new SentenceTreeNode(y.Value, ReverseSentence)));
    }

    public string GetPalindrom()
    {
        // Check end of word
        bool endOfWord1 = Sentence.Node.IsWordEnd;
        if (endOfWord1)
        {
            bool endOfWord2 = ReverseSentence.Node.IsWordEnd;
            if (endOfWord2)
            {
                return Sentence.GetSentence() + " " + ReverseSentence.GetSentence().ReverseString();
            }
        }

        List<string> markers1 = Sentence.Node.Markers;
        if (markers1.Any())
        {
            List<string> markers2 = ReverseSentence.Node.Markers;
            IEnumerable<string> intersection =
                markers1.Select(x => x.Trim('L', 'R')).Intersect(markers2.Select(x => x.Trim('L', 'R')));
            foreach (string intersectionMarker in intersection)
            {
                if (intersectionMarker.Contains('-'))
                {
                    // Mid of palindrom
                    return Sentence.GetSentence() + ReverseSentence.GetSentence().ReverseString().Substring(1);
                }

                bool endOfHalfPalindrom1 = Sentence.Node.Markers.Contains(intersectionMarker + "L");
                if (endOfHalfPalindrom1)
                {
                    bool endOfHalfPalindrom2 = ReverseSentence.Node.Markers.Contains(intersectionMarker + "R");
                    if (endOfHalfPalindrom2)
                    {
                        // Half of palindrom
                        return Sentence.GetSentence() + ReverseSentence.GetSentence().ReverseString();
                    }
                }
            }
        }

        return Solution.NoAnswer;
    }
}

public partial class SentenceTreeNode
{
    private readonly Node _node;
    private readonly SentenceTreeNode _parent;

    public SentenceTreeNode(Node node, SentenceTreeNode parent)
    {
        _node = node;
        _parent = parent;
    }

    public Node Node => _node;

    public SentenceTreeNode Parent => _parent;

    public string GetSentence()
    {
        var stringBuilder = new StringBuilder();
        GetSentence(stringBuilder);
        return stringBuilder.ToString();
    }

    private void GetSentence(StringBuilder stringBuilder)
    {
        if (Parent != null)
        {
            Parent.GetSentence(stringBuilder);
            if (Parent.Parent != null && Node.IsWordStart)
            {
                stringBuilder.Append(Solution.Space);
            }
        }

        if (Node.C != Node.RootChar)
        {
            stringBuilder.Append(Node.C);
        }
    }

    public List<KeyValuePair<char, Node>> GetNextNodes(Node root)
    {
        var nextNodes = new List<KeyValuePair<char, Node>>();
        if (Node.IsWordEnd)
        {
            nextNodes.AddRange(root.Children);
        }

        nextNodes.AddRange(Node.Children);

        return nextNodes;
    }
}

public partial class Word
{
    public Character[] Characters { get; }

    private Word(Character[] characters)
    {
        Characters = characters;
    }

    public Word Reverse()
    {
        return new Word(Characters.Reverse().ToArray());
    }

    public static Word Parse(string word)
    {
        int len = word.Length;
        var characters = new Character[len];
        for (int i = 0; i < len; i++)
        {
            characters[i] = new Character(word[i]);
        }
        // End of half palindrom indexes;
        int halfIndexCode = 1;
        // Mid of palindrom indexes;
        int midIndexCode = -3;

        characters[0].Markers.Add(word + "-1");
        characters[len - 1].Markers.Add(word + "-2");

        if (len > 1)
        {
            for (int i = 0; i < len - 1; i++)
            {
                // Check for EndOfHalfPalindrom
                bool endOfHalfPalindrom = true;
                for (int l = i, r = i + 1; l >= 0 && r < len; l--, r++)
                {
                    if (word[l] != word[r])
                    {
                        endOfHalfPalindrom = false;
                        break;
                    }
                }
                if (endOfHalfPalindrom)
                {
                    characters[i].Markers.Add(word + halfIndexCode + "L");
                    characters[i + 1].Markers.Add(word + halfIndexCode + "R");
                    halfIndexCode++;
                }

                // Check for MidOfPalindrom
                if (i == 0 || len < 3)
                {
                    // Word is to short or start index is to small
                    continue;
                }
                bool midOfPalindrom = true;
                for (int l = i - 1, r = i + 1; l >= 0 && r < len; l--, r++)
                {
                    if (word[l] != word[r])
                    {
                        midOfPalindrom = false;
                        break;
                    }
                }
                if (midOfPalindrom)
                {
                    characters[i].Markers.Add(word + midIndexCode.ToString());
                    midIndexCode--;
                }
            }
        }

        return new Word(characters);
    }
}

public partial class Character
{
    public char C { get; }

    public List<string> Markers { get; }

    public Character(char c)
    {
        C = c;
        Markers = new List<string>();
    }
}

public partial class Node
{
    public const char RootChar = '$';
    public char C { get; private set; }
    public bool IsWordStart { get; private set; }
    public List<string> Markers { get; set; } = new List<string>();
    public IDictionary<char, Node> Children { get; private set; } = new Dictionary<char, Node>();
    public bool IsWordEnd { get; set; }

    public Node(char c, bool isWordStart = false)
    {
        C = c;
        IsWordStart = isWordStart;
    }

    public void ProcessWord(Node root, Word word, int charIndex)
    {
        if (charIndex == word.Characters.Length)
        {
            IsWordEnd = true;
            return;
        }

        bool isWordStart = charIndex == 0;
        Node child;
        Character firstCharacter = word.Characters[charIndex];
        if (!Children.TryGetValue(firstCharacter.C, out child))
        {
            child = new Node(firstCharacter.C, isWordStart);
            Children[firstCharacter.C] = child;
        }

        child.Markers.AddRange(firstCharacter.Markers);
        child.ProcessWord(root, word, charIndex + 1);
    }
}

public static class Extensions
{
    public static string ReverseString(this string s)
    {
        return new string(s.Reverse().ToArray());
    }
}