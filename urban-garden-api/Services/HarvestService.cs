using UrbanGarden.Api.Models.Entities;

namespace UrbanGarden.Api.Services
{
    public class HarvestService : IHarvestService
    {
        private readonly IHarvestRepository _harvestRepository;

        public HarvestService(IHarvestRepository harvestRepository)
        {
            _harvestRepository = harvestRepository;
        }
        public IEnumerable<Harvest> GetAll()
        {
            return _harvestRepository.GetAll();
        }
        public Harvest? GetById(int id)
        {
            return _harvestRepository.GetById(id);
        }
        public IEnumerable<Harvest> GetAllByPlotId(int id)
        {
            return _harvestRepository.GetAllByPlotId(id);
        }
    }
}