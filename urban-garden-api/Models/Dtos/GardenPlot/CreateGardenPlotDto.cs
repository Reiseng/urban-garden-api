using System.ComponentModel.DataAnnotations;

namespace UrbanGarden.Api.Models.Dtos
{
    public class CreateGardenPlotDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 100 characters")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Size is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Size must be a positive number")]
        public double Size { get; set; }
        [Required(ErrorMessage = "Location is required")]
        public DirectionDto Location { get; set; } = new DirectionDto();
    }
}