using Microsoft.AspNetCore.Mvc;

using MenuService.Services;
using MenuService.Models;
using MenuService.DTOs;
using MongoDB.Bson;

namespace MenuService.Controllers;

[ApiController]
[Route("[controller]")]
public class MenuImagesController : ControllerBase
{
    
    private readonly MenuImagesService _menuImagesService;
    private readonly ILogger<MenuImagesController> _logger;

    public MenuImagesController(MenuImagesService menuService, ILogger<MenuImagesController> logger)
    {
        _menuImagesService = menuService;
        _logger = logger;
    }


    //  CREATE
    [HttpPost(Name = "Menu Images Post")]
    public async Task<ServiceResponse<List<MenuImageDTO>>> Post([FromBody]MenuImageDTO newEntry)
    {
        //  Ensure we are not specifying an entry ID, as we should only be specifying a name and the image data
        if (newEntry.Id != null && newEntry.Id != BsonObjectId.Empty)
        {
            Console.Write("Attempting to create a new collection entry, but specifying an ID: ");
            Console.WriteLine(newEntry.Id);
            Console.WriteLine("Cancelling...");
            return new ServiceResponse<List<MenuImageDTO>>() { Data = null, Success = false, Message = "Please do not specify an entry ID when attempting to add a collection entry. Leave the ID blank, and an ID will be assigned." };
        }

        //  Ensure we aren't creating a collection entry with a name that is already taken by another entry
        var existingItem = await _menuImagesService.GetAsyncByName(newEntry.Name);
        if (existingItem != null) {
            Console.Write("Attempting to create a new Menu Image, but specifying a Name already taken by an existing image: ");
            Console.WriteLine(newEntry.Name);
            Console.WriteLine("Cancelling...");
            return new ServiceResponse<List<MenuImageDTO>>() { Data = null, Success = false, Message = "Please choose a unique name for this collection entry." };
        }

        //  Create the new collection entry ID, then push the entire entry into the database
        newEntry.Id = ObjectId.GenerateNewId().ToString();
        await _menuImagesService.CreateAsync(newEntry);

        //  Grab the updated collection and return it as a success
        var collectionEntries = await _menuImagesService.GetAsync();
        return new ServiceResponse<List<MenuImageDTO>>() { Data = collectionEntries, Success = true, Message = "Success" };
    }


    //  READ
    [HttpGet(Name = "Menu Images Get")]
    public async Task<ServiceResponse<List<MenuImageDTO>>> Get()
    {
        //  grab the entire collection and return it as a success
        var imagesList = await _menuImagesService.GetAsync();
        return new ServiceResponse<List<MenuImageDTO>>() { Data = imagesList, Success = true, Message = "Success" };
    }


    //  UPDATE
    [HttpPut(Name = "Menu Images Put")]
    public async Task<ServiceResponse<List<MenuImageDTO>>> Put(MenuImageDTO updateEntry)
    {
        //  If the ID is null or invalid, return an error.
        if (updateEntry.Id == null || updateEntry.Id == BsonObjectId.Empty)
            return new ServiceResponse<List<MenuImageDTO>>() { Data = null, Success = false,Message = "You must specify a valid ID to update a collection entry." };

        //  If the ID is not tied to an actual entry, return an error
        var existingEntry = await _menuImagesService.GetAsyncById(updateEntry.Id);
        if (existingEntry == null)
            return new ServiceResponse<List<MenuImageDTO>>() { Data = null, Success = false, Message = "No Menu Image with the given ID: " + updateEntry.Id };

        //  Update the existing entry
        await _menuImagesService.UpdateAsync(updateEntry.Id, updateEntry);

        //  Grab the updated collection and return it as a success
        var menuList = await _menuImagesService.GetAsync();
        return new ServiceResponse<List<MenuImageDTO>>() { Data = menuList, Success = true, Message = "Success" };
    }


    //  DELETE
    [HttpDelete(Name = "Menu Images Delete")]
    public async Task<ServiceResponse<string>> Delete(string entryId)
    {
        //  If the ID is not tied to an actual entry, return an error
        var entry = await _menuImagesService.GetAsyncById(entryId);
        if (entry == null)
            return new ServiceResponse<string>() { Data = null, Success = false, Message = "No Menu Image with the given ID: " + entryId };

        //  Remove the existing entry
        await _menuImagesService.RemoveAsync(entryId);

        //  Return a success message
        return new ServiceResponse<string>() { Data = null, Success = true, Message = "Entry Deleted" };
    }
}
