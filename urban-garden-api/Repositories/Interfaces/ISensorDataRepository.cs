using UrbanGarden.Api.Models.Entities;

namespace UrbanGarden.Api.Repositories
{
    public interface ISensorDataRepository
    {
        void SaveSoilSensorData(Guid deviceId, List<double> MoistureValues, DateTime timestamp);
        List<SoilSensorReadings> GetLatestSoilSensorData(Guid deviceId);
        List<SoilSensorReadings> GetSoilSensorData(Guid deviceId, DateTime? from, DateTime? to, int limit);
        void SaveTemperatureSensorData(Guid deviceId, double temperature, double humidity, DateTime timestamp);
        TemperatureSensorReadings GetLatestTemperatureSensorData(Guid deviceId);
        List<TemperatureSensorReadings> GetTemperatureSensorData(Guid deviceId, DateTime? from, DateTime? to, int limit);
    }
}