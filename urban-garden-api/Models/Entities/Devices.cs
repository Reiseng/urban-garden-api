namespace UrbanGarden.Api.Models.Entities
{
    public class Device
    {
        public Guid ID { get; set; }
        public string MacAddress { get; set; } = null!;
        public string ApiKey { get; set; } = null!;
        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? LastSeenAt { get; set; }
        public int GardenPlotId { get; set; }
        public GardenPlot GardenPlot { get; set; } = null!;
        public ICollection<SoilSensorReadings> SoilReadings { get; set; }
            = new List<SoilSensorReadings>();


        public ICollection<TemperatureSensorReadings> TemperatureReadings { get; set; }
            = new List<TemperatureSensorReadings>();
    }
}