using System;

namespace Task3
{
    public class Animal
    {
        public string Species { get; protected set; }
        public double Weight { get; protected set; }

        public Animal(string species, double weight)
        {
            this.Species = species;
            this.Weight = weight;
        }
        public virtual string Description()
        {
            return $"Это {Species}, массой {Weight:F2} кг.";
        }
    }
}