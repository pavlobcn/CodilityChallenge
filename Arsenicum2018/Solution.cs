using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

class Solution
{
    private const int MaxLength = 50;
    private const string NoAnswer = "NO";
    public const char Space = ' ';
    public const char StartOfSentence = '$';

    public string solution(string S)
    {
        Node root1;
        Node root2;
        GetTree(S, out root1, out root2);

        foreach (KeyValuePair<char, Node> child in root1.Children)
        {
            if (child.Value.CanStartNewWord)
            {
                // Found word with 1 letter and this word is the result
                return child.Key.ToString();
            }
        }

        string result = "NO";

        /*
        var currentSentences = root.Children.Select(x => new SentenceTreeNode(x.Key, x.Value, null)).ToList();
        int iteration = 0;
        while (iteration < MaxLength && (result = GetPalindrom(currentSentences)) == NoAnswer)
        {
            currentSentences = currentSentences.SelectMany(x =>
            {
                x.ProcessNode(root);
                return x.Children;
            }).ToList();

            iteration++;
        }
        */

        return result;
    }

    private static void GetTree(string sentence, out Node root1, out Node root2)
    {
        var words = sentence.Split(Space).Select(w => new Word(w)).ToArray();
        root1 = new Node(StartOfSentence);
        root2 = new Node(StartOfSentence);
        foreach (Word word in words)
        {
            // Get tree for origin words
            ProcessWord(root1, word);
            // Get tree for reversed words
            ProcessWord(root2, word.Reverse());
        }
    }

    private string GetPalindrom(List<SentenceTreeNode> sentences)
    {
        string palindrom = sentences.Select(GetPalindrom).FirstOrDefault(x => x != NoAnswer);
        if (string.IsNullOrEmpty(palindrom))
        {
            return NoAnswer;
        }

        return palindrom;
    }

    private string GetPalindrom(SentenceTreeNode sentence)
    {
        if (sentence.C != Space)
        {
            return NoAnswer;
        }

        string s = string.Empty;
        string stringWithoutSpaces = string.Empty;
        while (sentence != null)
        {
            if (sentence.C != Space)
            {
                stringWithoutSpaces = sentence.C + stringWithoutSpaces;
            }
            s = sentence.C + s;
            sentence = sentence.Parent;
        }

        if (stringWithoutSpaces == new string(stringWithoutSpaces.Reverse().ToArray()))
        {
            return s.Trim();
        }

        return NoAnswer;
    }

    private static void ProcessWord(Node node, Word word)
    {
        if (word.Characters.Count == 0)
        {
            node.StartNewWord();
            return;
        }

        Node child;
        Character firstCharacter = word.Characters[0];
        if (!node.Children.TryGetValue(firstCharacter.C, out child))
        {
            child = new Node(firstCharacter.C);
            node.Children[firstCharacter.C] = child;
        }
        child.AddMarkers(firstCharacter.Markers);
        ProcessWord(child, word.SubWord());
    }

    private class SentenceTreeNode
    {
        private readonly char _char;
        private readonly Node _node;
        private readonly SentenceTreeNode _parent;
        private readonly List<SentenceTreeNode> _children = new List<SentenceTreeNode>();

        public SentenceTreeNode(char c, Node node, SentenceTreeNode parent)
        {
            _char = c;
            _node = node;
            _parent = parent != null && parent.C == Space ? parent.Parent : parent;
        }

        public char C => _char;

        public Node Node => _node;

        public List<SentenceTreeNode> Children => _children;

        public SentenceTreeNode Parent => _parent;

        public string Sentence
        {
            get
            {
                return (Parent == null ? String.Empty : Parent.Sentence) + C;
            }
        }

        public override string ToString()
        {
            return Sentence;
        }

        public void ProcessNode(Node root)
        {
            //if (_node.IsEndOfWord)
            {
                _children.Add(new SentenceTreeNode(Space, root, this));
            }

            _children.AddRange(_node.Children.Select(x => new SentenceTreeNode(x.Key, x.Value, this)));
        }
    }
}

public class Word
{
    private Word()
    {
        Characters = new List<Character>();
    }

    public Word(string word)
    {
        Characters = Parse(word);
    }

    public Word SubWord()
    {
        var word = new Word();
        word.Characters.AddRange(Characters.Skip(1));
        return word;
    }

    public Word Reverse()
    {
        var reversedWord = new Word();
        reversedWord.Characters.AddRange(Enumerable.Reverse(Characters));
        return reversedWord;
    }

    private static List<Character> Parse(string word)
    {
        // End of half palindrom indexes;
        int halfIndexCode = 1;
        var halfIndexes = Enumerable.Range(0, word.Length).Select(x => new List<int>()).ToArray();
        // Mid of palindrom indexes;
        int midIndexCode = -1;
        var midIndexes = Enumerable.Range(0, word.Length).Select(x => new List<int>()).ToArray();
        if (word.Length > 1)
        {
            for (int i = 0; i < word.Length - 1; i++)
            {
                // Check for EndOfHalfPalindrom
                string leftWord = word.Substring(0, i + 1);
                string rightWord = Reverse(word.Substring(i + 1, word.Length - i - 1));
                if (leftWord.EndsWith(rightWord) || rightWord.EndsWith(leftWord))
                {
                    halfIndexes[i].Add(halfIndexCode);
                    halfIndexes[i + 1].Add(halfIndexCode);
                    halfIndexCode++;
                }

                // Check for MidOfPalindrom
                if (i == 0 || word.Length < 3)
                {
                    // Word is to short or start index is to small
                    continue;
                }
                leftWord = word.Substring(0, i);
                rightWord = Reverse(word.Substring(i + 1, word.Length - i - 1));
                if (leftWord.EndsWith(rightWord) || rightWord.EndsWith(leftWord))
                {
                    midIndexes[i].Add(midIndexCode);
                    midIndexCode--;
                }
            }
        }

        var result = new List<Character>();
        for (int i = 0; i < word.Length; i++)
        {
            var markers = halfIndexes[i].Union(midIndexes[i]).Select(x => $"{word}:{x}").ToList();
            result.Add(new Character(word[i], markers));
        }

        return result;
    }

    public List<Character> Characters { get; }

    public override string ToString()
    {
        string word = new string(Characters.Select(c => c.C).ToArray());
        return $"{word}";
    }

    private static string Reverse(string word)
    {
        return new string(word.Reverse().ToArray());
    }

}

public class Character
{
    public char C { get; }

    public List<string> Markers { get; }

    public Character(char c, List<string> markers)
    {
        C = c;
        Markers = markers;
    }

    public override string ToString()
    {
        string markersString = string.Join(",", Markers);
        return $"{C}.{markersString}";
    }
}

public enum CharacterType
{
    Normal,
    MidOfPalindrom,
    EndOfHalfPalindrom
}

public class Node
{
    public bool CanStartNewWord { get; private set; }
    public char C { get; private set; }
    public HashSet<string> Markers { get; private set; } = new HashSet<string>();
    public IDictionary<char, Node> Children { get; private set; } = new Dictionary<char, Node>();

    public Node(char c)
    {
        C = c;
    }

    public void StartNewWord()
    {
        CanStartNewWord = true;
    }

    public void AddMarkers(IEnumerable<string> markersToAdd)
    {
        foreach (string markerToAdd in markersToAdd)
        {
            if (!Markers.Contains(markerToAdd))
            {
                Markers.Add(markerToAdd);
            }
        }
    }
}
