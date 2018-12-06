using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

class Solution
{
    private const int MaxLength = 1000;
    public const string NoAnswer = "NO";
    public const char Space = ' ';

    public string solution(string S)
    {
        string oneLetterPalindrom = GetOneLetterPalindrom(S);
        if (oneLetterPalindrom != NoAnswer)
        {
            return oneLetterPalindrom;
        }

        List<Word> words = GetWords(S).ToList();
        var palindromWord = words.LastOrDefault(w => w.IsPalindrom);
        if (palindromWord != null)
        {
            return palindromWord.OriginWord;
        }

        string result = NoAnswer;

        List<SymmetricGroup> symmetricGroups = words.Select(w => new SymmetricGroup(new Sentence(w), new Sentence())).ToList();
        int iteration = 0;
        while (iteration < MaxLength)
        {
            result = GetPalindrom(symmetricGroups);
            if (result != NoAnswer)
            {
                return result;
            }

            symmetricGroups = symmetricGroups.SelectMany(x => x.Join(words)).ToList();

            if (!symmetricGroups.Any())
            {
                return NoAnswer;
            }

            iteration++;
        }
        return result;
    }

    private IEnumerable<Word> GetWords(string sentence)
    {
        foreach (string w in sentence.Split(Space).Distinct())
        {
            var word = Word.Parse(w);
            yield return word;
            if (word.IsPalindrom)
            {
                yield break;
            }
        }
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
    public SymmetricGroup(Sentence sentence, Sentence reverseSentence)
    {
        Sentence = sentence;
        ReverseSentence = reverseSentence;
    }

    public Sentence Sentence { get; }
    public Sentence ReverseSentence { get; }

    public string GetPalindrom()
    {
        string palindrom = Solution.NoAnswer;
        if (Sentence.Length == ReverseSentence.Length)
        {
            if (Sentence.Words.Count == ReverseSentence.Words.Count)
            {
                bool sentenceIsResult = true;
                for (int i = 0; i < Sentence.Words.Count; i++)
                {
                    if (Sentence.Words[i] != ReverseSentence.Words[i])
                    {
                        sentenceIsResult = false;
                        break;
                    }
                }
                if (sentenceIsResult)
                {
                    palindrom = Sentence.GetSentence();
                }
            }
        }

        if (palindrom != Solution.NoAnswer)
        {
            if (!palindrom.IsPalindrom())
            {
                throw new Exception("Bug3");
            }
        }

        return palindrom;
    }

    internal IEnumerable<SymmetricGroup> Join(List<Word> words)
    {
        int len1 = Sentence.Length;
        int len2 = ReverseSentence.Length;
        if (len1 < len2)
        {
            foreach (Word word in words)
            {
                bool canJoin = true;
                int charCountToCheck = Math.Min(word.OriginWord.Length, len2 - len1);
                for (int i = 0; i < charCountToCheck; i++)
                {
                    if (word.OriginWord[i] != ReverseSentence[len2 - len1 - i - 1])
                    {
                        canJoin = false;
                        break;
                    }
                }

                if (canJoin)
                {
                    yield return new SymmetricGroup(Sentence.Append(word), ReverseSentence);
                }
            }
        }
        if (len1 > len2)
        {
            foreach (Word word in words)
            {
                bool canJoin = true;
                int charCountToCheck = Math.Min(word.OriginWord.Length, len1 - len2);
                for (int i = 0; i < charCountToCheck; i++)
                {
                    if (word.OriginWord[word.OriginWord.Length - 1 - i] != Sentence[len2 + i])
                    {
                        canJoin = false;
                        break;
                    }
                }

                if (canJoin)
                {
                    yield return new SymmetricGroup(Sentence, ReverseSentence.Prepend(word));
                }
            }
        }

        if (len1 == len2)
        {
            foreach (Word word in words)
            {
                yield return new SymmetricGroup(Sentence.Append(word), ReverseSentence);
            }
        }
    }
}

public partial class Sentence
{
    private readonly List<Word> _words = new List<Word>();

    public Sentence(Word word)
    {
        _words.Add(word);
    }

    public Sentence()
    {
    }

    public int Length => _words.Sum(w => w.OriginWord.Length);
    public List<Word> Words => _words;

    public string GetSentence()
    {
        return string.Join(Solution.Space.ToString(), _words.Select(w => w.OriginWord));
    }

    public Sentence Append(Word word)
    {
        var sentence = new Sentence();
        sentence._words.AddRange(_words);
        sentence._words.Add(word);
        return sentence;
    }

    public Sentence Prepend(Word word)
    {
        var sentence = new Sentence();
        sentence._words.Add(word);
        sentence._words.AddRange(_words);
        return sentence;
    }

    public char this[int index]
    {
        get
        {
            foreach (Word word in _words)
            {
                if (word.OriginWord.Length > index)
                {
                    return word.OriginWord[index];
                }

                index -= word.OriginWord.Length;
            }

            throw new IndexOutOfRangeException();
        }
    }
}

public partial class Word
{
    public string OriginWord { get; }
    public bool IsPalindrom { get; }

    private Word(string originWord, bool isPalindrom)
    {
        OriginWord = originWord;
        IsPalindrom = isPalindrom;
    }

    public static Word Parse(string word)
    {
        bool isPalindrom = true;
        var len = word.Length;
        var halfLen = word.Length / 2;
        for (int i = 0; i < halfLen; i++)
        {
            if (word[i] != word[len - i - 1])
            {
                isPalindrom = false;
                break;
            }
        }
        return new Word(word, isPalindrom);
    }
}

public static class Extensions
{
    public static string ReverseString(this string s)
    {
        return new string(s.Reverse().ToArray());
    }

    public static bool IsPalindrom(this string s)
    {
        var ss = s.Replace(" ", "");
        return ss == ss.ReverseString();
    }
}