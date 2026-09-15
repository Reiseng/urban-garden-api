namespace UrbanGarden.Api.Models.Entities
{
    public class CommandDevice
    {
        public required string CommandName { get; set; }
        public Dictionary<string, object> Parameters { get; set; } = new();
    }
}
