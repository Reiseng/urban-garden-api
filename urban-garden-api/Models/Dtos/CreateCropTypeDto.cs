using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using UrbanGarden.Api.Models.Enums;

namespace UrbanGarden.Api.Models.Dtos
{
    public class CreateCropTypeDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 100 characters")]
        public required string Name { get; set; }
        [JsonConverter(typeof(JsonNumberEnumConverter<Season>))]
        public Season Season { get; set; } = Season.YearRound;
        public bool IsPerennial { get; set; }
        public bool? Disponible { get; set; }
    }
}
