using System.Text.Json;
using UrbanGarden.Api.Infrastructure.MQTT.Dtos;
using UrbanGarden.Api.Infrastructure.MQTT.Services;

public class MqttHostedService : BackgroundService
{
    private readonly IMqttService _mqttService;

    public MqttHostedService(IMqttService mqttService)
    {
        _mqttService = mqttService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _mqttService.OnMessageReceived += async (topic, payload) =>
        {
            var data = JsonSerializer.Deserialize<SoilSensorDto>(payload); 
            var sensorId = topic.Split('/').Last();

            Console.WriteLine($"{topic}: Sensor: {sensorId} Humedad: {data?.Humidity} Fecha: {data?.Timestamp}");
            await Task.CompletedTask;
        };

        await _mqttService.Connect(stoppingToken);

        await _mqttService.Subscribe("sensors/soil/#");

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await _mqttService.Disconnect(cancellationToken);
        await base.StopAsync(cancellationToken);
    }
}