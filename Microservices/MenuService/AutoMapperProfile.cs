using AutoMapper;
using JinxMenuService.DTOs;
using JinxMenuService.Models;

namespace JinxMenuService.Profiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<MenuItem, MenuItemDTO>();
        CreateMap<MenuItemDTO, MenuItem>();
        CreateMap<MenuImage, MenuImageDTO>();
        CreateMap<MenuImageDTO, MenuImage>();
    }
}