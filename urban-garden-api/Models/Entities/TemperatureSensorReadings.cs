namespace UrbanGarden.Api.Models.Entities
{
    public class TemperatureSensorReadings
    {
        public int ID { get; set; }
        public Guid DeviceID { get; set; }
        public Device Device { get; set; } = null!;
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public DateTime Timestamp { get; set; }
    }
}