class Label : IDrawable
{
    private string _color;
    private bool _isVisible;

    public string Color
    {
        get => _color;
        set => _color = value;
    }

    public bool IsVisible
    {
        get => _isVisible;
        set => _isVisible = value;
    }

    public string Draw()
    {
        return IsVisible ? "drawn" : "hidden";
    }
}