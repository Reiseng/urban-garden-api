namespace UrbanGarden.Api.Models.Entities
{
    public class Device
    {
        public int Id { get; set; }
        public string MacAddress { get; set; } = null!;
        public string ApiKey { get; set; } = null!;
        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}