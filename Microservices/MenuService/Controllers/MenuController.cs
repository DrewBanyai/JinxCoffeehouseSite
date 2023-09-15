using Microsoft.AspNetCore.Mvc;

using REST_API.Services;
using REST_API.Models;
using System.Text.Json.Nodes;
using REST_API.DTOs;

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
