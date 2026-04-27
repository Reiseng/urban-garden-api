using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using UrbanGarden.Api.Models.Enums;

namespace UrbanGarden.Api.Models.Dtos
{
    public class CreateCropTypeDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 100 characters")]
        public required string Name { get; set; }
        public List<Season> Season { get; set; } = new List<Season>();
        /// <summary>
        /// Indica si el cultivo se puede volver a cosechar después de la primera cosecha (perenne) o si es necesario replantarlo cada temporada (anual).
        /// </summary>
        public bool IsPerennial { get; set; }
        public bool? Disponible { get; set; }
    }
}
