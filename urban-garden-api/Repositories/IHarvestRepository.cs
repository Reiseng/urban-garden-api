using UrbanGarden.Api.Models.Entities;
public interface IHarvestRepository
{
    IEnumerable<Harvest> GetAll();
    Harvest? GetById(int id);
    void Add(Harvest harvest);
}