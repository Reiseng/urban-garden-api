using UrbanGarden.Api.Models.Entities;
using UrbanGarden.Api.Data;

namespace UrbanGarden.Api.Repositories
{
    public class SensorDataRepository : ISensorDataRepository
    {
    private readonly UrbanGardenDbContext _context;

    public SensorDataRepository(UrbanGardenDbContext context)
    {
        _context = context;
    }
    public void SaveSoilSensorData(Guid deviceId, List<double> moistureValues, DateTime timestamp)
    {
        var readings = new List<SoilSensorReadings>();

        for (int i = 0; i < moistureValues.Count; i++)
        {
            readings.Add(new SoilSensorReadings
            {
                DeviceID = deviceId,
                SensorIndex = i,
                Moisture = moistureValues[i],
                Timestamp = timestamp
            });
        }

        _context.SoilSensorReadings.AddRange(readings);
        _context.SaveChanges();
    }
    public SoilSensorReadings? GetLatestSoilSensorData(Guid deviceId)
    {
        return _context.SoilSensorReadings
            .Where(d => d.DeviceID == deviceId)
            .OrderByDescending(d => d.Timestamp)
            .FirstOrDefault();
    }
    public List<SoilSensorReadings> GetSoilSensorData(Guid deviceId, DateTime? from, DateTime? to, int limit)
    {
        var query = _context.SoilSensorReadings
            .Where(d => d.DeviceID == deviceId);

        if(from.HasValue)
        {
            query = query.Where(d => d.Timestamp >= from.Value);
        }


        if(to.HasValue)
        {
            query = query.Where(d => d.Timestamp <= to.Value);
        }

        return query
            .OrderByDescending(d => d.Timestamp)
            .Take(limit)
            .ToList();
    }
    public void SaveTemperatureSensorData(Guid deviceId, double temperature, double humidity, DateTime timestamp)
    {
        var reading = new TemperatureSensorReadings
        {
            DeviceID = deviceId,
            Temperature = temperature,
            Humidity = humidity,
            Timestamp = timestamp
        };

        _context.TemperatureSensorReadings.Add(reading);
        _context.SaveChanges();
    }

    public TemperatureSensorReadings? GetLatestTemperatureSensorData(Guid deviceId)
    {
        return _context.TemperatureSensorReadings
            .Where(d => d.DeviceID == deviceId)
            .OrderByDescending(d => d.Timestamp)
            .FirstOrDefault();
    }

        public List<TemperatureSensorReadings> GetTemperatureSensorData(Guid deviceId, DateTime? from, DateTime? to, int limit)
        {
            var query = _context.TemperatureSensorReadings.Where(d => d.DeviceID == deviceId);

            if (from.HasValue)
            {
                query = query.Where(d => d.Timestamp >= from.Value);
            }

            if (to.HasValue)
            {
                query = query.Where(d => d.Timestamp <= to.Value);
            }

            return query.OrderByDescending(d => d.Timestamp)
                        .Take(limit)
                        .ToList();
        }
    }
}