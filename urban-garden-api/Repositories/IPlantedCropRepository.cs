using UrbanGarden.Api.Models.Entities;
public interface IPlantedCropRepository
{
    IEnumerable<PlantedCrop> GetAll();
    PlantedCrop? GetById(int id);
    CropType? GetCropTypeById(int id);
    void Add(PlantedCrop plantedCrop);
    void Update(PlantedCrop plantedCrop);
    void Delete(int id);
}