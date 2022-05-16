using Microsoft.AspNetCore.SignalR;

namespace HtmlNotificationsPoc.SignalR
{
    public class NotificationsHub : Hub
    {
        public async Task SendMessage()
        {
            await Clients.All.SendAsync("Notify", new NotificationPayload()
            {
                Title = "Notification!",
                Body = DateTime.Now.ToString()
            });
        }
    }
}
