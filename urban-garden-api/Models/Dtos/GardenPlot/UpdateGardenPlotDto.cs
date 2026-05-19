using System.ComponentModel.DataAnnotations;

namespace UrbanGarden.Api.Models.Dtos
{
    public class UpdateGardenPlotDto
    {
        [StringLength(100, MinimumLength = 1)]
        public string? Name { get; set; }

        [Range(0.01, double.MaxValue)]
        public double? Size { get; set; }

        public DirectionDto? Location { get; set; }
    }
}