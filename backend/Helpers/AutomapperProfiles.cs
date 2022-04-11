using AutoMapper;
using foodies_app.DTOs;
using foodies_app.Entities;

namespace foodies_app.Helpers;

public class AutomapperProfiles : Profile
{
    public AutomapperProfiles()
    {
        CreateMap<AppUser, UserDto>();
        CreateMap<OrderItem, GetOrderItemDto>();
    }
}