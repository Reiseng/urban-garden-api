namespace UrbanGarden.Api.Models.Dtos.Sensors
{
    public class TemperatureSensorReadingsDto
    {
        /// <summary>
        /// Identificador del dispositivo al que pertenecen las lecturas del sensor de temperatura y humedad.
        /// </summary>
        public Guid DeviceID { get; set; }
        /// <summary>
        /// Nivel de temperatura.
        /// </summary>
        public double Temperature { get; set; }
        /// <summary>
        /// Nivel de humedad.
        /// </summary>
        public double Humidity { get; set; }
        /// <summary>
        /// Fecha y hora de la lectura.
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}