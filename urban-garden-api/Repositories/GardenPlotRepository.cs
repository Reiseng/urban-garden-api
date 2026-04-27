using UrbanGarden.Api.Models.Entities;

namespace UrbanGarden.Api.Repositories
{
    public class GardenPlotRepository : IGardenPlotRepository
    {
        private readonly List<GardenPlot> _gardenPlots = new List<GardenPlot>();

        public IEnumerable<GardenPlot> GetAll()
        {
            return _gardenPlots;
        }

        public GardenPlot? GetById(int id)
        {
            return _gardenPlots.FirstOrDefault(gp => gp.ID == id);
        }

        public void Add(GardenPlot gardenPlot)
        {
            gardenPlot.ID = _gardenPlots.Count > 0 ? _gardenPlots.Max(gp => gp.ID) + 1 : 1;
            if (gardenPlot.ActiveCrop != null) gardenPlot.ActiveCrop.GardenPlotId = gardenPlot.ID;
            _gardenPlots.Add(gardenPlot);
        }

        public void Update(GardenPlot gardenPlot)
        {
            var existingGardenPlot = GetById(gardenPlot.ID);
            if (existingGardenPlot != null)
            {
                existingGardenPlot.Name = gardenPlot.Name;
                existingGardenPlot.Size = gardenPlot.Size;
                existingGardenPlot.Location = gardenPlot.Location;
                existingGardenPlot.ActiveCrop = gardenPlot.ActiveCrop;
            }
        }

        public void Delete(int id)
        {
            var gardenPlot = GetById(id);
            if (gardenPlot != null)
            {
                _gardenPlots.Remove(gardenPlot);
            }
        }
    }
}