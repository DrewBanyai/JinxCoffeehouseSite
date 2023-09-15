using Microsoft.AspNetCore.Mvc;

using REST_API.Services;
using REST_API.Models;
using System.Text.Json.Nodes;
using REST_API.DTOs;
using Microsoft.AspNetCore.Authentication;

namespace REST_API.Controllers;

[ApiController]
[Route("[controller]")]
public class MenuController : ControllerBase
{
    
    private readonly MenuItemService _menuService;
    private readonly ILogger<MenuController> _logger;

    public MenuController(MenuItemService menuService, ILogger<MenuController> logger)
    {
        _menuService = menuService;
        _logger = logger;
    }


    //  CREATE
    [HttpPost(Name = "Menu Post")]
    public async Task<ServiceResponse<List<MenuItemDTO>>> Post([FromBody]MenuPostBody? data)
    {
        MenuItemType menuItemType = MenuItemType.UNKNOWN;
        var itemTypeValue = data?.MenuItemType.ToUpper();
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

        var menuList = await _menuService.GetAsync();
        if (menuItemType != MenuItemType.UNKNOWN) menuList = menuList.Where(item => item.Type == menuItemType).ToList();
        return new ServiceResponse<List<MenuItemDTO>>() {
            Data = menuList,
            Success = true,
            Message = "Success"
        };
    }


    //  READ
    [HttpGet(Name = "Menu Get")]
    public async Task<ServiceResponse<List<MenuItemDTO>>> Get()
    {
        var menuList = await _menuService.GetAsync();
        return new ServiceResponse<List<MenuItemDTO>>() {
            Data = menuList,
            Success = true,
            Message = "Success"
        };
    }


    //  UPDATE
    [HttpPut(Name = "Menu Put")]
    public async Task<ServiceResponse<List<MenuItemDTO>>> Put(MenuItemDTO? updateItem)
    {
        if (updateItem?.Id != null)
        {
            var entry = await _menuService.GetAsync(updateItem.Id);
            if (entry == null)
                return new ServiceResponse<List<MenuItemDTO>>() { Data = null, Success = false, Message = "No Menu Item with the given ID: " + updateItem.Id };

            await _menuService.UpdateAsync(updateItem.Id, updateItem);
        }

        var menuList = await _menuService.GetAsync();
        return new ServiceResponse<List<MenuItemDTO>>() {
            Data = menuList,
            Success = true,
            Message = "Success"
        };
    }


    //  DELETE
    [HttpDelete(Name = "Menu Delete")]
    public async Task<ServiceResponse<string>> Delete(string itemId)
    {
        var entry = await _menuService.GetAsync(itemId);
        if (entry == null)
            return new ServiceResponse<string>() { Data = null, Success = false, Message = "No Menu Item with the given ID: " + itemId };

        await _menuService.RemoveAsync(itemId);

        return new ServiceResponse<string>() {
            Data = null,
            Success = true,
            Message = "Menu Item Deleted"
        };
    }
}
