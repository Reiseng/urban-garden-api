using UrbanGarden.Api.Models.Entities;
using UrbanGarden.Api.Models.Enums;

namespace UrbanGarden.Api.Services
{
    /// <summary>
    /// Interfaz para el servicio de cultivos plantados.
    /// </summary>
    public interface IHarvestService
    {
        /// <summary>
        /// Obtiene todas las cosechas realizadas.
        /// </summary>
        /// <returns>Enumerable de cosechas realizadas.</returns>
        IEnumerable<Harvest> GetAll();

        /// <summary>
        /// Obtiene una cosecha realizada por su ID.
        /// </summary>
        /// <param name="id">ID de la cosecha.</param>
        /// <returns>La cosecha realizada o null si no existe.</returns>
        Harvest? GetById(int id);

        /// <summary>
        /// Obtiene todas las cosechas realizadas en una parcela.
        /// </summary>
        /// <returns>Enumerable de cosechas realizadas en una parcela.</returns>
        IEnumerable<Harvest> GetAllByPlotId(int id);
    }
}