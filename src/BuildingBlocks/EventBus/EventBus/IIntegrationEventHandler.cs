namespace EventBus;

public interface IIntegrationEventHandler<in T>
{
    public Task Handle(T @event);
}