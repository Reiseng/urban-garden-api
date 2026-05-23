namespace UrbanGarden.Api.Models.Dtos.Sensors
{
    public class SoilSensorReadingsDto
    {
        /// <summary>
        /// Identificador del dispositivo al que pertenecen las lecturas del sensor de humedad del suelo.
        /// </summary>
        public Guid DeviceID { get; set; }
        /// <summary>
        /// Índice del sensor de humedad del suelo, en caso de que el dispositivo tenga múltiples
        /// sensores de este tipo.
        /// </summary>
        public int SensorIndex { get; set; }
        /// <summary>
        /// Nivel de humedad del suelo.
        /// </summary>
        public double Moisture { get; set; }
        /// <summary>
        /// Fecha y hora de la lectura.
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}