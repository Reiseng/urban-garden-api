namespace UrbanGarden.Api.Infrastructure.MQTT.Services
{
    public interface IMqttService
    {
        Task Connect(CancellationToken cancellationToken);
        Task Disconnect(CancellationToken cancellationToken);

        Task Publish(string topic, string payload);

        Task Subscribe(string topic);

        event Func<string, string, Task> OnMessageReceived;
    }
}