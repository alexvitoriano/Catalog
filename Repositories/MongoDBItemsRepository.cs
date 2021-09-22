using MongoDB.Bson;
using MongoDB.Driver;

public class MongoDBItemsRepository : IInMemItemsRepository
{
    private const string databasename = "catalog";
    private const string collectionName = "items";
    private readonly IMongoCollection<Item> itemsCollection;
    private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;

    public MongoDBItemsRepository(IMongoClient mongoClient)
    {
        IMongoDatabase database = mongoClient.GetDatabase(databasename);
        itemsCollection = database.GetCollection<Item>(collectionName);
    }

    public void CreateItem(Item item)
    {
        itemsCollection.InsertOne(item);
    }

    public void DeleteItem(Guid id)
    {
        var filter = filterBuilder.Eq(existingItem => existingItem.Id, id);
        itemsCollection.DeleteOne(filter);
    }

    public IEnumerable<Item> GetItems()
    {
        return itemsCollection.Find(new BsonDocument()).ToList();
    }

    public Item GetItem(Guid id)
    {
        var filter = filterBuilder.Eq(item => item.Id, id);
        return itemsCollection.Find(filter).SingleOrDefault();
    }

    public void UpdateItem(Item item)
    {
        var filter = filterBuilder.Eq(existingItem => existingItem.Id, item.Id);
        itemsCollection.ReplaceOne(filter, item);
    }
}