using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using ReportEase.api.Models;
using ReportEase.api.Repositories;

namespace ReportEase.api.Services;

public class PhotoService
{
    private readonly PhotoRepository _photoRepository;
    private readonly GridFSBucket _gridFSBucket;
    private readonly IMongoDatabase _database;

    public PhotoService(IConfiguration configuration, PhotoRepository photoRepository, MongoDbContext context)
   
    {
        var mongoClient = new MongoClient(configuration["MongoDB:ConnectionString"]);
        _database = mongoClient.GetDatabase(configuration["MongoDB:DatabaseName"]);
        _photoRepository = photoRepository;
        _gridFSBucket = new GridFSBucket(_database);

    }
    
    public async Task<ObjectId> UploadFileAsync(Stream fileStream, string fileName, string contentType)
    {
        var options = new GridFSUploadOptions
        {
            Metadata = new BsonDocument
            {
                { "contentType", contentType }
            }
        };

        ObjectId fileId;
        try
        {
            // Upload file to GridFS
            fileId = await _gridFSBucket.UploadFromStreamAsync(fileName, fileStream, options);
        }
        catch (Exception ex)
        {
            // Handle errors here
            throw new Exception("Error while uploading file to GridFS: " + ex.Message);
        }

        return fileId;
    }

    public async Task<List<Photo>> GetPhotosByFoodWasteReportIdAsync(ObjectId foodWasteReportId)
    {
        return await _photoRepository.GetPhotosByFoodWasteReportIdAsync(foodWasteReportId);
    }

    public async Task<List<Photo>> GetPhotosByDiscrepancyIdAsync(ObjectId discrepancyId)
    {
        return await _photoRepository.GetPhotosByDiscrepancyIdAsync(discrepancyId);
    }
    
    public async Task<GridFSFileInfo> GetFileInfoAsync(ObjectId fileId)
    {
        try
        {
            var filter = Builders<GridFSFileInfo>.Filter.Eq("_id", fileId);
            var fileInfoCursor = await _gridFSBucket.FindAsync(filter);
            var fileInfo = await fileInfoCursor.FirstOrDefaultAsync();

            if (fileInfo == null)
            {
                throw new Exception("File not found in GridFS.");
            }

            return fileInfo;
        }
        catch (Exception ex)
        {
            throw new Exception("Error while fetching file metadata: " + ex.Message);
        }
    }
    public async Task<Stream> GetPhotoStreamByIdAsync(ObjectId photoId)
    {
        var stream = new MemoryStream();
        
        await _gridFSBucket.DownloadToStreamAsync(photoId, stream);
        stream.Seek(0, SeekOrigin.Begin);
        return stream;
    }

    public async Task DeletePhotoAsync(ObjectId photoId)
    {
        await _gridFSBucket.DeleteAsync(photoId);
        await _photoRepository.DeletePhotoAsync(photoId);
    }
}