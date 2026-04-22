using UrbanGarden.Api.Models.Entities;
using UrbanGarden.Api.Models.Enums;

namespace UrbanGarden.Api.Services
{
    public interface IPlantedCropService
    {
        IEnumerable<PlantedCrop> GetAll();
        PlantedCrop? GetById(int id);
        void Add(PlantedCrop plantedCrop);
        void Update(int id, CropStatus status);
        void Harvest(int id);
        void Delete(int id);
    }
}