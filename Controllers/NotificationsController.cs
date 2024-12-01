using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/notifications")]
public class NotificationsController : ControllerBase
{
    private readonly NotificationService _service;

    public NotificationsController(NotificationService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllNotifications()
    {
        var notifications = await _service.GetAllNotificationsAsync();
        return Ok(notifications);
    }

    [HttpGet("{recipientId}")]
    public async Task<IActionResult> GetNotificationsForRecipient(string recipientId)
    {
        var notifications = await _service.GetNotificationsForRecipientAsync(recipientId);
        return Ok(notifications);
    }

    [HttpPost]
    public async Task<IActionResult> SendNotification([FromBody] Notification notification)
    {
        await _service.SendNotificationAsync(notification);
        return Ok("Notification sent.");
    }

    [HttpPut("{id}/read")]
    public async Task<IActionResult> MarkAsRead(string id)
    {
        await _service.MarkNotificationAsReadAsync(id);
        return NoContent();
    }
}


public class NotificationRequest
{
    public string ClientId { get; set; }
    public string Message { get; set; }
}
