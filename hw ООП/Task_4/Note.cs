public class Note : Document
{
    private bool _pinned;

    public bool Pinned
    {
        get => _pinned;
        set => _pinned = value;
    }

    public Note(string title, string content, bool pinned = false) : base(title, content)
    {
        Pinned = pinned;
    }

    public void Pin()
    {
        Pinned = true;
    }

    public override string Print()
    {
        string pinInfo = Pinned ? "[Pinned]" : string.Empty;
        return $"{pinInfo} {base.Print()}".Trim();
    }
}