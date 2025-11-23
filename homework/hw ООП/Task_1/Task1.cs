class Program
{
    static void Main()
    {
        var car1 = new Vehicle("БМВ", "E30", 150000);
        var car2 = new Vehicle("Нисан", "X-Trail", 252000);
        var car3 = new Vehicle("Шкода", "Октавиа III", 220800);

        Console.WriteLine(car1);
        Console.WriteLine(car2);
        Console.WriteLine(car3);
        Console.WriteLine();

        car1.Drive(41023);
        car2.Drive(12453);
        car3.Drive(86326);
        Console.WriteLine();

        Console.WriteLine("После поездок:");
        Console.WriteLine(car1);
        Console.WriteLine(car2);
        Console.WriteLine(car3);
    }
}