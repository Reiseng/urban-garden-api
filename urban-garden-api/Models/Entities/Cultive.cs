using System.ComponentModel.DataAnnotations;
using UrbanGarden.Api.Models.Enums;

namespace UrbanGarden.Api.Models.Entities
{
    public class Cultive
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public Season Season { get; set; } = Season.YearRound;
        public bool Disponible { get; set; } = true;
    }
}