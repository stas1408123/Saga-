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

            CreateMap<OrderDTO, ProductDTO>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.OrderId, opt => opt.MapFrom(x => x.Id))
                .ReverseMap();
        }
    }
}
