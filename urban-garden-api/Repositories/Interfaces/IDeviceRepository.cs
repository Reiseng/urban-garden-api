using UrbanGarden.Api.Models.Entities;
namespace UrbanGarden.Api.Repositories
{
    public interface IDeviceRepository
    {
        IEnumerable<Device> GetAll();
        Device? GetById(Guid id);
        Device? GetByMacAddress(string macAddress);
        Device? Add(Device device);
        void Update(Device device);
        void Delete(Guid id);
    }
}