namespace NotificationsApp
{
    public class Notifier
    {
        public event NotifyHandler Handlers;

        public void AddHandler(NotifyHandler handler)
        {
            Handlers += handler;
        }

        public void RemoveHandler(NotifyHandler handler)
        {
            Handlers -= handler;
        }

        public void Run(NotificationContext context)
        {
            if (Handlers != null)
            {
                foreach (var handler in Handlers.GetInvocationList())
                    ((NotifyHandler)handler)(context);
            }
        }
    }
}