using HtmlNotificationsPoc.Models;
using HtmlNotificationsPoc.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace HtmlNotificationsPoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionsController : ControllerBase
    {
        private readonly SubscriptionRepository repository;

        public SubscriptionsController(SubscriptionRepository repository)
        {
            this.repository = repository;
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
    }
}
