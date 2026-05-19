namespace UrbanGarden.Api.Models.Dtos
{
    public class DeviceDto
    {
        public Guid ID { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}