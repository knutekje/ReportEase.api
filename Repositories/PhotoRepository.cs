using MongoDB.Bson;
using MongoDB.Driver;
using ReportEase.api.Models;
using ReportEase.api.Repositories;

namespace ReportEase.api.Repositories;

public class PhotoRepository
{
    private readonly IMongoCollection<Photo> _photos;

    public PhotoRepository(MongoDbContext context)
    {
        _photos = context.GetCollection<Photo>("Photos");
    }

    public async Task<Photo> UploadPhotoAsync(Photo photo)
    {
        await _photos.InsertOneAsync(photo);
        return photo;
    }

    public async Task<List<Photo>> GetPhotosByFoodWasteReportIdAsync(ObjectId foodWasteReportId)
    {
        return await _photos.Find(p => p.AssociatedFoodWasteReportId == foodWasteReportId).ToListAsync();
    }

    public async Task<List<Photo>> GetPhotosByDiscrepancyIdAsync(ObjectId discrepancyId)
    {
        return await _photos.Find(p => p.AssociatedDiscrepancyId == discrepancyId).ToListAsync();
    }

    public async Task<Photo> GetPhotoByIdAsync(ObjectId photoId)
    {
        return await _photos.Find(p => p.Id == photoId).FirstOrDefaultAsync();
    }

    public async Task DeletePhotoAsync(ObjectId photoId)
    {
        await _photos.DeleteOneAsync(p => p.Id == photoId);
    }
}