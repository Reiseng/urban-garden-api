using UrbanGarden.Api.Models.Entities;
using UrbanGarden.Api.Data;

namespace UrbanGarden.Api.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly UrbanGardenDbContext _context;

        public DeviceRepository(UrbanGardenDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Device> GetAll()
        {
            return _context.Devices.ToList();
        }

        public Device? GetById(Guid id)
        {
            return _context.Devices.FirstOrDefault(d => d.ID == id);
        }
        public Device? GetByMacAddress(string macAddress)
        {
            return _context.Devices.FirstOrDefault(d => d.MacAddress == macAddress);
        }

        public Device? Add(Device device)
        {
            device.GardenPlotId = null; // Inicializa GardenPlotId como null
            device.ID = Guid.NewGuid(); // Genera un ID único
            device.CreatedAt = DateTime.UtcNow;
            _context.Devices.Add(device);
            _context.SaveChanges();
            return device;
        }

        public void Update(Device device)
        {
            var existing = _context.Devices.FirstOrDefault(d => d.ID == device.ID);
            if (existing == null) return;
            existing.Name = device.Name;
            existing.LastSeenAt = device.LastSeenAt;
            existing.GardenPlotId = device.GardenPlotId;
            existing.GardenPlot = device.GardenPlot;
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var existing = _context.Devices.FirstOrDefault(d => d.ID == id);
            if (existing == null) return;
            _context.Devices.Remove(existing);
            _context.SaveChanges();
        }
    }
}