using System.Linq;

// ReSharper disable once CheckNamespace
public partial class SymmetricGroup
{
    public override string ToString()
    {
        return Sentences.Select(x => x.ToString()).FirstOrDefault();
    }
}