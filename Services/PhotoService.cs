using MongoDB.Bson;
using MongoDB.Driver.GridFS;
using ReportEase.api.Models;
using ReportEase.api.Repositories;

namespace ReportEase.api.Services;

public class PhotoService
{
    private readonly PhotoRepository _photoRepository;
    private readonly GridFSBucket _gridFSBucket;

    public PhotoService(PhotoRepository photoRepository, MongoDbContext context)
    {
        _photoRepository = photoRepository;
        _gridFSBucket = context.GetGridFSBucket();
    }

    public async Task<ObjectId> UploadPhotoAsync(Stream fileStream, string fileName, string contentType, ObjectId? foodWasteReportId, ObjectId? discrepancyId)
    {
        var photoId = await _gridFSBucket.UploadFromStreamAsync(fileName, fileStream);

        var photo = new Photo
        {
            Id = photoId,
            Filename = fileName,
            ContentType = contentType,
            AssociatedFoodWasteReportId = foodWasteReportId,
            AssociatedDiscrepancyId = discrepancyId,
            UploadedAt = DateTime.UtcNow
        };

        await _photoRepository.UploadPhotoAsync(photo);

        return photoId;
    }

    public async Task<List<Photo>> GetPhotosByFoodWasteReportIdAsync(ObjectId foodWasteReportId)
    {
        return await _photoRepository.GetPhotosByFoodWasteReportIdAsync(foodWasteReportId);
    }

    public async Task<List<Photo>> GetPhotosByDiscrepancyIdAsync(ObjectId discrepancyId)
    {
        return await _photoRepository.GetPhotosByDiscrepancyIdAsync(discrepancyId);
    }

    /*public async Task<Stream> GetPhotoStreamByIdAsync(ObjectId photoId)
    {
        var stream = new MemoryStream();
        await _gridFSBucket.DownloadToStreamByIdAsync(photoId, stream);
        stream.Seek(0, SeekOrigin.Begin);
        return stream;
    }*/

    public async Task DeletePhotoAsync(ObjectId photoId)
    {
        await _gridFSBucket.DeleteAsync(photoId);
        await _photoRepository.DeletePhotoAsync(photoId);
    }
}