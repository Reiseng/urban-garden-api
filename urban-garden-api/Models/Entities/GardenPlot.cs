namespace UrbanGarden.Api.Models.Entities
{
    public class GardenPlot
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public double Size { get; set; }
        public Direction Location { get; set; } = new Direction();
        public ICollection<Device> Devices { get; set; } = new List<Device>();
        public ICollection<PlantedCrop> PlantedCrops { get; set; } 
            = new List<PlantedCrop>();
    }
}