namespace NotificationsApp
{
    public class NotificationContext
    {
        public string Message { get; set; }
        public bool SentByEmail { get; set; }
        public bool SentBySms { get; set; }
    }
}