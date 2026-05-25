using UrbanGarden.Api.Models.Dtos.Sensors;
using UrbanGarden.Api.Models.Entities;
using UrbanGarden.Api.Repositories;

namespace UrbanGarden.Api.Services
{
    public class SensorDataService : ISensorDataService
    {
        private readonly IDeviceService _deviceService;
        private readonly ISensorDataRepository _sensorDataRepository;
        public SensorDataService(IDeviceService deviceService, ISensorDataRepository sensorDataRepository)
        {
            _deviceService = deviceService;
            _sensorDataRepository = sensorDataRepository;
        }
        public void SaveSoilSensorData(Guid deviceId, List<double> rawValues, DateTime timestamp)
        {
            var device = ValidateDevice(deviceId);
            
            ValidateRawValues(rawValues);

            ValidateTimestamp(timestamp);

            _deviceService.UpdateLastSeen(device, DateTime.UtcNow);

            _sensorDataRepository.SaveSoilSensorData(deviceId, rawValues, timestamp);
        }
        public SoilSensorReadingsDto GetLatestSoilSensorData(Guid deviceId)
        {
            ValidateDevice(deviceId);
            return _sensorDataRepository.GetLatestSoilSensorData(deviceId);
        }
        public List<SoilSensorReadingsDto> GetSoilSensorData(Guid deviceId, DateTime? from, DateTime? to, int limit)
        {
            ValidateDevice(deviceId);
            if (from.HasValue && to.HasValue && from > to)
            {
                throw new ArgumentException("'from' cannot be greater than 'to'");
            }
            return _sensorDataRepository.GetSoilSensorData(deviceId, from, to, limit);
        }
        public void SaveTemperatureSensorData(Guid deviceId, double temperature, double humidity, DateTime timestamp)
        {
            var device = ValidateDevice(deviceId);

            ValidateTemperature(temperature);

            ValidateHumidity(humidity);
            
            ValidateTimestamp(timestamp);
            _deviceService.UpdateLastSeen(device, DateTime.UtcNow);

            _sensorDataRepository.SaveTemperatureSensorData(deviceId, temperature, humidity, timestamp);
        }
        public TemperatureSensorReadingsDto GetLatestTemperatureSensorData(Guid deviceId)
        {
            ValidateDevice(deviceId);
            return _sensorDataRepository.GetLatestTemperatureSensorData(deviceId);
        }
        public List<TemperatureSensorReadingsDto> GetTemperatureSensorData(Guid deviceId, DateTime? from, DateTime? to, int limit)
        {
            ValidateDevice(deviceId);
            if (from.HasValue && to.HasValue && from > to)
            {
                throw new ArgumentException("'from' cannot be greater than 'to'");
            }
            return _sensorDataRepository.GetTemperatureSensorData(deviceId, from, to, limit);
        }
        private Device ValidateDevice(Guid deviceID)
        {
            var existingDevice = _deviceService.GetById(deviceID);
            if (existingDevice == null)
            {
                throw new KeyNotFoundException($"Device with ID {deviceID} not found");
            }
            return existingDevice;
        }

        private static void ValidateRawValues(List<double> rawValues)
        {
            if (rawValues == null || rawValues.Count == 0)
            {
                throw new ArgumentException(
                    "Raw values cannot be null or empty");
            }

            if (rawValues.Any(v => v < 0 || v > 4095))
            {
                throw new ArgumentException(
                    "Raw values must be between 0 and 4095");
            }
        }
        private static void ValidateTimestamp(DateTime timestamp)
        {
            if (timestamp > DateTime.UtcNow)
            {
                throw new ArgumentException(
                    "Timestamp cannot be in the future");
            }

            if (timestamp < DateTime.UtcNow.AddDays(-30))
            {
                throw new ArgumentException(
                    "Timestamp cannot be older than 30 days");
            }
        }
        private static void ValidateTemperature(double temperature)
        {
            if (temperature < -50 || temperature > 100)
            {
                throw new ArgumentException(
                    "Temperature must be between -50 and 100 degrees Celsius");
            }
        }
        private static void ValidateHumidity(double humidity)
        {
            if (humidity < 0 || humidity > 100)
            {
                throw new ArgumentException(
                    "Humidity must be between 0 and 100%");
            }
        }
    }
}