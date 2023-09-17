using AutoMapper;
using REST_API.DTOs;
using REST_API.Models;

namespace REST_API.Profiles;

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