using System.Data;

namespace UrbanGarden.Api.Models.Dtos
{
    public class DeviceDto
    {
        /// <summary>
        /// Identificador único del dispositivo.
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// Nombre del dispositivo.
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// Fecha y hora de creación del dispositivo.
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Fecha y hora de la última vez que el dispositivo se comunicó con el sistema.
        /// </summary>
        public DateTime? LastSeenAt { get; set; }
    }
}