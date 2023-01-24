using AutoMapper;
using WareHouse.OrderService.API.ViewModels;
using WareHouse.OrderService.Application.Models;

namespace WareHouse.OrderService.API.Mapper
{
    public class ModelViewModelProfile : Profile
    {
        public ModelViewModelProfile()
        {
            CreateMap<PostOrderViewModel, OrderDetails>();
            CreateMap<OrderDetails, OrderViewModel>();

            CreateMap<OrderDetails, Order>().ReverseMap();
            CreateMap<Order, OrderViewModel>();
        }
    }
}
