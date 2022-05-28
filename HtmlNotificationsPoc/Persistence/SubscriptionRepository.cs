using HtmlNotificationsPoc.Models;

namespace HtmlNotificationsPoc.Persistence
{
    public class SubscriptionRepository
    {
        private List<Subscription> subscriptions = new List<Subscription>();

        public List<Subscription> All()
        {
            return subscriptions;
        }

        public void Create(Subscription subscription)
        {
            if (subscriptions.Any(s => s.PushP256DH == subscription.PushP256DH && s.PushAuth == subscription.PushAuth))
                throw new ArgumentException("Specified subscription already exists");

            subscriptions.Add(subscription);
        }
    }
}
