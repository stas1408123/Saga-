namespace WareHouse.IntegrationEvents
{
    public record IntegrationEvent<TPayload>(TPayload Payload) where TPayload : class;
}
