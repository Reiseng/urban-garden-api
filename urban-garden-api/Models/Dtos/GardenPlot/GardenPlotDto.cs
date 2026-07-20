using UrbanGarden.Api.Models.Entities;

namespace UrbanGarden.Api.Models.Dtos
{
    public class GardenPlotDto
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public double Size { get; set; }
        public DirectionDto Location { get; set; } = new DirectionDto();
        public ICollection<DeviceDto> Devices { get; set; } = new List<DeviceDto>();
        public ICollection<PlantedCropDto> PlantedCrops { get; set; } 
            = new List<PlantedCropDto>();
    }
}