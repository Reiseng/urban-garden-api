using UrbanGarden.Api.Models.Enums;

namespace UrbanGarden.Api.Models.Entities
{
    public class PlantedCrop
    {
        public int Id { get; set; }

        public int CropTypeId { get; set; }
        public CropType CropType { get; set; } = null!;
        public int GardenPlotId { get; set; }
        public GardenPlot GardenPlot { get; set; } = null!;
        public DateTime PlantedAt { get; set; } = DateTime.UtcNow;
        public CropStatus State { get; set; } = CropStatus.Planted;
        public ICollection<Harvest> Harvests { get; set; }
        = new List<Harvest>();
    }
}