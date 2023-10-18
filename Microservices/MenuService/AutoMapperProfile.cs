using AutoMapper;
using MenuService.DTOs;
using MenuService.Models;

namespace MenuService.Profiles;

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