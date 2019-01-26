// ReSharper disable once CheckNamespace
public partial class Group
{
    public override string ToString()
    {
        return _s.Substring(_startIndex, Length);
    }
}