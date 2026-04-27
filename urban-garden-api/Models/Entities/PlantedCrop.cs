using UrbanGarden.Api.Models.Enums;

namespace UrbanGarden.Api.Models.Entities
{
    public class PlantedCrop
    {
        public int Id { get; set; }

        public required int CropTypeId { get; set; }
        public required int GardenPlotId { get; set; }
        public DateTime PlantedAt { get; set; } = DateTime.UtcNow;
        public CropStatus State { get; set; } = CropStatus.Planted;
    }
}