using MongoDB.Driver;

class MongoDatabaseHolder
{
    static public MongoClient? mongoClient {get; set;} = null;
    static public IMongoDatabase? mongoDatabase {get; set;} = null;
}