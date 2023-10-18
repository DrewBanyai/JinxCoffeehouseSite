using MenuService.Models;
using MenuService.DTOs;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using AutoMapper;

namespace MenuService.Services;

public class MenuItemsService
{
    private readonly IMongoCollection<MenuItem> _menuItemCollection;
    private readonly IMapper _mapper;

    public MenuItemsService(IOptions<DatabaseSettings> databaseSettings, IMapper mapper)
    {
        if (MongoDatabaseHolder.mongoClient == null)
            MongoDatabaseHolder.mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

        if (MongoDatabaseHolder.mongoDatabase == null)
            MongoDatabaseHolder.mongoDatabase = MongoDatabaseHolder.mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

        _menuItemCollection = MongoDatabaseHolder.mongoDatabase.GetCollection<MenuItem>(databaseSettings.Value.MenuItemsCollectionName);

        _mapper = mapper;
    }

    public async Task<List<MenuItemDTO>> GetAsync() =>
        _mapper.Map<List<MenuItemDTO>>(await _menuItemCollection.Find(_ => true).ToListAsync());

    public async Task<MenuItemDTO?> GetAsyncById(string? id) =>
        _mapper.Map<MenuItemDTO>(await _menuItemCollection.Find(x => x.Id == id).FirstOrDefaultAsync());

    public async Task<MenuImageDTO?> GetAsyncByName(string name) =>
        _mapper.Map<MenuImageDTO>(await _menuItemCollection.Find(x => x.Name == name).FirstOrDefaultAsync());

    public async Task CreateAsync(MenuItemDTO newItem) {
        var newInsert = _mapper.Map<MenuItem>(newItem);
        newInsert.CreatedAt = DateTime.Now;
        newInsert.LastEditedAt = DateTime.Now;
        await _menuItemCollection.InsertOneAsync(newInsert);
    }
    

    public async Task UpdateAsync(string? id, MenuItemDTO updatedItem) {
        MenuItem updateEntry = _mapper.Map<MenuItem>(updatedItem);
        updateEntry.LastEditedAt = DateTime.Now;
        await _menuItemCollection.ReplaceOneAsync(x => x.Id == id, updateEntry);
    }

    public async Task RemoveAsync(string? id) =>
        await _menuItemCollection.DeleteOneAsync(x => x.Id == id);
}