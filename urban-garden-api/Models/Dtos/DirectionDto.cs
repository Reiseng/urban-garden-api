using System.ComponentModel.DataAnnotations;

namespace UrbanGarden.Api.Models.Dtos
{
public class DirectionDto
{
    [Required]
    public string Street { get; set; } = "";
    [Required]
    public string City { get; set; } = "";
    [Required]
    public string State { get; set; } = "";
    [Required]
    public string ZipCode { get; set; } = "";

    public DirectionDto() { }
}
}