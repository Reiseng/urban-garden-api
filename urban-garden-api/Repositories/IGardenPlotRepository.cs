using UrbanGarden.Api.Models.Entities;

namespace UrbanGarden.Api.Repositories
{
    public interface IGardenPlotRepository
    {
        IEnumerable<GardenPlot> GetAll();
        GardenPlot? GetById(int id);
        void Add(GardenPlot gardenPlot);
        void Update(GardenPlot gardenPlot);
        void Delete(int id);
    }
}