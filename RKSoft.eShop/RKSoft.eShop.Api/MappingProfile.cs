using AutoMapper;
using RKSoft.eShop.App.DTOs;
using RKSoft.eShop.Domain.Entities;

namespace RKSoft.eShop.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<EStore, StoreDTO>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}