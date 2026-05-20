using System.Text.Json;
using UrbanGarden.Api.Infrastructure.MQTT.Dtos;
using UrbanGarden.Api.Models.Entities;
using UrbanGarden.Api.Services;

namespace UrbanGarden.Api.Infrastructure.MQTT.Services
{

    public class SensorsService : ISensorsService
    {
        private readonly IDeviceService _deviceService;

        public SensorsService(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        public async Task ProcessSoilSensorData(string topic, string payload)
        {
            var data = JsonSerializer.Deserialize<SoilSensorDto>(payload);
            var device = ValidateDevice(topic, payload);
            if (device == null) return;
            Console.WriteLine($"ID del dispositivo: {device.ID} Humedad: {data?.Humidity} Fecha: {data?.Timestamp}");
            return;
        }

        public async Task ProcessTemperatureSensorData(string topic, string payload)
        {
            var data = JsonSerializer.Deserialize<TemperatureSensorDto>(payload);
            var device = ValidateDevice(topic, payload);
            if (device == null) return;
            Console.WriteLine($"ID del dispositivo: {device.ID} Temperatura: {data?.Temperature} Humedad Ambiente: {data?.Humidity} Fecha: {data?.Timestamp}");
            return;
        }

        private Device? ValidateDevice(string topic, string payload)
        {
            var parts = topic.Split('/');

            if (parts.Length < 4)
                return null;

            if (!Guid.TryParse(parts[1], out var deviceId))
                return null;

            var device = _deviceService.GetById(deviceId);

            if (device == null)
                return null;

            var baseDto =
                JsonSerializer.Deserialize<BaseSensorDto>(payload);

            if (baseDto?.ApiKey != device.ApiKey)
                return null;

            return device;
        }
    }
}