using WareHouse.OrderService.Application.Models;
using WareHouse.OrderService.Domain.Enums;

namespace WareHouse.OrderService.Application.Contracts.Strategy
{
    public interface IChangeOrderStatusStrategy
    {
        bool IsApplicable(OrderStatus status);
        Task<Order> ChangeOrderStatus(string orderId, CancellationToken cancellationToken);
    }
}
