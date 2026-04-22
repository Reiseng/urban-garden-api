namespace UrbanGarden.Api.Models.Dtos
{
public class DirectionDto
{
    public string? Street { get; set; } = "";
    public string? City { get; set; } = "";
    public string? State { get; set; } = "";
    public string? ZipCode { get; set; } = "";

    public DirectionDto() { }
}
}