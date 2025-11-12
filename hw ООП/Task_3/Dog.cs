using System;

namespace Task3
{
    public class Dog : Animal
    {
        // Поле: Длина шерсти
        public int HairLength { get; private set; }

        // Конструктор
        public Dog(string species, double weight, int hairLength) : base(species, weight)
        {
            HairLength = hairLength;
        }

        // Переопределён метод Description
        public override string Description()
        {
            return $"{base.Description()} Его шерсть длиной {HairLength} см.";
        }
    }
}