using UrbanGarden.Api.Models.Entities;

namespace UrbanGarden.Api.Models.Dtos
{
    public class GardenPlotDto
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public double Size { get; set; }
        public DirectionDto Location { get; set; } = new DirectionDto();
        public PlantedCropDto? ActiveCrop { get; set; }
        //public List<TaskDto> Tasks { get; set; } = new();
    }
}