namespace UrbanGarden.Api.Infrastructure.MQTT.Dtos
{
    public class CommandDto
    {
        public required string CommandName { get; set; }
        public Dictionary<string, object> Parameters { get; set; } = new();
    }
}