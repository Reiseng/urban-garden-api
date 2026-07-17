namespace UrbanGarden.Api.Models.Entities
{
    public class SoilSensorReadings
    {
        public int ID { get; set; }
        public Guid DeviceID { get; set; }
        public Device Device { get; set; } = null!;
        public int SensorIndex { get; set; }
        public double Moisture { get; set; }
        public DateTime Timestamp { get; set; }
    }
}