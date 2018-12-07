using System;
using System.Collections.Generic;
using System.Linq;

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

        List<Word> words = GetWords(S).ToList();
        var palindromWord = words.LastOrDefault(w => w.IsPalindrom);
        if (palindromWord != null)
        {
            return palindromWord.OriginWord;
        }

        string result = NoAnswer;

        var words1 = words.GroupBy(w => w.OriginWord[0]).ToDictionary(g => g.Key, g => g.ToList());
        var words2 = words.GroupBy(w => w.OriginWord[w.OriginWord.Length - 1]).ToDictionary(g => g.Key, g => g.ToList());
        List<SymmetricGroup> symmetricGroups = words.Select(w =>
            new SymmetricGroup(new Sentence(w), new Sentence(), new Difference(w.OriginWord))).ToList();
        while (symmetricGroups.Any(x => x.Sentence.Length < MaxLength))
        {
            result = GetPalindrom(symmetricGroups);
            if (result != NoAnswer)
            {
                return result;
            }

            symmetricGroups = symmetricGroups.SelectMany(x => x.Join(words1, words2)).ToList();

            if (!symmetricGroups.Any())
            {
                return NoAnswer;
            }
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
    public Sentence Sentence { get; private set; }
    public Sentence ReverseSentence { get; private set; }
    public Difference Difference { get; set; }

    public SymmetricGroup(Sentence sentence, Sentence reverseSentence, Difference difference)
    {
        Sentence = sentence;
        ReverseSentence = reverseSentence;
        Difference = difference;
    }

    public string GetPalindrom()
    {
        string palindrom = Solution.NoAnswer;
        if (Sentence.Length == ReverseSentence.Length)
        {
            palindrom = Sentence.GetSentence() + Solution.Space + ReverseSentence.GetSentence();
        }

        return palindrom;
    }

    internal IEnumerable<SymmetricGroup> Join(Dictionary<char, List<Word>> words1, Dictionary<char, List<Word>> words2)
    {
        int len1 = Sentence.Length;
        int len2 = ReverseSentence.Length;
        if (len1 < len2)
        {
            List<Word> words;
            if (!words1.TryGetValue(Difference.CharAtFromEnd(0), out words))
            {
                yield break;
            }
            foreach (Word word in words)
            {
                bool canJoin = true;
                int charCountToCheck = Math.Min(word.OriginWord.Length, len2 - len1);
                for (int i = 1; i < charCountToCheck; i++)
                {
                    if (word.OriginWord[i] != Difference.CharAtFromEnd(i))
                    {
                        canJoin = false;
                        break;
                    }
                }

                if (canJoin)
                {
                    Difference newDifference;
                    if (len2 - len1 >= word.OriginWord.Length)
                    {
                        string baseWord = ReverseSentence.Words.First.OriginWord;
                        newDifference = new Difference(baseWord, 0, len2 - len1 - word.OriginWord.Length);
                    }
                    else
                    {
                        newDifference = new Difference(word.OriginWord, len2 - len1, len2 - len1 - word.OriginWord.Length);
                    }
                    yield return new SymmetricGroup(Sentence.Append(word), ReverseSentence, newDifference);
                }
            }
        }
        if (len1 > len2)
        {
            List<Word> words;
            if (!words2.TryGetValue(Difference.CharAt(0), out words))
            {
                yield break;
            }
            foreach (Word word in words)
            {
                bool canJoin = true;
                int charCountToCheck = Math.Min(word.OriginWord.Length, len1 - len2);
                for (int i = 1; i < charCountToCheck; i++)
                {
                    if (word.OriginWord[word.OriginWord.Length - 1 - i] != Difference.CharAt(i))
                    {
                        canJoin = false;
                        break;
                    }
                }

                if (canJoin)
                {
                    Difference newDifference;
                    if (len1 - len2 >= word.OriginWord.Length)
                    {
                        string baseWord = Sentence.Words.Last.OriginWord;
                        newDifference = new Difference(baseWord, baseWord.Length - len1 + len2 + word.OriginWord.Length, len1 - len2 - word.OriginWord.Length);
                    }
                    else
                    {
                        newDifference = new Difference(word.OriginWord, 0, word.OriginWord.Length - len1 + len2);
                    }
                    yield return new SymmetricGroup(Sentence, ReverseSentence.Prepend(word), newDifference);
                }
            }
        }
    }
}

public partial class Sentence
{
    public int Length { get; private set; }

    public Sentence(Word word)
    {
        Words = new Words(word);
        Length = word.OriginWord.Length;
    }

    public Sentence()
    {
    }

    public Words Words { get; private set; }

    public string GetSentence()
    {
        return string.Join(Solution.Space.ToString(), Words.ToEnumerable());
    }

    public Sentence Append(Word word)
    {
        var sentence = new Sentence();
        sentence.Words = Words == null ? new Words(word) : new Words(Words, word);
        sentence.Length = Length + word.OriginWord.Length;
        return sentence;
    }

    public Sentence Prepend(Word word)
    {
        var sentence = new Sentence();
        sentence.Words = Words == null ? new Words(word) : new Words(word, Words);
        sentence.Length = Length + word.OriginWord.Length;
        return sentence;
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

public class Words
{
    public Word First { get; }
    public Word Last { get; }
    private Word MainWord { get; }
    private Words PreviousWords { get; }
    private Words NextWords { get; }

    public Words(Word word)
    {
        MainWord = word;
        First = word;
        Last = word;
        PreviousWords = null;
        NextWords = null;
    }

    public Words(Word word, Words words)
    {
        MainWord = word;
        First = word;
        Last = words.Last;
        PreviousWords = null;
        NextWords = words;
    }

    public Words(Words words, Word word)
    {
        MainWord = word;
        First = words.First;
        Last = word;
        PreviousWords = words;
        NextWords = null;
    }

    public IEnumerable<string> ToEnumerable()
    {
        if (PreviousWords != null)
        {
            foreach (string word in PreviousWords.ToEnumerable())
            {
                yield return word;
            }
        }

        yield return MainWord.OriginWord;

        if (NextWords != null)
        {
            foreach (string word in NextWords.ToEnumerable())
            {
                yield return word;
            }
        }
    }
}

public class Difference
{
    private readonly string _baseString;
    private readonly int _start;
    private readonly int _length;

    public Difference(string baseString, int start, int length)
    {
        _baseString = baseString;
        _start = start;
        _length = length;
    }

    public Difference(string baseString)
        : this(baseString, 0, baseString.Length)
    {
    }

    public char CharAt(int index)
    {
        return _baseString[_start + index];
    }

    public char CharAtFromEnd(int index)
    {
        return _baseString[_start + _length - index - 1];
    }
}