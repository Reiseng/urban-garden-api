using UrbanGarden.Api.Models.Dtos;
using UrbanGarden.Api.Models.Entities;
using UrbanGarden.Api.Models.Enums;
using UrbanGarden.Api.Repositories;

namespace UrbanGarden.Api.Services
{
    /// <summary>
    /// Servicio para gestionar operaciones de huertos urbanos.
    /// </summary>
    public class GardenPlotService : IGardenPlotService
    {
        private readonly IGardenPlotRepository _gardenPlotRepository;
        private readonly IPlantedCropService _plantedCropService;
        private readonly IHarvestService _harvestService;

        /// <summary>
        /// Constructor del servicio de huertos.
        /// </summary>
        /// <param name="gardenPlotRepository">Repositorio de huertos.</param>
        /// <param name="plantedCropService">Servicio de cultivos plantados.</param>
        /// <param name="harvestService">Servicio del historial de cosechas.</param>
        public GardenPlotService(IGardenPlotRepository gardenPlotRepository, IPlantedCropService plantedCropService, IHarvestService harvestService)
        {
            _gardenPlotRepository = gardenPlotRepository;
            _plantedCropService = plantedCropService;
            _harvestService = harvestService;
        }

        /// <summary>
        /// Obtiene todos los huertos urbanos disponibles.
        /// </summary>
        /// <returns>Enumerable de huertos.</returns>
        public IEnumerable<GardenPlot> GetAll()
        {
            return _gardenPlotRepository.GetAll();
        }

        /// <summary>
        /// Obtiene un huerto por su ID.
        /// </summary>
        /// <param name="id">ID del huerto.</param>
        /// <returns>El huerto encontrado o null si no existe.</returns>
        public GardenPlot? GetById(int id)
        {
            return _gardenPlotRepository.GetById(id);
        }

        /// <summary>
        /// Agrega un nuevo huerto urbano.
        /// </summary>
        /// <param name="gardenPlot">Huerto a agregar.</param>
        public void Add(GardenPlot gardenPlot)
        {
            _gardenPlotRepository.Add(gardenPlot);
        }

        /// <summary>
        /// Actualiza información básica de un huerto.
        /// </summary>
        /// <param name="id">ID del huerto.</param>
        /// <param name="dto">Datos actualizados del huerto.</param>
        /// <exception cref="KeyNotFoundException">Thrown when the garden plot is not found.</exception>
        public void UpdateBasicInfo(int id, UpdateGardenPlotDto dto)
        {
            var gardenPlot = _gardenPlotRepository.GetById(id) ?? throw new KeyNotFoundException("Garden plot not found");
            if (dto.Name != null) gardenPlot.Name = dto.Name;
            if (dto.Size.HasValue) gardenPlot.Size = dto.Size.Value;
            if (dto.Location != null)
            {
                gardenPlot.Location = new Direction
                {
                    Street = dto.Location.Street,
                    City = dto.Location.City,
                    State = dto.Location.State,
                    ZipCode = dto.Location.ZipCode
                };
            }
            _gardenPlotRepository.Update(gardenPlot);
        }

        /// <summary>
        /// Elimina un huerto por su ID.
        /// </summary>
        /// <param name="id">ID del huerto a eliminar.</param>
        /// <exception cref="KeyNotFoundException">Thrown when the garden plot is not found.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the garden plot has an active crop.</exception>
        public void Delete(int id)
        {
            var gardenPlot = _gardenPlotRepository.GetById(id) ?? throw new KeyNotFoundException("Garden plot not found");
            if (gardenPlot.PlantedCrops != null) throw new InvalidOperationException("Remove crop before deleting plot");
            _gardenPlotRepository.Delete(id);
        }

        /// <summary>
        /// Planta un cultivo en un huerto.
        /// </summary>
        /// <param name="gardenPlotId">ID del huerto.</param>
        /// <param name="cropTypeId">ID del tipo de cultivo.</param>
        /// <exception cref="KeyNotFoundException">Thrown when the garden plot is not found.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the garden plot already has an active crop.</exception>
        public void PlantCrop(int gardenPlotId, int cropTypeId)
        {
            var gardenPlot = _gardenPlotRepository.GetById(gardenPlotId) ?? throw new KeyNotFoundException("Garden plot not found");
            var existingCrop = FindPlantedCrop(gardenPlot, c => c.CropTypeId == cropTypeId);
            if (existingCrop?.State == CropStatus.Withered)
                throw new InvalidOperationException("Cannot plant the same crop type until the withered crop is removed");

            var plantedCrop = new PlantedCrop
            {
                CropTypeId = cropTypeId,
                GardenPlotId = gardenPlotId,
                State = CropStatus.Planted,
                PlantedAt = DateTime.UtcNow
            };

            _plantedCropService.Add(plantedCrop);
            gardenPlot.PlantedCrops ??= new List<PlantedCrop>();
            gardenPlot.PlantedCrops.Add(plantedCrop);
            _gardenPlotRepository.Update(gardenPlot);
        }

        /// <summary>
        /// Cosecha el cultivo activo de un huerto.
        /// </summary>
        /// <param name="gardenPlotId">ID del huerto.</param>
        /// <param name="dto">Datos a registrar de al cosecha.</param>
        /// <exception cref="KeyNotFoundException">Thrown when the garden plot is not found.</exception>
        /// <exception cref="InvalidOperationException">Thrown when there is no crop ready to harvest.</exception>
        public void HarvestCrop(int gardenPlotId, CreateHarvestDto dto)
        {
            var gardenPlot = _gardenPlotRepository.GetById(gardenPlotId) ?? throw new KeyNotFoundException("Garden plot not found");
            if (dto.Quantity <= 0) throw new InvalidOperationException("Quantity need to be a positive value.");
            var targetCrop = FindPlantedCrop(gardenPlot, c => c.Id == dto.cropId);
            if (gardenPlot.PlantedCrops == null || targetCrop?.State != CropStatus.ReadyForHarvest)
                throw new InvalidOperationException("No crop ready to harvest in this garden plot");

            var shouldRemove = _plantedCropService.Harvest(targetCrop!.Id, dto.Quantity);
            if (shouldRemove)
            {
                _plantedCropService.Delete(targetCrop.Id);
                gardenPlot.PlantedCrops.Remove(targetCrop);
                _gardenPlotRepository.Update(gardenPlot);
            }
        }

        /// <summary>
        /// Elimina el cultivo activo de un huerto.
        /// </summary>
        /// <param name="gardenPlotId">ID del huerto.</param>
        /// <param name="cropId" >ID del cultivo a remover</param>
        /// <exception cref="KeyNotFoundException">Thrown when the garden plot is not found.</exception>
        /// <exception cref="InvalidOperationException">Thrown when there is no active crop in the garden plot.</exception>
        public void RemoveCrop(int gardenPlotId, int cropId)
        {
            var gardenPlot = _gardenPlotRepository.GetById(gardenPlotId) ?? throw new KeyNotFoundException("Garden plot not found");
            if (gardenPlot.PlantedCrops == null) throw new InvalidOperationException("No active crop in this garden plot");
            var removeCrop = FindPlantedCrop(gardenPlot, c => c.Id == cropId)
                ?? throw new InvalidOperationException("Crop not found in this garden plot");
            _plantedCropService.Delete(removeCrop.Id);
            gardenPlot.PlantedCrops.Remove(removeCrop);
            _gardenPlotRepository.Update(gardenPlot);
        }
        /// <summary>
        /// Actualiza el estado del cultivo activo de un huerto.
        /// </summary>
        /// <param name="gardenPlotId">ID del huerto.</param>
        /// <param name="dto">Datos actualizados del cultivo activo.</param>
        /// <exception cref="KeyNotFoundException">Thrown when the garden plot is not found.</exception>
        /// <exception cref="InvalidOperationException">Thrown when there is no valid data.</exception>
        public void UpdateStatus(int gardenPlotId, UpdatePlantedCropDto dto)
        {
            var gardenPlot = _gardenPlotRepository.GetById(gardenPlotId) 
                ?? throw new KeyNotFoundException("Garden plot not found");
            if (gardenPlot.PlantedCrops == null || gardenPlot.PlantedCrops.Count <= 0) 
                throw new InvalidOperationException("No active crop in this garden plot");

            var targetCrop = FindPlantedCrop(gardenPlot, c => c.Id == dto.plantID);
            if (targetCrop?.State == dto.State)
                throw new InvalidOperationException("The crop is already in the specified state.");
            if (!IsValidTransition(targetCrop!.State, dto.State))
                throw new InvalidOperationException("Invalid state transition.");   
            if (targetCrop.State == CropStatus.Withered)
                throw new InvalidOperationException("Withered crops cannot be updated. Please remove the crop from the plot.");

            targetCrop.State = dto.State;
            _plantedCropService.Update(targetCrop.Id, dto.State);
            _gardenPlotRepository.Update(gardenPlot);
        }

        private PlantedCrop? FindPlantedCrop(GardenPlot gardenPlot, System.Func<PlantedCrop, bool> predicate)
        {
            if (gardenPlot.PlantedCrops == null) return null;
            foreach (var crop in gardenPlot.PlantedCrops)
            {
                if (predicate(crop))
                    return crop;
            }
            return null;
        }

        public IEnumerable<Harvest> GetHarvests(int gardenPlotId)
        {
            var gardenPlot = _gardenPlotRepository.GetById(gardenPlotId) 
                ?? throw new KeyNotFoundException("Garden plot not found");
            
            return _harvestService.GetAllByPlotId(gardenPlotId);
        }
        private bool IsValidTransition(CropStatus current, CropStatus next)
        {
            if (next == CropStatus.Withered)
                return true;
            return (current, next) switch
            {
                (CropStatus.Planted, CropStatus.Growing) => true,
                (CropStatus.Growing, CropStatus.ReadyForHarvest) => true,
                (CropStatus.ReadyForHarvest, CropStatus.Growing) => true, // perennes
                _ => false
            };
        }
        public void AddDevice(int gardenPlotId, Guid deviceId)
        {
            var gardenPlot = _gardenPlotRepository.GetById(gardenPlotId) 
                ?? throw new KeyNotFoundException("Garden plot not found");
            gardenPlot.Devices ??= new List<Device>();
            if (gardenPlot.Devices.Any(d => d.ID == deviceId)) throw new InvalidOperationException("Device already added to this garden plot");
            gardenPlot.Devices.Add(new Device { ID = deviceId });
            _gardenPlotRepository.Update(gardenPlot);
        }
        public void RemoveDevice(int gardenPlotId, Guid deviceId)
        {
            var gardenPlot = _gardenPlotRepository.GetById(gardenPlotId) 
                ?? throw new KeyNotFoundException("Garden plot not found");
            if (gardenPlot.Devices == null) throw new InvalidOperationException("No devices found in this garden plot");
            var removeDevice = gardenPlot.Devices.FirstOrDefault(d => d.ID == deviceId);
            if (removeDevice == null) throw new InvalidOperationException("Device not found in this garden plot");
            gardenPlot.Devices.Remove(removeDevice);
            _gardenPlotRepository.Update(gardenPlot);
        }
    }
}