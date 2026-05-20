namespace UrbanGarden.Api.Infrastructure.MQTT.Services
{
    public interface ISensorsService
    {
        public Task ProcessSoilSensorData(string topic, string payload);
        public Task ProcessTemperatureSensorData(string topic, string payload);
    }
}