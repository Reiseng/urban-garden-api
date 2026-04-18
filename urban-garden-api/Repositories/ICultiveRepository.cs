using UrbanGarden.Api.Models.Entities;
public interface ICultiveRepository
{
    IEnumerable<Cultive> GetAll();
    Cultive? GetById(int id);
    void Add(Cultive cultive);
    void Update(Cultive cultive);
    void Delete(int id);
}