using REST_API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace REST_API.Services;

public class MenuService
{
    private readonly IMongoCollection<MenuItemModel> _menuItemCollection;

    public MenuService(IOptions<DatabaseSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

        _menuItemCollection = mongoDatabase.GetCollection<MenuItemModel>(databaseSettings.Value.MenuCollectionName);
    }

    public async Task<List<MenuItemModel>> GetAsync() =>
        await _menuItemCollection.Find(_ => true).ToListAsync();

    public async Task<MenuItemModel?> GetAsync(string id) =>
        await _menuItemCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(MenuItemModel newItem) =>
        await _menuItemCollection.InsertOneAsync(newItem);

    public async Task UpdateAsync(string id, MenuItemModel updatedItem) =>
        await _menuItemCollection.ReplaceOneAsync(x => x.Id == id, updatedItem);

    public async Task RemoveAsync(string id) =>
        await _menuItemCollection.DeleteOneAsync(x => x.Id == id);
}