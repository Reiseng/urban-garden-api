namespace UrbanGarden.Api.Infrastructure.MQTT.Dtos
{
    public class SoilSensorDto
    {
        public string ApiKey { get; set; } = null!;
        public List<double> RawValues { get; set; } = [];
        public DateTime Timestamp { get; set; }
    }
}