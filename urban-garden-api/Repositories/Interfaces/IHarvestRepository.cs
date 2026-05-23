using UrbanGarden.Api.Models.Entities;
public interface IHarvestRepository
{
    IEnumerable<Harvest> GetAll();
    Harvest? GetById(int id);
    IEnumerable<Harvest> GetAllByPlotId(int id);
    void Add(Harvest harvest);
}