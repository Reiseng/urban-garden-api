using System.Text.Json;
using UrbanGarden.Api.Infrastructure.MQTT.Dtos;
using UrbanGarden.Api.Models.Entities;
using UrbanGarden.Api.Services;

namespace UrbanGarden.Api.Infrastructure.MQTT.Services
{

    public class SensorsService : ISensorsService
    {
        private readonly IDeviceService _deviceService;
        private readonly ISensorDataService _sensorDataService;

        public SensorsService(IDeviceService deviceService, ISensorDataService sensorDataService)
        {
            _deviceService = deviceService;
            _sensorDataService = sensorDataService;
        }

        public Task ProcessSoilSensorData(string topic, string payload)
        {
            var data = JsonSerializer.Deserialize<SoilSensorDto>(payload);
            var device = ValidateDevice(topic, payload);
            if (device == null) return Task.CompletedTask;
            Console.WriteLine($"ID del dispositivo: {device.ID} Humedad Raw: {string.Join(", ", data?.RawValues ?? [])} Fecha: {data?.Timestamp}");
            var timestamp = data?.Timestamp ?? DateTime.UtcNow;
            timestamp = DateTime.SpecifyKind(timestamp, DateTimeKind.Utc);
            _sensorDataService.SaveSoilSensorData(device.ID, data?.RawValues ?? new List<double>(), timestamp);
            return Task.CompletedTask;
        }

        public Task ProcessTemperatureSensorData(string topic, string payload)
        {
            var data = JsonSerializer.Deserialize<TemperatureSensorDto>(payload);
            var device = ValidateDevice(topic, payload);
            if (device == null) return Task.CompletedTask;
            Console.WriteLine($"ID del dispositivo: {device.ID} Temperatura: {data?.Temperature} Humedad Ambiente: {data?.Humidity} Fecha: {data?.Timestamp}");
            var timestamp = data?.Timestamp ?? DateTime.UtcNow;
            timestamp = DateTime.SpecifyKind(timestamp, DateTimeKind.Utc);
            _sensorDataService.SaveTemperatureSensorData(device.ID, data?.Temperature ?? 0, data?.Humidity ?? 0, timestamp);
            return Task.CompletedTask;
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