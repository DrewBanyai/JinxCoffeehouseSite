using Microsoft.AspNetCore.Mvc;

using REST_API.Services;
using REST_API.Models;
using System.Text.Json.Nodes;

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
    public async Task<ServiceResponse<List<MenuItem>>> Post([FromBody]MenuPostBody? data)
    {
        MenuItem.MenuItemType menuItemType = MenuItem.MenuItemType.UNKNOWN;
        var itemTypeValue = data?.MenuItemType.ToUpper();
        if (itemTypeValue != null) {
            switch (itemTypeValue) {
                case null:
                    break;
                case "DRINK":
                    menuItemType = MenuItem.MenuItemType.DRINK;
                    break;
                case "FOOD":
                    menuItemType = MenuItem.MenuItemType.FOOD;
                    break;
                case "OTHER":
                    menuItemType = MenuItem.MenuItemType.OTHER;
                    break;
            };
        }

        var menuList = await _menuService.GetAsync();
        if (menuItemType != MenuItem.MenuItemType.UNKNOWN) menuList = menuList.Where(item => item.Type == menuItemType).ToList();
        return new ServiceResponse<List<MenuItem>>() {
            Data = menuList,
            Success = true,
            Message = "Success"
        };
    }


    //  READ
    [HttpGet(Name = "Menu Get")]
    public async Task<ServiceResponse<List<MenuItem>>> Get()
    {
        var menuList = await _menuService.GetAsync();
        return new ServiceResponse<List<MenuItem>>() {
            Data = menuList,
            Success = true,
            Message = "Success"
        };
    }


    //  UPDATE
    [HttpPut(Name = "Menu Put")]
    public ServiceResponse<string> Put()
    {
        return new ServiceResponse<string>() {
            Data = null,
            Success = true,
            Message = "This route does not have a PUT request method, only POST. Try again."
        };
    }


    //  DELETE
    [HttpDelete(Name = "Menu Delete")]
    public ServiceResponse<string> Delete()
    {
        return new ServiceResponse<string>() {
            Data = null,
            Success = true,
            Message = "This route does not have a DELETE request method, only POST. Try again."
        };
    }
}
