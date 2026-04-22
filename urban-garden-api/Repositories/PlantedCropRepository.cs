using UrbanGarden.Api.Models.Entities;

namespace UrbanGarden.Api.Repositories
{
public class PlantedCropRepository : IPlantedCropRepository
{
    private readonly List<PlantedCrop> _plantedCrops = new();

    public IEnumerable<PlantedCrop> GetAll()
    {
        return _plantedCrops;
    }

    public PlantedCrop? GetById(int id)
    {
        return _plantedCrops.FirstOrDefault(c => c.Id == id);
    }
    public CropType? GetCropTypeById(int id)
    {
        // This is a stub. In a real implementation, this would query the database or another data source.
        return new CropType { ID = id, Name = $"CropType{id}", Season = Models.Enums.Season.YearRound, IsPerennial = false };
    }
    public void Add(PlantedCrop plantedCrop)
    {
        plantedCrop.Id = _plantedCrops.Count > 0 ? _plantedCrops.Max(c => c.Id) + 1 : 1;
        _plantedCrops.Add(plantedCrop);
    }

    public void Update(PlantedCrop plantedCrop)
    {
        var existing = GetById(plantedCrop.Id);
        if (existing == null) return;

        existing.CropTypeId = plantedCrop.CropTypeId;
        existing.PlantedAt = plantedCrop.PlantedAt;
        existing.State = plantedCrop.State;
    }

    public void Delete(int id)
    {
        var existing = GetById(id);
        if (existing == null) return;
        _plantedCrops.Remove(existing);
    }
}
}