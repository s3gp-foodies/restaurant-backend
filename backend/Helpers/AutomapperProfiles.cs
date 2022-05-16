using AutoMapper;
using foodies_app.DTOs;
using foodies_app.Entities;

namespace foodies_app.Helpers;

public class AutomapperProfiles : Profile
{
    public AutomapperProfiles()
    {
        CreateMap<AppUser, UserDto>();
        CreateMap<OrderItem, OrderItemDto>();
        CreateMap<OrderItem, OrderNewDto>();
        
        CreateMap<Order, OrderDto>();
        CreateMap<Order, OrderSubmissionDto>();
        CreateMap<OrderItem, OrderItemSubmissionDto>();
        
        CreateMap<MenuItem, MenuItemDto>();
        CreateMap<Allergy, AllergyDto>();
        CreateMap<Category, CategoryDto>().ReverseMap();
    }
}