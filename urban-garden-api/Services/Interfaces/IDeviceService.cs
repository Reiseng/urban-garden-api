using UrbanGarden.Api.Models.Dtos;
using UrbanGarden.Api.Models.Entities;

namespace UrbanGarden.Api.Services
{
    /// <summary>
    /// Interfaz para el servicio de dispositivos.
    /// </summary>
    public interface IDeviceService
    {
        /// <summary>
        /// Obtiene todos los dispositivos registrados.
        /// </summary>
        /// <returns>Enumerable de dispositivos.</returns>
        IEnumerable<Device> GetAll();

        /// <summary>
        /// Obtiene un dispositivo por su ID.
        /// </summary>
        /// <param name="id">ID del dispositivo.</param>
        /// <returns>El dispositivo encontrado o null si no existe.</returns>
        Device? GetById(Guid id);

        /// <summary>
        /// Obtiene un dispositivo por su dirección MAC.
        /// </summary>
        /// <param name="macAddress">Dirección MAC del dispositivo.</param>
        /// <returns>El dispositivo encontrado o null si no existe.</returns>
        Device? GetByMacAddress(string macAddress);

        /// <summary>
        /// Agrega un nuevo dispositivo.
        /// </summary>
        /// <param name="device">Dispositivo a agregar.</param>
        /// <returns>ApiKey necesaria para publicaciones atraves de MQTT.</returns>
        CreateDeviceDto Add(RegisterDeviceDto device);

        /// <summary>
        /// Actualiza información de un dispositivo.
        /// </summary>
        /// <param name="id">ID del dispositivo.</param>
        /// <param name="dto">Datos actualizados del dispositivo.</param>
        void Update(Guid id, UpdateDeviceDto dto);

        /// <summary>
        /// Actualiza la fecha de última conexión de un dispositivo.
        /// </summary>
        /// <param name="device">Dispositivo a actualizar.</param>
        /// <param name="lastSeen">Fecha de última conexión.</param>
        void UpdateLastSeen(Device device, DateTime lastSeen);

        /// <summary>
        /// Elimina un dispositivo por su ID.
        /// </summary>
        /// <param name="id">ID del dispositivo a eliminar.</param>
        void Delete(Guid id);
    }
}