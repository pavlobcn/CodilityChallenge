using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

class Solution
{
    private const int MaxLength = 50;
    private const string NoAnswer = "NO";

    public string solution(string S)
    {
        var root = new Node();

        foreach (Word word in S.Split(' ').Select(w => new Word(w)))
        {
            //ProcessWord(root, word);
        }

        var currentSentences = root.Children.Select(x => new SentenceTreeNode(x.Key, x.Value, null)).ToList();
        string result = "NO";
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

        return result;
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
        if (sentence.C != ' ')
        {
            return NoAnswer;
        }

        string s = string.Empty;
        string stringWithoutSpaces = string.Empty;
        while (sentence != null)
        {
            if (sentence.C != ' ')
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

    private static void ProcessWord(Node node, string word)
    {
        if (string.IsNullOrEmpty(word))
        {
            node.IsEndOfWord = true;
            return;
        }

        Node child;
        char firstLetter = word[0];
        if (!node.Children.TryGetValue(firstLetter, out child))
        {
            child = new Node();
            node.Children[firstLetter] = child;
        }

        ProcessWord(child, word.Substring(1));
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
            _parent = parent != null && parent.C == ' ' ? parent.Parent : parent;
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
            if (_node.IsEndOfWord)
            {
                _children.Add(new SentenceTreeNode(' ', root, this));
            }

            _children.AddRange(_node.Children.Select(x => new SentenceTreeNode(x.Key, x.Value, this)));
        }
    }

    private class Node
    {
        private IDictionary<char, Node> _children = new Dictionary<char, Node>();

        public IDictionary<char, Node> Children => _children;

        public bool IsEndOfWord { get; set; }
    }
}

public class Word
{
    public Word(string word)
    {
        Characters = Parse(word);
    }

    private static IList<Character> Parse(string word)
    {
        int index = -1;
        CharacterType type = CharacterType.Normal;
        if (word.Length > 1)
        {
            for (int i = 0; i < word.Length - 1; i++)
            {
                // Check for EndOfHalfPalindrom
                string leftWord = word.Substring(0, i + 1);
                string rightWord = Reverse(word.Substring(i + 1, word.Length - i - 1));
                if (leftWord.EndsWith(rightWord) || rightWord.EndsWith(leftWord))
                {
                    index = i;
                    type = CharacterType.EndOfHalfPalindrom;
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
                    index = i;
                    type = CharacterType.EndOfHalfPalindrom;
                }
            }
        }

        var result = new List<Character>();
        for (int i = 0; i < word.Length; i++)
        {
            result.Add(new Character(word[i], i == index ? type : CharacterType.Normal));
        }

        return result;
    }

    public IList<Character> Characters { get; }

    public override string ToString()
    {
        string word = new string(Characters.Select(c => c.C).ToArray());
        CharacterType type = CharacterType.Normal;
        int index = -1;
        for (int i = 0; i < Characters.Count; i++)
        {
            if (Characters[i].Type != CharacterType.Normal)
            {
                type = Characters[i].Type;
                index = i;
                break;
            }
        }

        return $"{word}:{type}:{index}";
    }

    private static string Reverse(string word)
    {
        return new string(word.Reverse().ToArray());
    }

}

public class Character
{
    public char C { get; }

    public CharacterType Type { get; }

    public Character(char c, CharacterType type)
    {
        C = c;
        Type = type;
    }

    public override string ToString()
    {
        return $"{C}:{Type}";
    }
}

public enum CharacterType
{
    Normal,
    MidOfPalindrom,
    EndOfHalfPalindrom
}