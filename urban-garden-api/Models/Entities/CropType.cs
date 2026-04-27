using UrbanGarden.Api.Models.Enums;

namespace UrbanGarden.Api.Models.Entities
{
    public class CropType
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public List<Season> Season { get; set; } = new List<Season>();
        public bool IsPerennial { get; set; }
        public bool Disponible { get; set; } = true;
    }
}