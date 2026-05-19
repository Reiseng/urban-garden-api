namespace UrbanGarden.Api.Models.Dtos
{
public class CreateDeviceDto
    {
        public Guid ID { get; set; }
        public string ApiKey { get; set; } = null!;
    }
}