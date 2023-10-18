using MenuService.Models;
using MenuService.DTOs;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using AutoMapper;

namespace MenuService.Services;

public class MenuImagesService
{
    private readonly IMongoCollection<MenuImage> _menuImagesCollection;
    private readonly IMapper _mapper;

    public MenuImagesService(IOptions<DatabaseSettings> databaseSettings, IMapper mapper)
    {
        if (MongoDatabaseHolder.mongoClient == null)
            MongoDatabaseHolder.mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

        if (MongoDatabaseHolder.mongoDatabase == null)
            MongoDatabaseHolder.mongoDatabase = MongoDatabaseHolder.mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

        _menuImagesCollection = MongoDatabaseHolder.mongoDatabase.GetCollection<MenuImage>(databaseSettings.Value.MenuImagesCollectionName);

        _mapper = mapper;
    }

    public async Task<List<MenuImageDTO>> GetAsync() =>
        _mapper.Map<List<MenuImageDTO>>(await _menuImagesCollection.Find(_ => true).ToListAsync());

    public async Task<MenuImageDTO?> GetAsyncById(string? id) =>
        _mapper.Map<MenuImageDTO>(await _menuImagesCollection.Find(x => x.Id == id).FirstOrDefaultAsync());

    public async Task<MenuImageDTO?> GetAsyncByName(string name) =>
        _mapper.Map<MenuImageDTO>(await _menuImagesCollection.Find(x => x.Name == name).FirstOrDefaultAsync());

    public async Task CreateAsync(MenuImageDTO newImage) {
        var newInsert = _mapper.Map<MenuImage>(newImage);
        newInsert.CreatedAt = DateTime.Now;
        newInsert.LastEditedAt = DateTime.Now;
        await _menuImagesCollection.InsertOneAsync(newInsert);
    }
    

    public async Task UpdateAsync(string? id, MenuImageDTO updatedImage) {
        MenuImage updateEntry = _mapper.Map<MenuImage>(updatedImage);
        updateEntry.LastEditedAt = DateTime.Now;
        await _menuImagesCollection.ReplaceOneAsync(x => x.Id == id, updateEntry);
    }

    public async Task RemoveAsync(string? id) =>
        await _menuImagesCollection.DeleteOneAsync(x => x.Id == id);
}