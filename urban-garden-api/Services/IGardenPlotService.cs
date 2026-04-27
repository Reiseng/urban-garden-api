using UrbanGarden.Api.Models.Dtos;
using UrbanGarden.Api.Models.Entities;

namespace UrbanGarden.Api.Services
{
    /// <summary>
    /// Interfaz para el servicio de huertos urbanos.
    /// </summary>
    public interface IGardenPlotService
    {
        /// <summary>
        /// Obtiene todos los huertos urbanos disponibles.
        /// </summary>
        /// <returns>Enumerable de huertos.</returns>
        IEnumerable<GardenPlot> GetAll();

        /// <summary>
        /// Obtiene un huerto por su ID.
        /// </summary>
        /// <param name="id">ID del huerto.</param>
        /// <returns>El huerto encontrado o null si no existe.</returns>
        GardenPlot? GetById(int id);

        /// <summary>
        /// Agrega un nuevo huerto urbano.
        /// </summary>
        /// <param name="GardenPlot">Huerto a agregar.</param>
        void Add(GardenPlot GardenPlot);

        /// <summary>
        /// Actualiza información básica de un huerto.
        /// </summary>
        /// <param name="id">ID del huerto.</param>
        /// <param name="dto">Datos actualizados del huerto.</param>
        void UpdateBasicInfo(int id, UpdateGardenPlotDto dto);

        /// <summary>
        /// Elimina un huerto por su ID.
        /// </summary>
        /// <param name="id">ID del huerto a eliminar.</param>
        void Delete(int id);

        /// <summary>
        /// Planta un cultivo en un huerto.
        /// </summary>
        /// <param name="gardenPlotId">ID del huerto.</param>
        /// <param name="cropTypeId">ID del tipo de cultivo.</param>
        void PlantCrop(int gardenPlotId, int cropTypeId);

        /// <summary>
        /// Cosecha el cultivo activo de un huerto.
        /// </summary>
        /// <param name="gardenPlotId">ID del huerto.</param>
        void HarvestCrop(int gardenPlotId);

        /// <summary>
        /// Elimina el cultivo activo de un huerto.
        /// </summary>
        /// <param name="gardenPlotId">ID del huerto.</param>
        void RemoveCrop(int gardenPlotId);
    }
}