using UrbanGarden.Api.Models.Entities;
using UrbanGarden.Api.Models.Enums;

namespace UrbanGarden.Api.Services
{
    /// <summary>
    /// Servicio para gestionar operaciones de cultivos plantados.
    /// </summary>
    public class PlantedCropService : IPlantedCropService
    {
        private readonly IPlantedCropRepository _repository;
        private readonly IHarvestRepository _harvestRepository;
        private readonly ICropTypeRepository _cropTypeRepository;

        /// <summary>
        /// Constructor del servicio de cultivos plantados.
        /// </summary>
        /// <param name="repository">Repositorio de cultivos plantados.</param>
        /// <param name="harvestRepository">Repositorio de cosechas.</param>
        /// <param name="cropTypeRepository">Repositorio de tipos de cultivos.</param>
        public PlantedCropService(IPlantedCropRepository repository, IHarvestRepository harvestRepository, ICropTypeRepository cropTypeRepository)
        {
            _repository = repository;
            _harvestRepository = harvestRepository;
            _cropTypeRepository = cropTypeRepository;
        }

        /// <summary>
        /// Obtiene todos los cultivos plantados.
        /// </summary>
        /// <returns>Enumerable de cultivos plantados.</returns>
        public IEnumerable<PlantedCrop> GetAll()
        {
            return _repository.GetAll();
        }

        /// <summary>
        /// Obtiene un cultivo plantado por su ID.
        /// </summary>
        /// <param name="id">ID del cultivo plantado.</param>
        /// <returns>El cultivo plantado encontrado o null si no existe.</returns>
        public PlantedCrop? GetById(int id)
        {
            return _repository.GetById(id);
        }

        /// <summary>
        /// Agrega un nuevo cultivo plantado.
        /// </summary>
        /// <param name="plantedCrop">Cultivo plantado a agregar.</param>
        public void Add(PlantedCrop plantedCrop)
        {
            _repository.Add(plantedCrop);
        }

        /// <summary>
        /// Actualiza el estado de un cultivo plantado.
        /// </summary>
        /// <param name="id">ID del cultivo plantado.</param>
        /// <param name="status">Nuevo estado del cultivo.</param>
        /// <exception cref="KeyNotFoundException">Thrown when the planted crop is not found.</exception>
        public void Update(int id, CropStatus status)
        {
            var plantedCrop = _repository.GetById(id) ?? throw new KeyNotFoundException("PlantedCrop not found");
            plantedCrop.State = status;
            _repository.Update(plantedCrop);
        }

        /// <summary>
        /// Cosecha un cultivo plantado.
        /// </summary>
        /// <param name="id">ID del cultivo plantado.</param>
        /// <returns>True si el cultivo debe ser eliminado, false si es perenne.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the planted crop is not found.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the crop is not ready for harvest.</exception>
        public bool Harvest(int id)
        {
            var plantedCrop = _repository.GetById(id) ?? throw new KeyNotFoundException("PlantedCrop not found");
            var cropType = _cropTypeRepository.GetById(plantedCrop.CropTypeId);

            if (plantedCrop.State != CropStatus.ReadyForHarvest)
                throw new InvalidOperationException("Crop is not ready for harvest");

            _harvestRepository.Add(new Harvest
            {
                PlantedCropId = plantedCrop.Id,
                CropTypeId = plantedCrop.CropTypeId,
                Quantity = 1.0m,
                Date = DateTime.UtcNow
            });

            if (cropType?.IsPerennial == true)
            {
                plantedCrop.State = CropStatus.Growing;
                _repository.Update(plantedCrop);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Elimina un cultivo plantado por su ID.
        /// </summary>
        /// <param name="id">ID del cultivo plantado a eliminar.</param>
        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}