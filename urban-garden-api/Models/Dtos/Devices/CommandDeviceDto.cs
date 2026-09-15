namespace UrbanGarden.Api.Models.Dtos
{
    public class CommandDeviceDto
    {
        public required string CommandName { get; set; }
        public Dictionary<string, object> Parameters { get; set; } = new();
    }
}
