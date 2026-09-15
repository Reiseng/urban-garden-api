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
            Console.WriteLine("1 - Entrando a GetAll");

            var devices = _context.Devices.ToList();

            Console.WriteLine($"2 - Devices encontrados: {devices.Count}");

            return devices;
        }

        public Device? GetById(Guid id)
        {
            Console.WriteLine($"1 - GetById: {id}");

            var device = _context.Devices.FirstOrDefault(d => d.ID == id);

            Console.WriteLine($"2 - Resultado: {device != null}");

            return device;
        }
        public Device? GetByMacAddress(string macAddress)
        {
            Console.WriteLine("1 - Entrando a GetByMacAddress");

            var device = _context.Devices.FirstOrDefault(d => d.MacAddress == macAddress);

            Console.WriteLine($"2 - Device encontrado: {device?.ID}");

            return device;
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