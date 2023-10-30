using AutoMapper;
using BusinessObject;
using eStoreAPI.DTOs;

namespace eStoreAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            CreateMap<AddProductDtos, Product>();
            CreateMap<Product, AddProductDtos>();
            CreateMap<Product, UpdateProductDto>();
            CreateMap<UpdateProductDto, Product>();
            CreateMap<Member, LoginDto>();
            CreateMap<LoginDto, Member>(); 
            CreateMap<Member, RegisterDto>();
            CreateMap<RegisterDto, Member>();
            CreateMap<Order, SortDto>();
            CreateMap<SortDto, Order>();
            CreateMap<AddOrderDtos, Order>();
        }

    }
}
