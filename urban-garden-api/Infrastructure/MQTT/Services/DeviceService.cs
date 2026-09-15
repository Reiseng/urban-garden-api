using UrbanGarden.Api.Infrastructure.MQTT.Dtos;
using UrbanGarden.Api.Models.Dtos;
using UrbanGarden.Api.Models.Entities;
using UrbanGarden.Api.Services;

namespace UrbanGarden.Api.Infrastructure.MQTT.Services
{
    public class DeviceServiceMQTT : IDeviceServiceMQTT
    {
        private readonly IDeviceService _deviceService;
        private readonly IMqttService _mqttService;

        public DeviceServiceMQTT(IDeviceService deviceService, IMqttService mqttService)
        {
            _deviceService = deviceService;
            _mqttService = mqttService;
        }

        public async Task<Device> GetDeviceById(Guid deviceId)
        {
            var device = _deviceService.GetById(deviceId);
            if (device == null)
            {
                throw new ArgumentException($"Device with ID {deviceId} not found.");
            }
            return device;
        }

        public void UpdateLastSeen(Device device, DateTime lastSeen)
        {
            _deviceService.UpdateLastSeen(device, lastSeen);
        }

        public async Task SendCommandToDeviceAsync(Guid deviceId, CommandDto command)
        {
            string topic = $"devices/{deviceId}/commands";
            string payload = System.Text.Json.JsonSerializer.Serialize(command);
            await _mqttService.Publish(topic, payload);
        }

        public async Task UpdateDeviceConfiguration(Guid deviceId, ConfigDto configuration)
        {
            var device = await GetDeviceById(deviceId);
            UpdateDeviceDto deviceDto = new UpdateDeviceDto
            {

                Name = device.Name,
                Config = new ConfigDeviceDto
                {
                    SoilSensorCount = configuration.SoilSensorCount,
                    SoilMoistureInterval = configuration.SoilMoistureInterval,
                    TemperatureInterval = configuration.TemperatureInterval,
                    KeepAliveInterval = configuration.KeepAliveInterval
                }
            };

            _deviceService.Update(device.ID, deviceDto);

            // Send the updated configuration to the device via MQTT
            string topic = $"devices/{device.ID}/config";
            string payload = System.Text.Json.JsonSerializer.Serialize(configuration);
            await _mqttService.Publish(topic, payload);
        }
    }
}