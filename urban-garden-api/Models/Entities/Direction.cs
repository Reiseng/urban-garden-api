namespace UrbanGarden.Api.Models.Entities
{
public class Direction
{
    public string Street { get; set; } = "";
    public string City { get; set; } = "";
    public string State { get; set; } = "";
    public string ZipCode { get; set; } = "";

    public Direction() { }
}
}