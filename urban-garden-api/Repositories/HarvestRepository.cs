using UrbanGarden.Api.Models.Entities;
using UrbanGarden.Api.Data;

namespace UrbanGarden.Api.Repositories
{
    public class HarvestRepository : IHarvestRepository
    {
        private readonly UrbanGardenDbContext _context;

        public HarvestRepository(UrbanGardenDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Harvest> GetAll()
        {
            return _context.Harvests.ToList();
        }

        public Harvest? GetById(int id)
        {
            return _context.Harvests.FirstOrDefault(h => h.Id == id);
        }

        public IEnumerable<Harvest> GetAllByPlotId(int id)
        {
            return _context.Harvests.Where(h => h.PlantedCrop.GardenPlotId == id);
        }

        public void Add(Harvest harvest)
        {
            _context.Harvests.Add(harvest);
            _context.SaveChanges();
        }
    }
}