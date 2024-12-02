using Microsoft.AspNetCore.SignalR;

public class MessageHub : Hub
{
    public async Task SendMessage(string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", message);
    }

    public async Task SendNotification(string clientId, string notification)
    {
        await Clients.Client(clientId).SendAsync("ReceiveNotification", notification);
    }
    public async Task AcknowledgeNotification(string notificationId)
    {
        throw new NotImplementedException();
        
    }
}
