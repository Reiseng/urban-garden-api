namespace UrbanGarden.Api.Models.Dtos
{
public class CreateDeviceDto
    {
        /// <summary>
        /// Identificador único del dispositivo.
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// Clave API del dispositivo.
        /// </summary>
        public string ApiKey { get; set; } = null!;
    }
}