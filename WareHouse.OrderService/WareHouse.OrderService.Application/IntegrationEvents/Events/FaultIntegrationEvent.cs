using Warehouse.Contracts.DTOs;

namespace WareHouse.IntegrationEvents
{
    public record FaultIntegrationEvent : IntegrationEvent<FaultDTO>
    {
        public FaultIntegrationEvent(FaultDTO Payload) : base(Payload)
        {
        }
    }
}
