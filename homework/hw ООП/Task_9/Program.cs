using System;
using System.Collections.Generic;

namespace InventoryExample
{
    class Program
    {
        static void Main()
        {
            var inventory = new Inventory();

            var effect1 = new Effect("atk", 3);
            var effect2 = new Effect("hp", 50);
            var effect3 = new Effect("def", 2);

            var sword = new Item("sword01", "Sword of Testing", Rarity.Rare, new[] { effect1, effect3 });
            var potion = new Item("potion01", "Healing Potion", Rarity.Uncommon, new[] { effect2 });

            inventory.Add(sword);
            inventory.Add(potion);
            Console.WriteLine($"Первый предмет: {inventory[0].Name}");
            Console.WriteLine($"Предмет по Id: {inventory["potion01"].Name}");

            Console.WriteLine("Предметы с редкостью не ниже Uncommon:");
            foreach (var item in inventory.EnumerateByRarity(Rarity.Uncommon))
            {
                Console.WriteLine($"- {item.Name} ({item.Rarity})");
            }

            inventory.RemoveAt(1);

            Console.WriteLine($"Всего предметов: {inventory.Count}");

            try
            {
                var removedItem = inventory[1];
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Попытка доступа к несуществующему предмету по индексу");
            }
        }
    }
}