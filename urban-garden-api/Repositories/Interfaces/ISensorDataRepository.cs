using UrbanGarden.Api.Models.Dtos.Sensors;

namespace UrbanGarden.Api.Repositories
{
    public interface ISensorDataRepository
    {
        void SaveSoilSensorData(Guid deviceId, List<double> MoistureValues, DateTime timestamp);
        SoilSensorReadingsDto GetLatestSoilSensorData(Guid deviceId);
        List<SoilSensorReadingsDto> GetSoilSensorData(Guid deviceId, DateTime? from, DateTime? to, int limit);
        void SaveTemperatureSensorData(Guid deviceId, double temperature, double humidity, DateTime timestamp);
        TemperatureSensorReadingsDto GetLatestTemperatureSensorData(Guid deviceId);
        List<TemperatureSensorReadingsDto> GetTemperatureSensorData(Guid deviceId, DateTime? from, DateTime? to, int limit);
    }
}