using UrbanGarden.Api.Models.Entities;

namespace UrbanGarden.Api.Services
{
    /// <summary>
    /// Servicio para gestionar operaciones de tipos de cultivos.
    /// </summary>
    public class CropTypeService : ICropTypeService
    {
        private readonly ICropTypeRepository _repository;

        /// <summary>
        /// Constructor del servicio de tipos de cultivos.
        /// </summary>
        /// <param name="repository">Repositorio de tipos de cultivos.</param>
        public CropTypeService(ICropTypeRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Obtiene todos los tipos de cultivos disponibles.
        /// </summary>
        /// <returns>Enumerable de tipos de cultivos.</returns>
        public IEnumerable<CropType> GetAll()
        {
            return _repository.GetAll();
        }

        /// <summary>
        /// Obtiene un tipo de cultivo por su ID.
        /// </summary>
        /// <param name="id">ID del tipo de cultivo.</param>
        /// <returns>El tipo de cultivo encontrado o null si no existe.</returns>
        public CropType? GetById(int id)
        {
            return _repository.GetById(id);
        }

        /// <summary>
        /// Agrega un nuevo tipo de cultivo.
        /// </summary>
        /// <param name="cropType">Tipo de cultivo a agregar.</param>
        public void Add(CropType cropType)
        {
            _repository.Add(cropType);
        }

        /// <summary>
        /// Actualiza un tipo de cultivo existente.
        /// </summary>
        /// <param name="cropType">Tipo de cultivo con datos actualizados.</param>
        /// <exception cref="KeyNotFoundException">Thrown when the crop type is not found.</exception>
        public void Update(CropType cropType)
        {
            if (_repository.GetById(cropType.ID) == null) throw new KeyNotFoundException("CropType not found");
            _repository.Update(cropType);
        }

        /// <summary>
        /// Elimina un tipo de cultivo por su ID.
        /// </summary>
        /// <param name="id">ID del tipo de cultivo a eliminar.</param>
        /// <exception cref="KeyNotFoundException">Thrown when the crop type is not found.</exception>
        public void Delete(int id)
        {
            if (_repository.GetById(id) == null) throw new KeyNotFoundException("CropType not found");
            _repository.Delete(id);
        }
    }
}