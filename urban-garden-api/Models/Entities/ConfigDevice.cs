namespace UrbanGarden.Api.Models.Entities
{
    public class ConfigDevice
    {
        public int SoilSensorCount { get; set; } = 5;
        public int TemperatureInterval { get; set; } = 900;       // 15 min
        public int SoilMoistureInterval { get; set; } = 3600;    // 1 h
        public int KeepAliveInterval { get; set; } = 600;        // 10 min
    }
}