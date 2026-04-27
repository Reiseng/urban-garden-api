using UrbanGarden.Api.Models.Entities;

namespace UrbanGarden.Api.Repositories
{
    public class HarvestRepository : IHarvestRepository
    {
        private readonly List<Harvest> _harvests = new();

        public IEnumerable<Harvest> GetAll()
        {
            return _harvests;
        }

        public Harvest? GetById(int id)
        {
            return _harvests.FirstOrDefault(h => h.Id == id);
        }

        public IEnumerable<Harvest> GetAllByPlotId(int id)
        {
            return _harvests.Where(h => h.GardenPlotId == id);
        }

        public void Add(Harvest harvest)
        {
            harvest.Id = _harvests.Count > 0 ? _harvests.Max(h => h.Id) + 1 : 1;
            _harvests.Add(harvest);
        }
    }
}