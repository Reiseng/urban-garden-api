using UrbanGarden.Api.Models.Dtos.Sensors;

namespace UrbanGarden.Api.Repositories
{
    public class SensorDataRepository : ISensorDataRepository
    {
        private readonly List<SoilSensorReadingsDto> _soilSensorData = new();
        private readonly List<TemperatureSensorReadingsDto> _temperatureSensorData = new();
        public void SaveSoilSensorData(Guid deviceId, List<double> MoistureValues, DateTime timestamp)
        {
            for (int i = 0; i < MoistureValues.Count; i++)
            {
                _soilSensorData.Add(new SoilSensorReadingsDto
                {
                    DeviceID = deviceId,
                    SensorIndex = i,
                    Moisture = MoistureValues[i],
                    Timestamp = timestamp
                });
            }
        }

        public SoilSensorReadingsDto GetLatestSoilSensorData(Guid deviceId)
        {
            return _soilSensorData.Where(d => d.DeviceID == deviceId)
                                  .OrderByDescending(d => d.Timestamp)
                                  .FirstOrDefault() ?? new SoilSensorReadingsDto();
        }

        public List<SoilSensorReadingsDto> GetSoilSensorData(Guid deviceId, DateTime? from, DateTime? to, int limit)
        {
            var query = _soilSensorData.Where(d => d.DeviceID == deviceId);

            if (from.HasValue)
            {
                query = query.Where(d => d.Timestamp >= from);
            }

            if (to.HasValue)
            {
                query = query.Where(d => d.Timestamp <= to);
            }

            return query.OrderByDescending(d => d.Timestamp)
                        .Take(limit)
                        .ToList();
        }

        public void SaveTemperatureSensorData(Guid deviceId, double temperature, double humidity, DateTime timestamp)
        {
            _temperatureSensorData.Add(new TemperatureSensorReadingsDto
            {
                DeviceID = deviceId,
                Temperature = temperature,
                Humidity = humidity,
                Timestamp = timestamp
            });
        }

        public TemperatureSensorReadingsDto GetLatestTemperatureSensorData(Guid deviceId)
        {
            return _temperatureSensorData.Where(d => d.DeviceID == deviceId)
                                          .OrderByDescending(d => d.Timestamp)
                                          .FirstOrDefault() ?? new TemperatureSensorReadingsDto();
        }

        public List<TemperatureSensorReadingsDto> GetTemperatureSensorData(Guid deviceId, DateTime? from, DateTime? to, int limit)
        {
            var query = _temperatureSensorData.Where(d => d.DeviceID == deviceId);

            if (from.HasValue)
            {
                query = query.Where(d => d.Timestamp >= from);
            }

            if (to.HasValue)
            {
                query = query.Where(d => d.Timestamp <= to);
            }

            return query.OrderByDescending(d => d.Timestamp)
                        .Take(limit)
                        .ToList();
        }
    }
}