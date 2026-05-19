namespace UrbanGarden.Api.Models.Dtos
{
    /// <summary>
    /// DTO para actualizar información de un dispositivo.
    /// </summary>
    public class UpdateDeviceDto
    {
        /// <summary>
        /// Nombre del dispositivo.
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}