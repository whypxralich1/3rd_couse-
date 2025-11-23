using TemperatureSensorApp;
    public class StatisticsCollector
    {
        private int _countAbove90 = 0;

        public void Subscribe(TemperatureSensor sensor)
        {
            sensor.TemperatureChanged += OnTemperatureChanged;
        }

        public void Unsubscribe(TemperatureSensor sensor)
        {
            sensor.TemperatureChanged -= OnTemperatureChanged;
        }

        private void OnTemperatureChanged(TemperatureSensor sender, int value)
        {
            if (value > 90)
            {
                _countAbove90++;
            }
        }

        public void Report()
        {
            Console.WriteLine($"Количество случаев температуры выше 90°C: {_countAbove90}");
        }
    }