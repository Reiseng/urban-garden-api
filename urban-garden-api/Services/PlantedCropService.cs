using UrbanGarden.Api.Models.Entities;
using UrbanGarden.Api.Models.Enums;

namespace UrbanGarden.Api.Services
{
    public class PlantedCropService : IPlantedCropService
    {
        private readonly IPlantedCropRepository _repository;

        public PlantedCropService(IPlantedCropRepository repository)
        {
            _repository = repository;
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
            if (plantedCrop == null) throw new KeyNotFoundException("PlantedCrop not found");

            var cropType = _repository.GetCropTypeById(plantedCrop.CropTypeId);

            switch (plantedCrop.State)
            {
                case CropStatus.Planted:
                case CropStatus.Growing:
                    throw new InvalidOperationException("Crop is not ready for harvest");

                case CropStatus.ReadyForHarvest:

                    if (cropType?.IsPerennial == true)
                    {
                        plantedCrop.State = CropStatus.Growing;
                        _repository.Update(plantedCrop);
                    }
                    else
                    {
                        _repository.Delete(id);
                    }
                    break;

                case CropStatus.Withered:
                    throw new InvalidOperationException("Crop is withered and cannot be harvested");
            }
        }
        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}