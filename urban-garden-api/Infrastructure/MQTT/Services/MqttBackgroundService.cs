using UrbanGarden.Api.Infrastructure.MQTT.Services;

public class MqttHostedService : BackgroundService
{
    private readonly IMqttService _mqttService;
    private readonly ISensorsService _sensorsService;

    public MqttHostedService(IMqttService mqttService, ISensorsService sensorsService)
    {
        _mqttService = mqttService;
        _sensorsService = sensorsService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _mqttService.OnMessageReceived += async (topic, payload) =>
        {
            if (topic.EndsWith("/sensors/soil"))
            {
                await _sensorsService.ProcessSoilSensorData(topic, payload);
                return;
            }

            if (topic.EndsWith("/sensors/temperature"))
            {
                await _sensorsService.ProcessTemperatureSensorData(topic, payload);
                return;
            }
            await Task.CompletedTask;
        };

        await _mqttService.Connect(stoppingToken);

        await _mqttService.Subscribe("devices/+/sensors/soil");
        await _mqttService.Subscribe("devices/+/sensors/temperature");

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await _mqttService.Disconnect(cancellationToken);
        await base.StopAsync(cancellationToken);
    }
}