namespace UrbanGarden.Api.Infrastructure.MQTT.Dtos
{
    public class TemperatureSensorDto
    {
        public string ApiKey { get; set; } = null!;
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public DateTime Timestamp { get; set; }
    }
}