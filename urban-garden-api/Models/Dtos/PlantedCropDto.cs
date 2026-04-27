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
}