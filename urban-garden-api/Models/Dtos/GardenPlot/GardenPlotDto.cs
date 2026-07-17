using UrbanGarden.Api.Models.Entities;

namespace UrbanGarden.Api.Models.Dtos
{
    public class GardenPlotDto
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public double Size { get; set; }
        public DirectionDto Location { get; set; } = new DirectionDto();
        public ICollection<Device> Devices { get; set; } = new List<Device>();
        public ICollection<PlantedCrop> PlantedCrops { get; set; } 
            = new List<PlantedCrop>();
    }
}