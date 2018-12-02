using System;
using System.Collections.Generic;
using System.Linq;

class Solution
{
    private const int MaxLength = 600000;
    private const string NoAnswer = "NO";
    public const char Space = ' ';
    public const char StartOfSentence = '$';

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

        var symmetricGroup = new SymmetricGroup(
            root1.Children.Select(x => new SentenceTreeNode(x.Key, x.Value, null)).ToList(),
            root2.Children.Select(x => new SentenceTreeNode(x.Key, x.Value, null)).ToList());
        List<SymmetricGroup> symmetricGroups = symmetricGroup.Join();
        int iteration = 0;
        while (iteration < MaxLength)
        {
            symmetricGroups = symmetricGroups.Where(x => x.Sentences.Any()).ToList();
            if (!symmetricGroups.Any())
            {
                return NoAnswer;
            }

            result = GetPalindrom(symmetricGroups);
            if (result != NoAnswer)
            {
                return result;
            }

            symmetricGroups = GetNextSymmetricGroups(symmetricGroups, root1, root2);

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

        if (len > 2)
        {
            for (int i = 2; i < len - 2; i++)
            {
                if (s[i - 1] == Space)
                {
                    if (s[i + 1] == Space)
                    {
                        return s[i].ToString();
                    }

                    i += 2;
                }
            }
        }

        return NoAnswer;
    }

    private static List<SymmetricGroup> GetNextSymmetricGroups(List<SymmetricGroup> symmetricGroups, Node root1, Node root2)
    {
        return symmetricGroups.SelectMany(x =>
        {
            List<SentenceTreeNode> newSentences = SentenceTreeNode.GetNewSentence(x.Sentences, root1);
            List<SentenceTreeNode> newReversedSentences = SentenceTreeNode.GetNewSentence(x.ReverseSentences, root2);

            var newSymmetricGroup = new SymmetricGroup(newSentences, newReversedSentences);
            return newSymmetricGroup.Join();
        }).ToList();
    }

    private static void GetTree(string sentence, out Node root1, out Node root2)
    {
        var words = sentence.Split(Space).Select(w => new Word(w)).ToArray();
        root1 = new Node(StartOfSentence);
        root2 = new Node(StartOfSentence);
        foreach (Word word in words)
        {
            // Get tree for origin words
            ProcessWord(root1, word, 0);
            // Get tree for reversed words
            ProcessWord(root2, word.Reverse(), 0);
        }
    }

    private string GetPalindrom(List<SymmetricGroup> symmetricGroups)
    {
        string palindrom = symmetricGroups.Select(GetPalindrom).FirstOrDefault(x => x != NoAnswer);
        if (string.IsNullOrEmpty(palindrom))
        {
            return NoAnswer;
        }

        return palindrom;
    }

    private string GetPalindrom(SymmetricGroup symmetricGroup)
    {
        SentenceTreeNode sentence1;
        SentenceTreeNode sentence2;
        // Check end of word
        sentence1 = symmetricGroup.Sentences.FirstOrDefault(x => x.Node.CanStartNewWord);
        if (sentence1 != null)
        {
            sentence2 = symmetricGroup.ReverseSentences.FirstOrDefault(x => x.Node.CanStartNewWord);
            if (sentence2 != null)
            {
                return sentence1.Sentence + " " + sentence2.Sentence.ReverseString();
            }
        }

        List<string> markers1 = symmetricGroup.Sentences.SelectMany(x => x.Node.Markers).ToList();
        if (markers1.Any())
        {
            List<string> markers2 = symmetricGroup.ReverseSentences.SelectMany(x => x.Node.Markers).ToList();
            IEnumerable<string> intersection =
                markers1.Select(x => x.Trim('L', 'R')).Intersect(markers2).Select(x => x.Trim('L', 'R'));
            foreach (string intersectionMarker in intersection)
            {
                if (intersectionMarker.Contains('-'))
                {
                    sentence1 = symmetricGroup.Sentences.First(x => x.Node.Markers.Contains(intersectionMarker));
                    sentence2 = symmetricGroup.ReverseSentences.First(x => x.Node.Markers.Contains(intersectionMarker));

                    return sentence1.Sentence + sentence2.Sentence.ReverseString().Substring(1);
                }

                sentence1 = symmetricGroup.Sentences.FirstOrDefault(x =>
                    x.Node.Markers.Contains(intersectionMarker + "L"));
                if (sentence1 != null)
                {
                    sentence2 = symmetricGroup.ReverseSentences.FirstOrDefault(
                        x => x.Node.Markers.Contains(intersectionMarker + "R"));

                    if (sentence2 != null)
                    {
                        return sentence1.Sentence + sentence2.Sentence.ReverseString().Substring(1);
                    }
                }
            }
        }

        return NoAnswer;
    }

    private static void ProcessWord(Node node, Word word, int charIndex)
    {
        if (charIndex == word.Characters.Count)
        {
            node.StartNewWord();
            return;
        }

        Node child;
        Character firstCharacter = word.Characters[charIndex];
        if (!node.Children.TryGetValue(firstCharacter.C, out child))
        {
            child = new Node(firstCharacter.C);
            node.Children[firstCharacter.C] = child;
        }
        child.AddMarkers(firstCharacter.Markers);
        ProcessWord(child, word, charIndex + 1);
    }
}

public class SymmetricGroup
{
    public SymmetricGroup(List<SentenceTreeNode> sentences, List<SentenceTreeNode> reverseSentences)
    {
        Sentences = sentences;
        ReverseSentences = reverseSentences;
    }

    public List<SentenceTreeNode> Sentences { get; }
    public List<SentenceTreeNode> ReverseSentences { get; }

    public List<SymmetricGroup> Join()
    {
        var join = Sentences
            .Join(ReverseSentences, x => x.C, y => y.C, (x, y) => new Tuple<SentenceTreeNode, SentenceTreeNode>(x, y))
            .ToList();
        var newSentences = join.Select(x => x.Item1).Distinct().ToList();
        var newReverseSentences = join.Select(y => y.Item2).Distinct().ToList();
        return newSentences.Join(newReverseSentences, x => x.C, y => y.C,
                (x, y) => new SymmetricGroup(new List<SentenceTreeNode> { x }, new List<SentenceTreeNode> { y }))
            .ToList();
    }

    public override string ToString()
    {
        return Sentences.Select(x => x.ToString()).FirstOrDefault();
    }
}

public class SentenceTreeNode
{
    private readonly char _char;
    private readonly Node _node;
    private readonly SentenceTreeNode _parent;
    private readonly List<SentenceTreeNode> _children = new List<SentenceTreeNode>();

    public SentenceTreeNode(char c, Node node, SentenceTreeNode parent, bool startNewWord = false)
    {
        _char = c;
        _node = node;
        _parent = parent != null && parent.C == Solution.Space ? parent.Parent : parent;
        StartNewWord = startNewWord;
    }

    public char C => _char;

    public Node Node => _node;

    public List<SentenceTreeNode> Children => _children;

    public SentenceTreeNode Parent => _parent;

    public string Sentence
    {
        get
        {
            return string.Concat(
                Parent == null ? String.Empty : Parent.Sentence,
                StartNewWord ? Solution.Space.ToString() : string.Empty,
                C);
        }
    }

    public bool StartNewWord { get; private set; }

    public override string ToString()
    {
        return Sentence;
    }

    public static List<SentenceTreeNode> GetNewSentence(List<SentenceTreeNode> sentences, Node root)
    {
        var newSentences = new List<SentenceTreeNode>();
        foreach (SentenceTreeNode sentenceTreeNode in sentences)
        {
            if (sentenceTreeNode.Node.CanStartNewWord)
            {
                newSentences.AddRange(root.Children.Select(child => new SentenceTreeNode(child.Key, child.Value, sentenceTreeNode, true)));
            }

            newSentences.AddRange(sentenceTreeNode.Node.Children.Select(child => new SentenceTreeNode(child.Key, child.Value, sentenceTreeNode)));
        }

        return newSentences;
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
        var halfIndexes = Enumerable.Range(0, word.Length).Select(x => new List<string>()).ToArray();
        // Mid of palindrom indexes;
        int midIndexCode = -3;
        var midIndexes = Enumerable.Range(0, word.Length).Select(x => new List<string>()).ToArray();

        midIndexes[0].Add("-1");
        midIndexes[word.Length - 1].Add("-2");

        if (word.Length > 1)
        {
            for (int i = 0; i < word.Length - 1; i++)
            {
                // Check for EndOfHalfPalindrom
                string leftWord = word.Substring(0, i + 1);
                string rightWord = word.Substring(i + 1, word.Length - i - 1).ReverseString();
                if (leftWord.EndsWith(rightWord) || rightWord.EndsWith(leftWord))
                {
                    halfIndexes[i].Add(halfIndexCode + "L");
                    halfIndexes[i + 1].Add(halfIndexCode + "R");
                    halfIndexCode++;
                }

                // Check for MidOfPalindrom
                if (i == 0 || word.Length < 3)
                {
                    // Word is to short or start index is to small
                    continue;
                }
                leftWord = word.Substring(0, i);
                rightWord = word.Substring(i + 1, word.Length - i - 1).ReverseString();
                if (leftWord.EndsWith(rightWord) || rightWord.EndsWith(leftWord))
                {
                    midIndexes[i].Add(midIndexCode.ToString());
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

public static class Extensions
{
    public static string ReverseString(this string s)
    {
        return new string(s.Reverse().ToArray());
    }
}
