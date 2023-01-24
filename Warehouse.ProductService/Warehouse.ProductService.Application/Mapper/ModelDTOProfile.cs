using AutoMapper;
using Warehouse.Contracts.DTOs;
using Warehouse.ProductService.Application.Models;

namespace Warehouse.ProductService.Application.Mapper
{
    public class ModelDTOProfile : Profile
    {
        public ModelDTOProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}
