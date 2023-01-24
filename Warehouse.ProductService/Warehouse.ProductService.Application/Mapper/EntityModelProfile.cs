using AutoMapper;
using Warehouse.ProductService.Application.Models;
using WarehouseService.Domain.Entities;

namespace Warehouse.ProductService.Application.Mapper
{
    public class EntityModelProfile : Profile
    {
        public EntityModelProfile()
        {
            CreateMap<ProductEntity, Product>().ReverseMap();
            CreateMap<CategoryEntity, Category>().ReverseMap();
        }
    }
}
