using MassTransit;
using WareHouse.OrderService.Application.Saga.States;

namespace WareHouse.OrderService.Application.Saga.StateMachines
{
    public class OrderStateMachine : MassTransitStateMachine<OrderState>
    {
    }
}
