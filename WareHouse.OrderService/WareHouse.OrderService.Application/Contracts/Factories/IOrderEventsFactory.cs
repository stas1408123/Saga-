using Warehouse.Contracts.DTOs;
using WareHouse.IntegrationEvents;
using WareHouse.OrderService.Application.Models;
using WareHouse.OrderService.Domain.Enums;

namespace WareHouse.OrderService.Application.Contracts.Factories
{
    public interface IOrderEventsFactory
    {
        IntegrationEvent<OrderDTO> CreateIntegrationEvent(OrderStatus status, Order orderDto);
    }
}
