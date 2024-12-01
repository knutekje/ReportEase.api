using MongoDB.Driver;
using ReportEase.api.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

public class NotificationRepository
{
    private readonly IMongoCollection<Notification> _notifications;

    public NotificationRepository(MongoDbContext context)
    {
        _notifications = context.GetCollection<Notification>("Notifications");
    }

    public async Task<List<Notification>> GetAllAsync()
    {
        return await _notifications.Find(_ => true).ToListAsync();
    }

    public async Task<List<Notification>> GetByRecipientAsync(string recipientId)
    {
        return await _notifications.Find(n => n.RecipientId == recipientId).ToListAsync();
    }

    public async Task<Notification> GetByIdAsync(string id)
    {
        return await _notifications.Find(n => n.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Notification notification)
    {
        await _notifications.InsertOneAsync(notification);
    }

    public async Task MarkAsReadAsync(string id)
    {
        var filter = Builders<Notification>.Filter.Eq(n => n.Id, id);
        var update = Builders<Notification>.Update.Set(n => n.IsRead, true);
        await _notifications.UpdateOneAsync(filter, update);
    }
}
