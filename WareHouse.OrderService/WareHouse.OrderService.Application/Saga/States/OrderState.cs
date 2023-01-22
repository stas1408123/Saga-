using MassTransit;

namespace WareHouse.OrderService.Application.Saga.States
{
    public class OrderState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public int CurrentState { get; set; }
    }
}
