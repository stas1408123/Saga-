using MassTransit;
using WareHouse.OrderService.Application.Saga.States;

namespace WareHouse.OrderService.Application.Saga.StateMachines
{
    public class OrderStateMachine : MassTransitStateMachine<OrderState>
    {
        //public State Pending { get; private set; }
        //public State Approved { get; private set; }
        //public State InReview { get; private set; }
        //public State Declined { get; private set; }

        //public Event<ProductInStockIntegrationEvent> ProductInStock { get; private set; }
        //public Event<ProductLowStockIntegrationEvent> ProductLowStock { get; private set; }
        //public Event<ProductOutOfStockIntegrationEvent> ProductOutOfStock { get; private set; }

        //public OrderStateMachine()
        //{
        //    InstanceState(x => x.CurrentState, Pending, Approved, InReview, Declined);

        //    Event(() => ProductInStock, x => x.CorrelateById(context => context.ConversationId!.Value));
        //    Event(() => ProductLowStock, x => x.CorrelateById(context => context.ConversationId!.Value));
        //    Event(() => ProductOutOfStock, x => x.CorrelateById(context => context.ConversationId!.Value));
        //}
    }
}
