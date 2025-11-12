using System;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            Dog dog = new Dog("Собака", 25.5, 10);
            Cat cat = new Cat("Кошка", 4.2, "Черепаховый");

            Console.WriteLine(dog.Description());
            Console.WriteLine(cat.Description());
        }
    }
}