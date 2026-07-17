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
        /// <param name="dto">Datos a registrar de al cosecha.</param>
        void HarvestCrop(int gardenPlotId, CreateHarvestDto dto);

        /// <summary>
        /// Elimina el cultivo activo de un huerto.
        /// </summary>
        /// <param name="gardenPlotId">ID del huerto.</param>
        /// <param name="cropId">ID del cultivo a remover </param>
        void RemoveCrop(int gardenPlotId, int cropId);

        /// <summary>
        /// Actualiza el estado del cultivo activo de un huerto.
        /// </summary>
        /// <param name="gardenPlotId">ID del huerto.</param>
        /// <param name="dto">Datos a actualizar del cultivo activo.</param>
        void UpdateStatus(int gardenPlotId, UpdatePlantedCropDto dto);

        /// <summary>
        /// Obtiene el historial de cosechas de un huerto.
        /// </summary>
        /// <param name="gardenPlotId">ID del huerto.</param>
        /// <returns>Enumerable de cosechas.</returns>
        IEnumerable<Harvest> GetHarvests(int gardenPlotId);

        /// <summary>
        /// Agrega un dispositivo de monitoreo a un huerto.
        /// </summary>
        /// <param name="gardenPlotId">ID del huerto.</param>
        /// <param name="deviceId">ID del dispositivo.</param>
        void AddDevice(int gardenPlotId, Guid deviceId);

        /// <summary>
        /// Elimina un dispositivo de monitoreo de un huerto.
        /// </summary>
        /// <param name="gardenPlotId">ID del huerto.</param>
        /// <param name="deviceId">ID del dispositivo.</param>
        void RemoveDevice(int gardenPlotId, Guid deviceId);
    }
}