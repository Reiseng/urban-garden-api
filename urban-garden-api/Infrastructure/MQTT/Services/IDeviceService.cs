using UrbanGarden.Api.Infrastructure.MQTT.Dtos;
using UrbanGarden.Api.Models.Entities;

namespace UrbanGarden.Api.Infrastructure.MQTT.Services
{
    public interface IDeviceServiceMQTT
    {
        public Task<Device> GetDeviceById(Guid deviceId);
        public void UpdateLastSeen(Device device, DateTime lastSeen);
        Task SendCommandToDeviceAsync(Guid deviceId, CommandDto command);

        Task UpdateDeviceConfiguration(
            Guid deviceId,
            ConfigDto configuration);
    }
}