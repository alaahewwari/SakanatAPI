using BusinessLogic.Services.NotificationServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController(
        INotificationServices notificationServices
        ) : ApiController
    {
        [HttpGet]
        [Route(ApiRoutes.Notifications.GetNotifications)]
        public async Task<IActionResult> GetAllNotificationsByUserIdAsync([FromQuery] SieveModel sieveModel, [FromRoute] Guid userId)
        {
            var response = await notificationServices.GetAllNotificationsByUserIdAsync(sieveModel, userId);
            return response.Match(
                               Ok,
                          Problem
                                 );
        }
        [HttpGet]
        [Route(ApiRoutes.Notifications.GetNotificationById)]
        public async Task<IActionResult> GetNotificationByIdAsync([FromRoute] Guid id)
        {
            var response = await notificationServices.GetNotificationByIdAsync(id);
            return response.Match(
                                  Ok,
                                  Problem
                                  );
        }

        [HttpPut]
        [Route(ApiRoutes.Notifications.UpdateNotificationsStatus)]
        public async Task<IActionResult> UpdateNotificationsStatusAsync([FromRoute] Guid id)
        {
            var response = await notificationServices.UpdateNotificationsStatusAsync(id);
            return response.Match(
                               Ok,
                               Problem
                               );
        }
    }
}
