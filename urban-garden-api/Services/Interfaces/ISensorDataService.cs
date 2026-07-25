using UrbanGarden.Api.Models.Dtos.Sensors;

namespace UrbanGarden.Api.Services
{
    /// <summary>
    /// Interfaz para el servicio de datos de sensores.
    /// </summary>
    public interface ISensorDataService
    {
        /// <summary>
        /// Guarda los datos de los sensores de humedad del suelo para un dispositivo específico.
        /// </summary>
        /// <param name="deviceId">El ID del dispositivo.</param>
        /// <param name="rawValues">La lista de valores brutos.</param>
        /// <param name="timestamp">La marca de tiempo.</param>
        void SaveSoilSensorData(Guid deviceId, List<double> rawValues, DateTime timestamp);
        /// <summary>
        /// Obtiene los datos más recientes de los sensores de humedad del suelo para un dispositivo específico.
        /// </summary>
        /// <param name="deviceId">El ID del dispositivo.</param>
        /// <returns>Datos de los sensores de humedad del suelo.</returns>
        List<LatestSoilSensorReadingsDto> GetLatestSoilSensorData(Guid deviceId);
        /// <summary>
        /// Obtiene los datos de los sensores de humedad del suelo para un dispositivo específico dentro de un rango de fechas y con un límite de resultados.
        /// </summary>
        /// <param name="deviceId">El ID del dispositivo.</param>
        /// <param name="from">La fecha de inicio.</param>
        /// <param name="to">La fecha de fin.</param>
        /// <param name="limit">El límite de resultados.</param>
        /// <returns>Lista de datos de los sensores de humedad del suelo.</returns>
        List<SoilSensorReadingsDto> GetSoilSensorData(Guid deviceId, DateTime? from, DateTime? to, int limit);

        /// <summary>
        /// Guarda los datos de los sensores de temperatura y humedad para un dispositivo específico.
        /// </summary>
        /// <param name="deviceId">El ID del dispositivo.</param>
        /// <param name="temperature">La temperatura.</param>
        /// <param name="humidity">La humedad.</param>
        /// <param name="timestamp">La marca de tiempo.</param>
        void SaveTemperatureSensorData(Guid deviceId, double temperature, double humidity, DateTime timestamp);
        /// <summary>
        /// Obtiene los datos más recientes de los sensores de temperatura y humedad para un dispositivo específico.
        /// </summary>
        /// <param name="deviceId">El ID del dispositivo.</param>
        /// <returns>Datos de los sensores de temperatura y humedad.</returns>
        TemperatureSensorReadingsDto GetLatestTemperatureSensorData(Guid deviceId);
        /// <summary>
        /// Obtiene los datos de los sensores de temperatura y humedad para un dispositivo específico dentro de un rango de fechas y con un límite de resultados.
        /// </summary>
        /// <param name="deviceId">El ID del dispositivo.</param>
        /// <param name="from">La fecha de inicio.</param>
        /// <param name="to">La fecha de fin.</param>
        /// <param name="limit">El límite de resultados.</param>
        /// <returns>Lista de datos de los sensores de temperatura y humedad.</returns>
        List<TemperatureSensorReadingsDto> GetTemperatureSensorData(Guid deviceId, DateTime? from, DateTime? to, int limit);
    }
}