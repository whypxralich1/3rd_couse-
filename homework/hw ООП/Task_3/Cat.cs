using System;

namespace Task3
{
    public class Cat : Animal
    {
        public string Color { get; private set; }
        public Cat(string species, double weight, string color) : base(species, weight)
        {
            Color = color;
        }
        public override string Description()
        {
            return $"{base.Description()} Окрас кота — {Color}.";
        }
    }
}