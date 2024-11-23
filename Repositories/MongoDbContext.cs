namespace ReportEase.api.Repositories;

using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;
using Microsoft.Extensions.Options;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;
    // Constructor to initialize MongoDB connection
    public MongoDbContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration["MongoDB:ConnectionString"]);
        _database = client.GetDatabase(configuration["MongoDB:DatabaseName"]);
    }

   
    public IMongoCollection<T> GetCollection<T>(string name)
    {
        return _database.GetCollection<T>(name);
    }

    // Get GridFS bucket for file storage
    public GridFSBucket GetGridFSBucket()
    {
        return new GridFSBucket(_database, new GridFSBucketOptions
        {
            BucketName = "uploads",
            ChunkSizeBytes = 255 * 1024, // Optional: 255 KB default chunk size
            WriteConcern = WriteConcern.WMajority,
            ReadPreference = ReadPreference.Primary
        });
    }
}
