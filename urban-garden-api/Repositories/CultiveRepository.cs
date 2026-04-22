using UrbanGarden.Api.Models.Entities;

namespace UrbanGarden.Api.Repositories
{
public class CultiveRepository : ICultiveRepository
{
    private readonly List<Cultive> _cultives = new();

    public IEnumerable<Cultive> GetAll()
    {
        return _cultives;
    }

    public Cultive? GetById(int id)
    {
        return _cultives.FirstOrDefault(c => c.ID == id);
    }

    public void Add(Cultive cultive)
    {
            cultive.ID = _cultives.Count > 0 ? _cultives.Max(c => c.ID) + 1 : 1;
        _cultives.Add(cultive);
    }

    public void Update(Cultive cultive)
    {
        var existing = GetById(cultive.ID);
        if (existing == null) return;

        existing.Name = cultive.Name;
        existing.Season = cultive.Season;
        existing.Disponible = cultive.Disponible;
    }

    public void Delete(int id)
    {
        var existing = GetById(id);
        if (existing == null) return;
        _cultives.Remove(existing);
    }
}
}