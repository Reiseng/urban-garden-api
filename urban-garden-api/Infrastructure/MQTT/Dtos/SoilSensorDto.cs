namespace UrbanGarden.Api.Infrastructure.MQTT.Dtos
{
    public class SoilSensorDto
    {
        public string ApiKey { get; set; } = null!;
        public List<double> HumidityValues { get; set; } = [];
        public DateTime Timestamp { get; set; }
    }
}