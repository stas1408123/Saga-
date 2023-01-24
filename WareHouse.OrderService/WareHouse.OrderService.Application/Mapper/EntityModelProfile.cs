using AutoMapper;
using WareHouse.OrderService.Application.Models;
using WareHouse.OrderService.Domain.Entities;

namespace WareHouse.OrderService.Application.Mapper
{
    public class EntityModelProfile : Profile
    {
        public EntityModelProfile()
        {
            CreateMap<OrderEntity, Order>().ReverseMap();
        }
    }
}
