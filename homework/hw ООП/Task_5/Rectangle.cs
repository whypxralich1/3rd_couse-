class Rectangle : IDrawable, IScalable
{
    private string _color;
    private bool _isVisible;
    private double _scale;
    private double _width;
    private double _height;
    private readonly double _minVisibleArea;

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

    public double Scale
    {
        get => _scale;
        set => _scale = value;
    }

    public double Width
    {
        get => _width;
        set => _width = value;
    }

    public double Height
    {
        get => _height;
        set => _height = value;
    }

    public Rectangle(string color, double width, double height, double minVisibleArea)
    {
        Color = color;
        Width = width;
        Height = height;
        _minVisibleArea = minVisibleArea;
        IsVisible = true;
        Scale = 1.0;
    }

    public string Draw()
    {
        var effectiveWidth = Width * Scale;
        var effectiveHeight = Height * Scale;
        
        var area = effectiveWidth * effectiveHeight;
        if (area < _minVisibleArea)
            return "too small";
        
        return IsVisible ? $"rect W={effectiveWidth}, H={effectiveHeight}, color={Color}" : "hidden";
    }

    public void Resize(double factor)
    {
        Scale *= factor;
        if (Scale < 0)
            Scale = 0;
    }
}