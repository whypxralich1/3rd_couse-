using TemperatureSensorApp;
class Program
    {
        static void Main()
        {
            var sensor = new TemperatureSensor();

            var alarm = new ConsoleAlarm();
            alarm.Subscribe(sensor);

            var stats = new StatisticsCollector();
            stats.Subscribe(sensor);
            sensor.Start();

            sensor.CriticalLevelReached -= alarm.OnCriticalLevelReached;

            stats.Report();
        }
    }