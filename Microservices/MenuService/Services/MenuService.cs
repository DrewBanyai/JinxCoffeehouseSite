using REST_API.Models;
using REST_API.DTOs;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using AutoMapper;
using Microsoft.VisualBasic;

namespace REST_API.Services;

public class MenuItemService
{
    private readonly IMongoCollection<MenuItem> _menuItemCollection;
    private readonly IMapper _mapper;

    public MenuItemService(IOptions<DatabaseSettings> databaseSettings, IMapper mapper)
    {
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

        _menuItemCollection = mongoDatabase.GetCollection<MenuItem>(databaseSettings.Value.MenuCollectionName);

        _mapper = mapper;
    }

    public async Task<List<MenuItemDTO>> GetAsync() =>
        _mapper.Map<List<MenuItemDTO>>(await _menuItemCollection.Find(_ => true).ToListAsync());

    public async Task<MenuItemDTO?> GetAsync(string id) =>
        _mapper.Map<MenuItemDTO>(await _menuItemCollection.Find(x => x.Id == id).FirstOrDefaultAsync());

    public async Task CreateAsync(MenuItemDTO newItem) {
        var newMenuItem = _mapper.Map<MenuItem>(newItem);
        await _menuItemCollection.InsertOneAsync(newMenuItem);
    }
    

    public async Task UpdateAsync(string id, MenuItemDTO updatedItem) =>
        await _menuItemCollection.ReplaceOneAsync(x => x.Id == id, _mapper.Map<MenuItem>(updatedItem));

    public async Task RemoveAsync(string id) =>
        await _menuItemCollection.DeleteOneAsync(x => x.Id == id);
}