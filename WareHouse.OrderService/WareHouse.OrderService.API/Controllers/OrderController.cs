using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WareHouse.OrderService.API.ViewModels;
using WareHouse.OrderService.Application.Contracts.Services;
using WareHouse.OrderService.Application.Models;
using WareHouse.OrderService.Domain.Enums;

namespace WareHouse.OrderService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly IValidator<PostOrderViewModel> _postOrderViewModel;
        private readonly IValidator<ChangeOrderStatusViewModel> _changeStatusValidator;

        public OrderController(IOrderService orderService,
            IMapper mapper,
            IValidator<PostOrderViewModel> postOrderViewModel,
            IValidator<ChangeOrderStatusViewModel> changeStatusValidator)
        {
            ArgumentNullException.ThrowIfNull(orderService);
            ArgumentNullException.ThrowIfNull(mapper);
            ArgumentNullException.ThrowIfNull(postOrderViewModel);
            ArgumentNullException.ThrowIfNull(changeStatusValidator);

            _orderService = orderService;
            _mapper = mapper;
            _postOrderViewModel = postOrderViewModel;
            _changeStatusValidator = changeStatusValidator;
        }

        [HttpPost]
        public async Task<OrderViewModel> MakeOrder(PostOrderViewModel viewModel, CancellationToken cancellationToken)
        {
            await _postOrderViewModel.ValidateAndThrowAsync(viewModel, cancellationToken);

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
            await _changeStatusValidator.ValidateAndThrowAsync(viewModel, cancellationToken);

            var order = await _orderService.ChangeStatus(id, (OrderStatus)viewModel.Status, cancellationToken);
            var result = _mapper.Map<OrderViewModel>(order);

            return result;
        }

        [HttpDelete]
        public async Task<int> DeleteFailedOrders(CancellationToken cancellationToken)
        {
            var result = await _orderService.DeleteFailedOrders(cancellationToken);

            return result;
        }
    }
}
