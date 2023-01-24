using AutoMapper;
using Warehouse.Contracts.DTOs;
using WareHouse.OrderService.Application.Models;

namespace WareHouse.OrderService.Application.Mapper
{
    public class ModelDTOProfile : Profile
    {
        public ModelDTOProfile()
        {
            CreateMap<Order, OrderDTO>().ReverseMap();
        }
    }
}
