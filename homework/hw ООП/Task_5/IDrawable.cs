public interface IDrawable
{
    string Color { get; set; }
    bool IsVisible { get; set; }
    string Draw();
}