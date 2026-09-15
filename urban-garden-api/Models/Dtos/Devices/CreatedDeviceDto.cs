using UrbanGarden.Api.Infrastructure.MQTT.Dtos;

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
        /// <summary>
        /// Configuración del dispositivo.
        /// </summary>
        public ConfigDeviceDto Config { get; set; } = new ConfigDeviceDto();
    }
}