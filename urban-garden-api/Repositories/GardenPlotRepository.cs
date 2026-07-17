using UrbanGarden.Api.Models.Entities;
using UrbanGarden.Api.Data;

namespace UrbanGarden.Api.Repositories
{
    public class GardenPlotRepository : IGardenPlotRepository
    {
        private readonly UrbanGardenDbContext _context;

        public GardenPlotRepository(UrbanGardenDbContext context)
        {
            _context = context;
        }

        public IEnumerable<GardenPlot> GetAll()
        {
            return _context.GardenPlots.ToList();
        }

        public GardenPlot? GetById(int id)
        {
            return _context.GardenPlots.FirstOrDefault(gp => gp.ID == id);
        }

        public void Add(GardenPlot gardenPlot)
        {
            if (gardenPlot.PlantedCrops != null && gardenPlot.PlantedCrops.Count > 0)
            {
                foreach (var c in gardenPlot.PlantedCrops)
                    {
                        c.GardenPlotId = gardenPlot.ID;
                    }
            }
            _context.GardenPlots.Add(gardenPlot);
            _context.SaveChanges();
        }

        public void Update(GardenPlot gardenPlot)
        {
            var existingGardenPlot = GetById(gardenPlot.ID);
            if (existingGardenPlot != null)
            {
                existingGardenPlot.Name = gardenPlot.Name;
                existingGardenPlot.Size = gardenPlot.Size;
                existingGardenPlot.Location = gardenPlot.Location;
                existingGardenPlot.PlantedCrops = gardenPlot.PlantedCrops;
            }
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var gardenPlot = GetById(id);
            if (gardenPlot != null)
            {
                _context.GardenPlots.Remove(gardenPlot);
                _context.SaveChanges();
            }
        }
    }
}