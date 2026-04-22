using UrbanGarden.Api.Models.Entities;
using UrbanGarden.Api.Models.Enums;

namespace UrbanGarden.Api.Services
{
    public class PlantedCropService : IPlantedCropService
    {
        private readonly IPlantedCropRepository _repository;
        private readonly IHarvestRepository _harvestRepository;

        public PlantedCropService(IPlantedCropRepository repository, IHarvestRepository harvestRepository)
        {
            _repository = repository;
            _harvestRepository = harvestRepository;
        }

        public IEnumerable<PlantedCrop> GetAll()
        {
            return _repository.GetAll();
        }

        public PlantedCrop? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Add(PlantedCrop plantedCrop)
        {
            _repository.Add(plantedCrop);
        }

        public void Update(int id, CropStatus status)
        {
            var plantedCrop = _repository.GetById(id);
            if (plantedCrop == null) throw new KeyNotFoundException("PlantedCrop not found");
            plantedCrop.State = status;
            _repository.Update(plantedCrop);
        }
        public void Harvest(int id)
        {
            var plantedCrop = _repository.GetById(id);
            if (plantedCrop == null) throw new KeyNotFoundException();

            var cropType = _repository.GetCropTypeById(plantedCrop.CropTypeId);

            if (plantedCrop.State != CropStatus.ReadyForHarvest)
                throw new InvalidOperationException();

            _harvestRepository.Add(new Harvest
            {
                PlantedCropId = plantedCrop.Id,
                CropTypeId = plantedCrop.CropTypeId,
                Quantity = 1.0m, // This is a stub. In a real implementation, this would be calculated based on the crop type and growth conditions.
                Date = DateTime.UtcNow
            });

            if (cropType?.IsPerennial == true)
            {
                plantedCrop.State = CropStatus.Growing;
                _repository.Update(plantedCrop);
            }
            else
            {
                _repository.Delete(id);
            }
        }
        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}