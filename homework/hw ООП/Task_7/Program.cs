using System;

namespace NotificationsApp
{
    internal class Program
    {
        private static void Main()
        {
            var notifier = new Notifier();
            
            notifier.AddHandler(SendEmail);
            notifier.AddHandler(SendSms);
            notifier.AddHandler(AddSignature);

            var firstContext = new NotificationContext { Message = "Привет!" };
            notifier.Run(firstContext);
            PrintResult(firstContext, "Результат первого уведомления:");

            notifier.RemoveHandler(SendSms);
            var secondContext = new NotificationContext { Message = "Как дела?" };
            notifier.Run(secondContext);
            PrintResult(secondContext, "Результат второго уведомления (после удаления одного обработчика):");
        }

        private static void PrintResult(NotificationContext context, string title)
        {
            Console.WriteLine(title);
            Console.WriteLine($"Final message: {context.Message}");
            Console.WriteLine($"Sent by email: {context.SentByEmail}");
            Console.WriteLine($"Sent by SMS: {context.SentBySms}\n");
        }
        private static void SendEmail(NotificationContext context)
        {
            context.SentByEmail = true;
            context.Message += " via email.";
        }

        private static void SendSms(NotificationContext context)
        {
            context.SentBySms = true;
            context.Message += " via sms.";
        }

        private static NotifyHandler AddSignature = ctx =>
        {
            ctx.Message += " — Sent";
        };
    }
}