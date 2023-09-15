using Microsoft.AspNetCore.Mvc;
using REST_API.Services;
using REST_API.Models;
using System.Text.Json.Nodes;

namespace REST_API.Controllers;

[ApiController]
[Route("[controller]")]
public class MenuController : ControllerBase
{
    
    private readonly MenuService _menuService;
    private readonly ILogger<MenuController> _logger;

    public MenuController(MenuService menuService, ILogger<MenuController> logger)
    {
        _menuService = menuService;
        _logger = logger;
    }


    //  CREATE
    [HttpPost(Name = "Menu Post")]
    public async Task<List<MenuItemModel>> Post([FromBody]JsonObject? data)
    {
        MenuItemModel.MenuItemType menuItemType = MenuItemModel.MenuItemType.UNKNOWN;
        var itemTypeValue = data?["MenuItemType"];
        if (itemTypeValue != null) {
            switch (itemTypeValue.GetValue<string>().ToUpper()) {
                case null:
                    break;
                case "DRINK":
                    menuItemType = MenuItemModel.MenuItemType.DRINK;
                    break;
                case "FOOD":
                    menuItemType = MenuItemModel.MenuItemType.FOOD;
                    break;
                case "OTHER":
                    menuItemType = MenuItemModel.MenuItemType.OTHER;
                    break;
            };
        }

        var menuList = await _menuService.GetAsync();
        if (menuItemType != MenuItemModel.MenuItemType.UNKNOWN) menuList = menuList.Where(item => item.Type == menuItemType).ToList();
        return menuList;
    }


    //  READ
    [HttpGet(Name = "Menu Get")]
    public async Task<List<MenuItemModel>> Get()
    {
        var menuList = await _menuService.GetAsync();
        return menuList;
    }


    //  UPDATE
    [HttpPut(Name = "Menu Put")]
    public string Put()
    {
        return "This route does not have a PUT request method, only POST. Try again.";
    }


    //  DELETE
    [HttpDelete(Name = "Menu Delete")]
    public string Delete()
    {
        return "This route does not have a DELETE request method, only POST. Try again.";
    }
}
