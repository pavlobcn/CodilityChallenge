using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

        var words1 = words.GroupBy(w => w.OriginWord[0]).ToDictionary(g => g.Key, g => g.ToList());
        var words2 = words.GroupBy(w => w.OriginWord[w.OriginWord.Length - 1]).ToDictionary(g => g.Key, g => g.ToList());
        List<SymmetricGroup> symmetricGroups = words.Select(w =>
            new SymmetricGroup(new Sentence(w), new Sentence(), new LinkedList<char>(w.OriginWord))).ToList();
        int iteration = 0;
        while (iteration < MaxLength)
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
    public Sentence Sentence { get; private set; }
    public Sentence ReverseSentence { get; private set; }
    public LinkedList<char> Difference { get; set; }

    public SymmetricGroup(Sentence sentence, Sentence reverseSentence, LinkedList<char> difference)
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
            using (var cache = new CachedEnumerable<char>(ReverseSentence.GetReverseChars(len1)))
            {
                var reverseSentenceChars = cache.GetEnumerator();
                reverseSentenceChars.MoveNext();
                var reverseSentenceChar = reverseSentenceChars.Current;
                List<Word> words;
                if (!words1.TryGetValue(reverseSentenceChar, out words))
                {
                    yield break;
                }
                foreach (Word word in words)
                {
                    reverseSentenceChars = cache.GetEnumerator();
                    reverseSentenceChars.MoveNext();
                    bool canJoin = true;
                    int charCountToCheck = Math.Min(word.OriginWord.Length, len2 - len1);
                    for (int i = 1; i < charCountToCheck; i++)
                    {
                        reverseSentenceChars.MoveNext();
                        reverseSentenceChar = reverseSentenceChars.Current;
                        if (word.OriginWord[i] != reverseSentenceChar)
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
        }
        if (len1 > len2)
        {
            using (var cache = new CachedEnumerable<char>(Sentence.GetChars(len2)))
            {
                var sentenceChars = Sentence.GetChars(len2).GetEnumerator();
                sentenceChars.MoveNext();
                var sentenceChar = sentenceChars.Current;
                List<Word> words;
                if (!words2.TryGetValue(sentenceChar, out words))
                {
                    yield break;
                }
                foreach (Word word in words)
                {
                    sentenceChars = cache.GetEnumerator();
                    sentenceChars.MoveNext();
                    bool canJoin = true;
                    int charCountToCheck = Math.Min(word.OriginWord.Length, len1 - len2);
                    for (int i = 1; i < charCountToCheck; i++)
                    {
                        sentenceChars.MoveNext();
                        sentenceChar = sentenceChars.Current;
                        if (word.OriginWord[word.OriginWord.Length - 1 - i] != sentenceChar)
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
        }

        if (len1 == len2)
        {
            foreach (Word word in words1.SelectMany(g => g.Value))
            {
                yield return new SymmetricGroup(Sentence.Append(word), ReverseSentence, new LinkedList<char>(w.OriginWord));
            }
        }
    }
}

public partial class Sentence
{
    public int Length { get; private set; }

    public Sentence(Word word)
    {
        Words.Add(word);
        Length = word.OriginWord.Length;
    }

    public Sentence()
    {
    }

    public List<Word> Words { get; } = new List<Word>();

    public string GetSentence()
    {
        return string.Join(Solution.Space.ToString(), Words.Select(w => w.OriginWord));
    }

    public Sentence Append(Word word)
    {
        var sentence = new Sentence();
        sentence.Words.AddRange(Words);
        sentence.Words.Add(word);
        sentence.Length = Length + word.OriginWord.Length;
        return sentence;
    }

    public Sentence Prepend(Word word)
    {
        var sentence = new Sentence();
        sentence.Words.Add(word);
        sentence.Words.AddRange(Words);
        sentence.Length = Length + word.OriginWord.Length;
        return sentence;
    }

    public IEnumerable<char> GetChars(int startIndex)
    {
        foreach (Word word in Words)
        {
            if (word.OriginWord.Length <= startIndex)
            {
                startIndex -= word.OriginWord.Length;
                continue;
            }

            for (int i = startIndex; i < word.OriginWord.Length; i++)
            {
                yield return word.OriginWord[i];
                startIndex--;
            }
        }
    }

    public IEnumerable<char> GetReverseChars(int startIndexFromEnd)
    {
        foreach (Word word in Enumerable.Reverse(Words))
        {
            if (word.OriginWord.Length < startIndexFromEnd)
            {
                startIndexFromEnd -= word.OriginWord.Length;
                continue;
            }

            for (int i = word.OriginWord.Length - startIndexFromEnd - 1; i >= 0 ; i--)
            {
                yield return word.OriginWord[i];
                startIndexFromEnd--;
            }
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

public class CachedEnumerable<T> : IEnumerable<T>, IDisposable
{
    IEnumerator<T> _enumerator;
    readonly List<T> _cache = new List<T>();

    public CachedEnumerable(IEnumerable<T> enumerable)
        : this(enumerable.GetEnumerator())
    {
    }

    public CachedEnumerable(IEnumerator<T> enumerator)
    {
        _enumerator = enumerator;
    }

    public IEnumerator<T> GetEnumerator()
    {
        // The index of the current item in the cache.
        int index = 0;

        // Enumerate the _cache first
        for (; index < _cache.Count; index++)
        {
            yield return _cache[index];
        }

        // Continue enumeration of the original _enumerator, 
        // until it is finished. 
        // This adds items to the cache and increment 
        for (; _enumerator != null && _enumerator.MoveNext(); index++)
        {
            var current = _enumerator.Current;
            _cache.Add(current);
            yield return current;
        }

        if (_enumerator != null)
        {
            _enumerator.Dispose();
            _enumerator = null;
        }

        // Some other users of the same instance of CachedEnumerable
        // can add more items to the cache, 
        // so we need to enumerate them as well
        for (; index < _cache.Count; index++)
        {
            yield return _cache[index];
        }
    }

    public void Dispose()
    {
        if (_enumerator != null)
        {
            _enumerator.Dispose();
            _enumerator = null;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}