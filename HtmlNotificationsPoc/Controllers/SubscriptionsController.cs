using HtmlNotificationsPoc.Configuration;
using HtmlNotificationsPoc.Models;
using HtmlNotificationsPoc.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json;
using WebPush;

namespace HtmlNotificationsPoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionsController : ControllerBase
    {
        private readonly SubscriptionRepository repository;
        private readonly PushNotificationOptions pushNotificationOptions;

        public SubscriptionsController(SubscriptionRepository repository, IOptionsSnapshot<PushNotificationOptions> options)
        {
            this.repository = repository;
            this.pushNotificationOptions = options.Value;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(repository.All());
        }

        [HttpPut]
        public IActionResult Put(Subscription subscription)
        {
            repository.Create(subscription);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(Guid id)
        {
            repository.Delete(id);
            return Ok();
        }

        [HttpPost]
        [Route("notify/{id}")]
        public IActionResult Notify([FromRoute] Guid id, [FromBody] NotificationPayload payload)
        {
            var subscription = repository.Single(id);

            if (subscription != null)
            {
                var pushSubscription = new PushSubscription(subscription.PushEndpoint, subscription.PushP256DH, subscription.PushAuth);
                var vapidDetails = new VapidDetails("mailto:example@example.com", pushNotificationOptions.PublicKey, pushNotificationOptions.PrivateKey);
                var jsonPayload = JsonSerializer.Serialize(payload, new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                using (var webPushClient = new WebPushClient())
                {
                    webPushClient.SendNotification(pushSubscription, jsonPayload, vapidDetails);
                }

                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
