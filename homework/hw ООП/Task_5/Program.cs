using System;

class Program
{
    static void Main()
    {
        var label = new Label
        {
            Color = "Red",
            IsVisible = true
        };
        Console.WriteLine("Label тест:");
        Console.WriteLine(label.Draw());

        label.IsVisible = false;
        Console.WriteLine(label.Draw()); 


        // прямоугольник
        var rectSmall = new Rectangle(
            color: "Blue",
            width: 1,
            height: 1,
            minVisibleArea: 2);

        Console.WriteLine("\nФигура слишком маленькая:");
        Console.WriteLine(rectSmall.Draw());

        rectSmall.Resize(3);
        Console.WriteLine(rectSmall.Draw());

        rectSmall.IsVisible = false;
        Console.WriteLine(rectSmall.Draw());
    }
}