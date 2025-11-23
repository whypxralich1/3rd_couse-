using System;

namespace TemperatureSensorApp
{
    public class TemperatureSensor
    {
        public delegate void TemperatureEventHandler(TemperatureSensor sender, int value);

        // обычное событие
        public event TemperatureEventHandler? TemperatureChanged;

        // кастомное событие
        private EventHandler<int>? _criticalLevelReached;

        public event EventHandler<int>? CriticalLevelReached
        {
            add
            {
                _criticalLevelReached += value;
                Console.WriteLine("подписчик был добавлен");
            }
            remove
            {
                _criticalLevelReached -= value;
                Console.WriteLine("подписчик был удалён");
            }
        }

        private Random _random = new Random();

        public void Start()
        {
            int count = _random.Next(5, 11);
            for (int i = 0; i < count; i++)
            {
                int temp = _random.Next(80, 121);
                TemperatureChanged?.Invoke(this, temp);

                if (temp > 100)
                { 
                    // вызов кастомного события
                    _criticalLevelReached?.Invoke(this, temp);
                }
            }
        }
    }
}
