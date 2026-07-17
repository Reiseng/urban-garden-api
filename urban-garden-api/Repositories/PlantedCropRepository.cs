using UrbanGarden.Api.Models.Entities;
using UrbanGarden.Api.Data;

namespace UrbanGarden.Api.Repositories
{
public class PlantedCropRepository : IPlantedCropRepository
{
    private readonly UrbanGardenDbContext _context;

    public PlantedCropRepository(UrbanGardenDbContext context)
    {
        _context = context;
    }

    public IEnumerable<PlantedCrop> GetAll()
    {
        return _context.PlantedCrops.ToList();
    }

    public PlantedCrop? GetById(int id)
    {
        return _context.PlantedCrops.FirstOrDefault(c => c.Id == id);
    }
    public void Add(PlantedCrop plantedCrop)
    {
        _context.PlantedCrops.Add(plantedCrop);
        _context.SaveChanges();
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
        _context.PlantedCrops.Remove(existing);
        _context.SaveChanges();
    }
}
}