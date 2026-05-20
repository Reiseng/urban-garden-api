using System.ComponentModel.DataAnnotations;

namespace UrbanGarden.Api.Models.Dtos
{
public class RegisterDeviceDto
    {
        [Required(ErrorMessage = "MAC Address is required")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "MAC Address must be between 1 and 100 characters")]
        [RegularExpression(@"^([0-9A-Fa-f]{2}[:\-]){5}([0-9A-Fa-f]{2})$", ErrorMessage = "Invalid MAC Address format")]
        public string MacAddress { get; set; } = null!;
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 100 characters")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Registration Key is required")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Registration Key must be between 1 and 100 characters")]
        public string RegistrationKey { get; set; } = null!;
    }
}