using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WareHouse.OrderService.API.ViewModels;
using WareHouse.OrderService.Application.Contracts.Services;
using WareHouse.OrderService.Application.Models;

namespace WareHouse.OrderService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<OrderViewModel> MakeOrder(PostOrderViewModel viewModel, CancellationToken cancellationToken)
        {
            var orderDetails = _mapper.Map<OrderDetails>(viewModel);

            var order = await _orderService.PerformOrder(orderDetails, cancellationToken);
            var result = _mapper.Map<OrderViewModel>(order);

            return result;
        }

        [HttpGet]
        public async Task<IEnumerable<OrderViewModel>> GetAll(CancellationToken cancellationToken)
        {
            var orders = await _orderService.GetAll(cancellationToken);
            var result = _mapper.Map<IEnumerable<OrderViewModel>>(orders);

            return result;
        }

        [HttpGet("{id}")]
        public async Task<OrderViewModel> GetById(string id, CancellationToken cancellationToken)
        {
            var order = await _orderService.GetById(id, cancellationToken);
            var result = _mapper.Map<OrderViewModel>(order);

            return result;
        }

        [HttpPatch("{id}")]
        public async Task<OrderViewModel> ChangeStatus(string id, ChangeOrderStatusViewModel viewModel, CancellationToken cancellationToken)
        {
            var order = await _orderService.ChangeStatus(id, viewModel.Status, cancellationToken);
            var result = _mapper.Map<OrderViewModel>(order);

            return result;
        }
    }
}
