namespace ReportEase.api.Repositories;

using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;
using Microsoft.Extensions.Options;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;
    public MongoDbContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration["MongoDB:ConnectionString"]);
        _database = client.GetDatabase(configuration["MongoDB:DatabaseName"]);
    }

   
    public IMongoCollection<T> GetCollection<T>(string name)
    {
        return _database.GetCollection<T>(name);
    }

    public GridFSBucket GetGridFSBucket()
    {
        return new GridFSBucket(_database);
    }
}
