using System.Text.Json.Serialization;
using UrbanGarden.Api.Models.Enums;

namespace UrbanGarden.Api.Models.Dtos
{
    public class CultivoDto
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public Season Season { get; set; } = Season.YearRound;
        public bool? Disponible { get; set; }
    }
}