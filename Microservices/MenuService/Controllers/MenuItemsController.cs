using Microsoft.AspNetCore.Mvc;

using MenuService.Services;
using MenuService.Models;
using MenuService.DTOs;
using MongoDB.Bson;

namespace MenuService.Controllers;

[ApiController]
[Route("[controller]")]
public class MenuItemsController : ControllerBase
{
    
    private readonly MenuItemsService _menuItemsService;
    private readonly ILogger<MenuItemsController> _logger;

    public MenuItemsController(MenuItemsService menuService, ILogger<MenuItemsController> logger)
    {
        _menuItemsService = menuService;
        _logger = logger;
    }


    //  CREATE
    [HttpPost(Name = "Menu Items Post")]
    public async Task<ServiceResponse<List<MenuItemDTO>>> Post([FromBody]MenuItemDTO newEntry)
    {
        //  Ensure we are not specifying an entry ID, as we should only be specifying a name and the image data
        if (newEntry.Id != null && newEntry.Id != BsonObjectId.Empty)
        {
            Console.Write("Attempting to create a new collection entry, but specifying an ID: ");
            Console.WriteLine(newEntry.Id);
            Console.WriteLine("Cancelling...");
            return new ServiceResponse<List<MenuItemDTO>>() { Data = null, Success = false, Message = "Please do not specify an entry ID when attempting to add a collection entry. Leave the ID blank, and an ID will be assigned." };
        }

        //  Ensure we aren't creating a collection entry with a name that is already taken by another entry
        var existingItem = await _menuItemsService.GetAsyncByName(newEntry.Name);
        if (existingItem != null) {
            Console.Write("Attempting to create a new Menu Image, but specifying a Name already taken by an existing image: ");
            Console.WriteLine(newEntry.Name);
            Console.WriteLine("Cancelling...");
            return new ServiceResponse<List<MenuItemDTO>>() { Data = null, Success = false, Message = "Please choose a unique name for this collection entry." };
        }

        //  Create the new collection entry ID, then push the entire entry into the database
        newEntry.Id = ObjectId.GenerateNewId().ToString();
        await _menuItemsService.CreateAsync(newEntry);

        //  Grab the updated collection and return it as a success
        var collectionEntries = await _menuItemsService.GetAsync();
        return new ServiceResponse<List<MenuItemDTO>>() { Data = collectionEntries, Success = true, Message = "Success" };
    }


    //  READ
    [HttpGet(Name = "Menu Items Get")]
    public async Task<ServiceResponse<List<MenuItemDTO>>> Get(string? itemType)
    {
        MenuItemType menuItemType = MenuItemType.UNKNOWN;
        var itemTypeValue = itemType?.ToUpper();
        if (itemTypeValue != null) {
            switch (itemTypeValue) {
                case null:
                    break;
                case "DRINK":
                    menuItemType = MenuItemType.DRINK;
                    break;
                case "FOOD":
                    menuItemType = MenuItemType.FOOD;
                    break;
                case "OTHER":
                    menuItemType = MenuItemType.OTHER;
                    break;
            };
        }

        var itemsList = await _menuItemsService.GetAsync();
        if (menuItemType != MenuItemType.UNKNOWN) itemsList = itemsList.Where(item => item.Type == menuItemType).ToList();
        return new ServiceResponse<List<MenuItemDTO>>() {
            Data = itemsList,
            Success = true,
            Message = "Success"
        };
    }


    //  UPDATE
    [HttpPut(Name = "Menu Items Put")]
    public async Task<ServiceResponse<List<MenuItemDTO>>> Put(MenuItemDTO updateEntry)
    {
        if (updateEntry.Id != null)
        {
            var entry = await _menuItemsService.GetAsyncById(updateEntry.Id);
            if (entry == null)
                return new ServiceResponse<List<MenuItemDTO>>() { Data = null, Success = false, Message = "No Menu Item with the given ID: " + updateEntry.Id };

            await _menuItemsService.UpdateAsync(updateEntry.Id, updateEntry);
        }

        var menuList = await _menuItemsService.GetAsync();
        return new ServiceResponse<List<MenuItemDTO>>() {
            Data = menuList,
            Success = true,
            Message = "Success"
        };
    }


    //  DELETE
    [HttpDelete(Name = "Menu Items Delete")]
    public async Task<ServiceResponse<string>> Delete(string? itemId)
    {
        var entry = await _menuItemsService.GetAsyncById(itemId);
        if (entry == null)
            return new ServiceResponse<string>() { Data = null, Success = false, Message = "No Menu Item with the given ID: " + itemId };

        await _menuItemsService.RemoveAsync(itemId);

        return new ServiceResponse<string>() {
            Data = null,
            Success = true,
            Message = "Menu Item Deleted"
        };
    }
}
