namespace UrbanGarden.Api.Models.Dtos
{
public class RegisterDeviceDto
    {
        public string MacAddress { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string RegistrationKey { get; set; } = null!;
    }
}