using UrbanGarden.Api.Models.Enums;

namespace UrbanGarden.Api.Models.Entities
{
    public class CropType
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public Season Season { get; set; } = Season.YearRound;
        public bool IsPerennial { get; set; }
        public bool Disponible { get; set; } = true;
    }
}