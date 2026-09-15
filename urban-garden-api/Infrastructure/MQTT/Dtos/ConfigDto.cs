namespace UrbanGarden.Api.Infrastructure.MQTT.Dtos
{
    public class ConfigDto
    {
        public int SoilSensorCount { get; set; }
        public int TemperatureInterval { get; set; }
        public int SoilMoistureInterval { get; set; }
        public int KeepAliveInterval { get; set; }
    }
}