using UrbanGarden.Api.Models.Entities;
using UrbanGarden.Api.Models.Enums;

namespace UrbanGarden.Api.Services
{
    /// <summary>
    /// Interfaz para el servicio de cultivos plantados.
    /// </summary>
    public interface IPlantedCropService
    {
        /// <summary>
        /// Obtiene todos los cultivos plantados.
        /// </summary>
        /// <returns>Enumerable de cultivos plantados.</returns>
        IEnumerable<PlantedCrop> GetAll();

        /// <summary>
        /// Obtiene un cultivo plantado por su ID.
        /// </summary>
        /// <param name="id">ID del cultivo plantado.</param>
        /// <returns>El cultivo plantado encontrado o null si no existe.</returns>
        PlantedCrop? GetById(int id);

        /// <summary>
        /// Agrega un nuevo cultivo plantado.
        /// </summary>
        /// <param name="plantedCrop">Cultivo plantado a agregar.</param>
        void Add(PlantedCrop plantedCrop);

        /// <summary>
        /// Actualiza el estado de un cultivo plantado.
        /// </summary>
        /// <param name="id">ID del cultivo plantado.</param>
        /// <param name="status">Nuevo estado del cultivo.</param>
        void Update(int id, CropStatus status);

        /// <summary>
        /// Cosecha un cultivo plantado.
        /// </summary>
        /// <param name="id">ID del cultivo plantado.</param>
        /// <returns>True si el cultivo debe ser eliminado, false si es perenne.</returns>
        bool Harvest(int id);

        /// <summary>
        /// Elimina un cultivo plantado por su ID.
        /// </summary>
        /// <param name="id">ID del cultivo plantado a eliminar.</param>
        void Delete(int id);
    }
}