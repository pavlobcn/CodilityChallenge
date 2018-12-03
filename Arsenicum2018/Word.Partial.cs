using System.Linq;

// ReSharper disable once CheckNamespace
public partial class Word
{
    public override string ToString()
    {
        string word = new string(Characters.Select(c => c.C).ToArray());
        return $"{word}";
    }
}