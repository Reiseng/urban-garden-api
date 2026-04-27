using UrbanGarden.Api.Models.Entities;

namespace UrbanGarden.Api.Services
{
    /// <summary>
    /// Interfaz para el servicio de tipos de cultivos.
    /// </summary>
    public interface ICropTypeService
    {
        /// <summary>
        /// Obtiene todos los tipos de cultivos disponibles.
        /// </summary>
        /// <returns>Enumerable de tipos de cultivos.</returns>
        IEnumerable<CropType> GetAll();

        /// <summary>
        /// Obtiene un tipo de cultivo por su ID.
        /// </summary>
        /// <param name="id">ID del tipo de cultivo.</param>
        /// <returns>El tipo de cultivo encontrado o null si no existe.</returns>
        CropType? GetById(int id);

        /// <summary>
        /// Agrega un nuevo tipo de cultivo.
        /// </summary>
        /// <param name="cropType">Tipo de cultivo a agregar.</param>
        void Add(CropType cropType);

        /// <summary>
        /// Actualiza un tipo de cultivo existente.
        /// </summary>
        /// <param name="cropType">Tipo de cultivo con datos actualizados.</param>
        void Update(CropType cropType);

        /// <summary>
        /// Elimina un tipo de cultivo por su ID.
        /// </summary>
        /// <param name="id">ID del tipo de cultivo a eliminar.</param>
        void Delete(int id);
    }
}