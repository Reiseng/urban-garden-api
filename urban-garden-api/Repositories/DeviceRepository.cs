using UrbanGarden.Api.Models.Entities;

namespace UrbanGarden.Api.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly List<Device> _devices = new();

        public IEnumerable<Device> GetAll()
        {
            return _devices;
        }

        public Device? GetById(Guid id)
        {
            return _devices.FirstOrDefault(d => d.ID == id);
        }
        public Device? GetByMacAddress(string macAddress)
        {
            return _devices.FirstOrDefault(d => d.MacAddress == macAddress);
        }

        public Device? Add(Device device)
        {
            device.ID = Guid.NewGuid(); // Genera un ID único
            device.CreatedAt = DateTime.UtcNow;
            _devices.Add(device);
            return device;
        }

        public void Update(Device device)
        {
            var existing = GetByMacAddress(device.MacAddress);
            if (existing == null) return;

            existing.Name = device.Name;
            existing.ApiKey = device.ApiKey;
        }

        public void Delete(Guid id)
        {
            var existing = GetById(id);
            if (existing == null) return;
            _devices.Remove(existing);
        }
    }
}