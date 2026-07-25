using Microsoft.AspNetCore.Mvc;
using UrbanGarden.Api.Services;

namespace UrbanGarden.Api.Controllers
{
    /// <summary>
    ///  Controlador para consultar los datos de los sensores de los dispositivos.
    /// </summary>
    [ApiController]
    [Route("api/{version}/devices/{deviceId}/sensors")]
    public class SensorDataController : ControllerBase
    {
        private readonly ISensorDataService _sensorDataService;

        /// <summary>
        /// Constructor del controlador de datos de sensores.
        /// </summary>
        /// <param name="sensorDataService">El servicio de datos de sensores.</param>
        public SensorDataController(ISensorDataService sensorDataService)
        {
            _sensorDataService = sensorDataService;
        }

        /// <summary>
        /// Obtiene los datos más recientes de los sensores de humedad del suelo para un dispositivo específico.
        /// </summary>
        /// <param name="deviceId">El ID del dispositivo.</param>
        /// <returns>Los ultimos datos de los sensores de humedad del suelo.</returns>
        /// <response code="200">Datos de los sensores de humedad del suelo obtenidos exitosamente.</response>
        /// <response code="400">Solicitud inválida, por ejemplo, si el ID del dispositivo no es válido.</response>
        /// <response code="404">No se encontraron datos para el dispositivo especificado.</response>
        /// <response code="500">Error interno del servidor al procesar la solicitud.</response>
        [HttpGet("soil/latest")]
        public IActionResult GetLatestSoilSensorData(Guid deviceId)
        {
            try
            {
                var data = _sensorDataService.GetLatestSoilSensorData(deviceId);
                if (data == null)
                {
                    return NoContent();
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Obtiene los datos de los sensores de humedad del suelo para un dispositivo específico dentro de un rango de fechas y con un límite de resultados.
        /// </summary>
        /// <param name="deviceId">El ID del dispositivo.</param>
        /// <param name="from">La fecha de inicio.</param>
        /// <param name="to">La fecha de fin.</param>
        /// <param name="limit">El límite de resultados (opcional, por defecto 100).</param>
        /// <returns>Lista de datos de los sensores de humedad del suelo.</returns>
        /// <response code="200">Datos de los sensores de humedad del suelo obtenidos exitosamente.</response>
        /// <response code="400">Solicitud inválida, por ejemplo, si el ID del dispositivo no es válido, si el límite no está en el rango permitido o si las fechas son inconsistentes.</response>
        /// <response code="404">No se encontraron datos para el dispositivo especificado.</response>
        /// <response code="500">Error interno del servidor al procesar la solicitud.</response>
        [HttpGet("soil")]
        public IActionResult GetSoilSensorData(
            Guid deviceId,
            DateTime? from,
            DateTime? to,
            int limit = 100)
        {
            try
            {
                if (limit <= 0 || limit > 1000)
                {
                    return BadRequest(
                        "Limit must be between 1 and 1000");
                }

                if (from.HasValue && to.HasValue &&
                    from > to)
                {
                    return BadRequest(
                        "'from' cannot be greater than 'to'");
                }

                var data = _sensorDataService
                    .GetSoilSensorData(
                        deviceId,
                        from,
                        to,
                        limit);

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Obtiene los datos más recientes de los sensores de temperatura y humedad para un dispositivo específico
        /// </summary>
        /// <param name="deviceId">El ID del dispositivo.</param>
        /// <returns>Los ultimos datos de los sensores de temperatura y humedad.</returns>
        /// <response code="200">Datos de los sensores de temperatura y humedad obtenidos exitosamente.</response>
        /// <response code="400">Solicitud inválida, por ejemplo, si el ID del dispositivo no es válido.</response>
        /// <response code="404">No se encontraron datos para el dispositivo especificado.</response>
        /// <response code="500">Error interno del servidor al procesar la solicitud.</response>
        [HttpGet("temperature/latest")]
        public IActionResult GetLatestTemperatureSensorData(Guid deviceId)
        {
            try
            {
                var data = _sensorDataService.GetLatestTemperatureSensorData(deviceId);
                if (data == null)
                {
                    return NoContent();
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene los datos de los sensores de temperatura y humedad para un dispositivo específico dentro de un rango de fechas y con un límite de resultados.
        /// </summary>
        /// <param name="deviceId">El ID del dispositivo.</param>
        /// <param name="from">La fecha de inicio.</param>
        /// <param name="to">La fecha de fin.</param>
        /// <param name="limit">El límite de resultados (opcional, por defecto 100).</param>
        /// <returns>Lista de datos de los sensores de temperatura y humedad.</returns>
        /// <response code="200">Datos de los sensores de temperatura y humedad obtenidos exitosamente.</response>
        /// <response code="400">Solicitud inválida, por ejemplo, si el ID del
        /// dispositivo no es válido, si el límite no está en el rango permitido o si las fechas son inconsistentes.</response>
        /// <response code="404">No se encontraron datos para el dispositivo especificado.</response>
        /// <response code="500">Error interno del servidor al procesar la solicitud.</response>
        [HttpGet("temperature")]
        public IActionResult GetTemperatureSensorData(
            Guid deviceId,
            DateTime? from,
            DateTime? to,
            int limit = 100)
        {
            try
            {
                if (limit <= 0 || limit > 1000)
                {
                    return BadRequest(
                        "Limit must be between 1 and 1000");
                }

                if (from.HasValue && to.HasValue &&
                    from > to)
                {
                    return BadRequest(
                        "'from' cannot be greater than 'to'");
                }

                var data = _sensorDataService.GetTemperatureSensorData(deviceId, from, to, limit);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}