using UrbanGarden.Api.Models.Entities;

namespace UrbanGarden.Api.Repositories
{
public class CropTypeRepository : ICropTypeRepository
{
    private readonly List<CropType> _crops = new();

    public IEnumerable<CropType> GetAll()
    {
        return _crops;
    }

    public CropType? GetById(int id)
    {
        return _crops.FirstOrDefault(c => c.ID == id);
    }

    public void Add(CropType cropType)
    {
        cropType.ID = _crops.Count > 0 ? _crops.Max(c => c.ID) + 1 : 1;
        _crops.Add(cropType);
    }

    public void Update(CropType cropType)
    {
        var existing = GetById(cropType.ID);
        if (existing == null) return;

        existing.Name = cropType.Name;
        existing.Season = cropType.Season;
        existing.Disponible = cropType.Disponible;
    }

    public void Delete(int id)
    {
        var existing = GetById(id);
        if (existing == null) return;
        _crops.Remove(existing);
    }
}
}