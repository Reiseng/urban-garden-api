namespace UrbanGarden.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using UrbanGarden.Api.Models.Dtos;
    using UrbanGarden.Api.Models.Entities;
    using UrbanGarden.Api.Models.Enums;
    using UrbanGarden.Api.Services;

    /// <summary>
    /// Controlador para gestionar operaciones CRUD de dispositivos.
    /// </summary>
    /// <remarks>
    /// Este controlador permite registrar nuevos dispositivos, obtener información de los dispositivos registrados y actualizar la información de los dispositivos existentes.
    /// </remarks>
    [ApiController]
    [Route("api/{version}/devices")]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _deviceService;

        /// <summary>
        /// Constructor del controlador de dispositivos.
        /// </summary>
        /// <param name="deviceService">Servicio de dispositivos.</param>
        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        /// <summary>
        /// Obtiene la lista de todos los dispositivos registrados.
        /// </summary>
        /// <returns>Lista de dispositivos.</returns>
        /// <response code="200">Lista de dispositivos obtenida exitosamente.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpGet]
        public IActionResult GetAll()
        {
            var devices = _deviceService.GetAll();
            var devicesDto = devices.Select(d => new DeviceDto
            {
                ID = d.ID,
                Name = d.Name,
                CreatedAt = d.CreatedAt,
                LastSeenAt = d.LastSeenAt
            }).ToList();
            return Ok(devicesDto);
        }

        /// <summary>
        /// Obtiene la información de un dispositivo por su ID.
        /// </summary>
        /// <param name="id">ID del dispositivo.</param>
        /// <returns>Información del dispositivo.</returns>
        /// <response code="200">Información del dispositivo obtenida exitosamente.</response>
        /// <response code="404">Dispositivo no encontrado.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var device = _deviceService.GetById(id);
            if (device == null)
            {
                return NotFound();
            }
            var deviceDto = new DeviceDto
            {
                ID = device.ID,
                Name = device.Name,
                CreatedAt = device.CreatedAt,
                LastSeenAt = device.LastSeenAt
            };
            return Ok(deviceDto);
        }

        /// <summary>
        /// Registra un nuevo dispositivo en el sistema.
        /// </summary>
        /// <param name="device">Información del dispositivo a registrar.</param>
        /// <returns>Información del dispositivo registrado, incluyendo su API Key.</returns>
        /// <response code="201">Dispositivo registrado exitosamente.</response>
        /// <response code="400">Solicitud inválida, por ejemplo, si la información del dispositivo es incorrecta.</response>
        /// <response code="401">Acceso no autorizado, por ejemplo, si la API Key es inválida.</response>
        /// <response code="500">Error interno del servidor al procesar la solicitud.</response>
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDeviceDto device)
        {
            try
            {
                var result = _deviceService.Add(device);
                return CreatedAtAction(nameof(GetById), new {version = RouteData.Values["version"], id = result.ID }, result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}