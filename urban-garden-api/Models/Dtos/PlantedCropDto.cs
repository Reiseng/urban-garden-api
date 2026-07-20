using UrbanGarden.Api.Models.Entities;
using UrbanGarden.Api.Models.Enums;

namespace UrbanGarden.Api.Models.Dtos
{
    public class PlantedCropDto
    {
        public int Id { get; set; }
        public CropTypeDto CropType { get; set; } = null!;
        public GardenPlotDto GardenPlot { get; set; } = null!;
        public DateTime PlantedAt { get; set; } = DateTime.UtcNow;
        public CropStatus State { get; set; } = CropStatus.Planted;
    }
    public class PlantCropDto
    {
        public int CropTypeId { get; set; }
    }
    public class UpdatePlantedCropDto
    {
        public int plantID { get; set; }
        public CropStatus State { get; set; }
    }
}