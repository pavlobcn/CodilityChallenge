// ReSharper disable once CheckNamespace
public partial class Character
{
    public override string ToString()
    {
        string markersString = string.Join(",", Markers);
        return $"{C}.{markersString}";
    }
}
public partial class Node
{
    public override string ToString()
    {
        return $"{C},IsWordStart={IsWordStart},IsWordEnd={IsWordEnd},Children.Count=,{Children.Count}";
    }
}