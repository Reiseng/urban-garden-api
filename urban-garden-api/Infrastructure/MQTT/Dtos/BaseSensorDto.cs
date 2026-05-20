namespace UrbanGarden.Api.Infrastructure.MQTT.Dtos
{
    public class BaseSensorDto
    {
        public string ApiKey { get; set; } = null!;

        public DateTime Timestamp { get; set; }
    }
}