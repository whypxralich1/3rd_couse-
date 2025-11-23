using System;

public interface IDrawable
{
    string Color { get; set; }
    bool IsVisible { get; set; }
    string Draw()
    {
        return IsVisible ? "drawn" : "hidden";
    }
}

public interface IScalable
{
    double Scale { get; set; }
    void Resize(double factor)
    {
        Scale *= factor;
        if (Scale < 0)
            Scale = 0;
    }
}

public class Label : IDrawable
{
    public string Color { get; set; }
    public bool IsVisible { get; set; }

    public Label(string color, bool isVisible = true)
    {
        Color = color;
        IsVisible = isVisible;
    }

    public string Draw()
    {
        return IsVisible ? "label drawn" : "hidden";
    }
}

public class Rectangle : IDrawable, IScalable
{
    private double width;
    private double height;
    private double scale;
    public string Color { get; set; }
    public bool IsVisible { get; set; }
    public double Width
    {
        get => width;
        set => width = value >= 0 ? value : 0;
    }
    public double Height
    {
        get => height;
        set => height = value >= 0 ? value : 0;
    }
    public double MinVisibleArea { get; set; } = 1.0;

    public double Scale
    {
        get => scale;
        set
        {
            scale = value;
            if (scale < 0)
                scale = 0;
        }
    }

    public Rectangle(double width, double height, string color, bool isVisible = true, double minVisibleArea = 1.0)
    {
        Width = width;
        Height = height;
        Color = color;
        IsVisible = isVisible;
        MinVisibleArea = minVisibleArea;
        Scale = 1.0;
    }

    public void Resize(double factor)
    {
        Scale *= factor;
        if (Scale < 0)
            Scale = 0;
    }

    public string Draw()
    {
        double effectiveWidth = Width * Scale;
        double effectiveHeight = Height * Scale;
        double area = effectiveWidth * effectiveHeight;

        if (area < MinVisibleArea)
        {
            return "too small";
        }
        if (!IsVisible)
        {
            return "hidden";
        }
        return $"rect W={effectiveWidth:F2}, H={effectiveHeight:F2}, color={Color}";
    }
}