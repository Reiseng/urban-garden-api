using System.Text.Json.Serialization;
using UrbanGarden.Api.Models.Enums;

namespace UrbanGarden.Api.Models.Dtos
{
    public class CropTypeDto
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public List<Season> Season { get; set; } = new List<Season>();
        public bool IsPerennial { get; set; }
        public bool? Disponible { get; set; }
    }
}