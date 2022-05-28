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

        public void Delete(Guid id)
        {
            var subscription = subscriptions.FirstOrDefault(s => s.Id == id);

            if (subscription != null)
                subscriptions.Remove(subscription);
        }

        public Subscription Single(Guid id)
        {
            return subscriptions.SingleOrDefault(s => s.Id == id);
        }
    }
}
