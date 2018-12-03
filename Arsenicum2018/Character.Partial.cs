// ReSharper disable once CheckNamespace
public partial class Character
{
    public override string ToString()
    {
        string markersString = string.Join(",", Markers);
        return $"{C}.{markersString}";
    }
}