using System;
using TemperatureSensorApp;

    public class ConsoleAlarm
    {
        public void Subscribe(TemperatureSensor sensor)
        {
            sensor.TemperatureChanged += OnTemperatureChanged;
            sensor.CriticalLevelReached += OnCriticalLevelReached;
        }

        public void Unsubscribe(TemperatureSensor sensor)
        {
            sensor.TemperatureChanged -= OnTemperatureChanged;
            sensor.CriticalLevelReached -= OnCriticalLevelReached;
        }

        public void OnTemperatureChanged(TemperatureSensor sender, int value)
        {
            Console.WriteLine($"Текущая температура: {value} °C");
        }

        public void OnCriticalLevelReached(object? sender, int value)
        {
            Console.WriteLine("внимание, перегрев");
        }
    }
