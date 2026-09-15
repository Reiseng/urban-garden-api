namespace UrbanGarden.Api.Models.Dtos
{
    public class ConfigDeviceDto
    {
        public int SoilSensorCount { get; set; }
        public int TemperatureInterval { get; set; }
        public int SoilMoistureInterval { get; set; }
        public int KeepAliveInterval { get; set; }
    }
}