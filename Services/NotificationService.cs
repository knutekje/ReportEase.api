using Microsoft.AspNetCore.SignalR;

public class NotificationService
{
    private readonly NotificationRepository _repository;
    private readonly IHubContext<MessageHub> _hubContext;

    public NotificationService(NotificationRepository repository, IHubContext<MessageHub> hubContext)
    {
        _repository = repository;
        _hubContext = hubContext;
    }

    public async Task<List<Notification>> GetAllNotificationsAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<List<Notification>> GetNotificationsForRecipientAsync(string recipientId)
    {
        return await _repository.GetByRecipientAsync(recipientId);
    }

    public async Task SendNotificationAsync(Notification notification)
    {
        await _repository.CreateAsync(notification);

        if (notification.Type == NotificationType.Direct && !string.IsNullOrEmpty(notification.RecipientId))
        {
            await _hubContext.Clients.User(notification.RecipientId).SendAsync("ReceiveNotification", notification);
        }
        else
        {
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", notification);
        }
    }

    public async Task MarkNotificationAsReadAsync(string id)
    {
        await _repository.MarkAsReadAsync(id);
    }
}
