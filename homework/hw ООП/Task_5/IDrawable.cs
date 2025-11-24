public interface IDrawable
{
    string Color { get; set; } //цвет
    bool IsVisible { get; set; } //прозрачность
    string Draw();
}