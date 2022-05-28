namespace HtmlNotificationsPoc.Models
{
    public class Subscription
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string PushEndpoint { get; set; }
        public string PushP256DH { get; set; }
        public string PushAuth { get; set; }
    }
}
