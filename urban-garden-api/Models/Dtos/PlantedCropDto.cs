using UrbanGarden.Api.Models.Entities;
using UrbanGarden.Api.Models.Enums;

namespace UrbanGarden.Api.Models.Dtos
{
    public class PlantedCropDto
    {
        public int Id { get; set; }

        public required int CropTypeId { get; set; }

        public DateTime PlantedAt { get; set; } = DateTime.UtcNow;
        public CropStatus State { get; set; } = CropStatus.Planted;
    }
    public class PlantCropDto
    {
        public int CropTypeId { get; set; }
    }
    public class UpdatePlantedCropDto
    {
        public CropStatus State { get; set; }
    }
}