using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Mvc;
using UrbanGarden.Api.Models.Dtos;
using UrbanGarden.Api.Models.Entities;
using UrbanGarden.Api.Services;

namespace UrbanGarden.Api.Controllers
{
    /// <summary>
    /// Controlador para gestionar operaciones de huertos urbanos.
    /// </summary>
    [ApiController]
    [Route("api/{version}/plots")]
    public class GardenPlotsController : ControllerBase
    {
        private readonly IGardenPlotService _gardenPlotService;
        /// <summary>
        /// Constructor del controlador de huertos.
        /// </summary>
        /// <param name="gardenPlotService">Servicio de huertos.</param>
        public GardenPlotsController(IGardenPlotService gardenPlotService)
        {
            _gardenPlotService = gardenPlotService;
        }

        /// <summary>
        /// Obtiene todos los huertos urbanos disponibles.
        /// </summary>
        /// <returns>Lista de huertos.</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var gardenPlots = _gardenPlotService.GetAll();
            var gardenPlotsDto = gardenPlots.Select(g => new GardenPlotDto
            {
                ID = g.ID,
                Name = g.Name,
                Size = g.Size,
                Location = new DirectionDto{
                    City = g.Location.City,
                    ZipCode = g.Location.ZipCode,
                    Street = g.Location.Street,
                    State = g.Location.State
                    },
                ActiveCrop = g.ActiveCrop != null
                ? new PlantedCropDto
                {
                    Id = g.ActiveCrop.Id,
                    CropTypeId = g.ActiveCrop.CropTypeId,
                    PlantedAt = g.ActiveCrop.PlantedAt,
                    State = g.ActiveCrop.State
                }: null
            });
            return Ok(gardenPlotsDto);
        }

        /// <summary>
        /// Obtiene un huerto específico por su ID.
        /// </summary>
        /// <param name="id">ID del huerto.</param>
        /// <returns>El huerto encontrado o 404 si no existe.</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var gardenPlot = _gardenPlotService.GetById(id);
            var gardenPlotDto = gardenPlot != null 
            ? new GardenPlotDto
            {
                ID = gardenPlot.ID,
                Name = gardenPlot.Name,
                Size = gardenPlot.Size,
                Location = new DirectionDto{
                    City = gardenPlot.Location.City,
                    ZipCode = gardenPlot.Location.ZipCode,
                    Street = gardenPlot.Location.Street,
                    State = gardenPlot.Location.State
                    },
                ActiveCrop = gardenPlot.ActiveCrop != null
                ? new PlantedCropDto
                {
                    Id = gardenPlot.ActiveCrop.Id,
                    CropTypeId = gardenPlot.ActiveCrop.CropTypeId,
                    PlantedAt = gardenPlot.ActiveCrop.PlantedAt,
                    State = gardenPlot.ActiveCrop.State
                }: null
            }:null;
            if (gardenPlot == null) return NotFound();
            return Ok(gardenPlotDto);
        }

        /// <summary>
        /// Crea un nuevo huerto urbano.
        /// </summary>
        /// <param name="gardenPlot">Datos del huerto a crear.</param>
        /// <returns>El huerto creado con su ID asignado.</returns>
        [HttpPost]
        public IActionResult Create([FromBody] CreateGardenPlotDto gardenPlot)
        {
            var newGardenPlot = new GardenPlot
            {
                Name = gardenPlot.Name,
                Size = gardenPlot.Size,
                Location = new Direction
                {
                    City = gardenPlot.Location.City,
                    Street = gardenPlot.Location.Street,
                    State = gardenPlot.Location.State,
                    ZipCode = gardenPlot.Location.ZipCode
                }
            };
            _gardenPlotService.Add(newGardenPlot);
            return CreatedAtAction(nameof(GetById), new { version = RouteData.Values["version"],id = newGardenPlot.ID }, newGardenPlot);
        }

        /// <summary>
        /// Actualiza información básica de un huerto existente.
        /// </summary>
        /// <param name="id">ID del huerto a actualizar.</param>
        /// <param name="gardenPlot">Datos actualizados del huerto.</param>
        /// <returns>204 No Content si se actualiza correctamente.</returns>
        [HttpPatch("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateGardenPlotDto gardenPlot)
        {
            try
            {
                _gardenPlotService.UpdateBasicInfo(id, gardenPlot);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Elimina un huerto por su ID.
        /// </summary>
        /// <param name="id">ID del huerto a eliminar.</param>
        /// <returns>204 No Content si se elimina correctamente.</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _gardenPlotService.Delete(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Planta un cultivo en un huerto.
        /// </summary>
        /// <param name="id">ID del huerto.</param>
        /// <param name="dto">Datos del cultivo a plantar.</param>
        /// <returns>204 No Content si se planta correctamente.</returns>
        [HttpPost("{id}/plant")]
        public IActionResult PlantCrop(int id, [FromBody] PlantCropDto dto)
        {
            try
            {
                _gardenPlotService.PlantCrop(id, dto.CropTypeId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Cosecha el cultivo activo de un huerto.
        /// </summary>
        /// <param name="id">ID del huerto.</param>
        /// <param name="dto">Datos a registrar de al cosecha.</param>
        /// <returns>204 No Content si se cosecha correctamente.</returns>
        [HttpPost("{id}/harvest")]
        public IActionResult HarvestCrop(int id, [FromBody]CreateHarvestDto dto)
        {
            try
            {
                _gardenPlotService.HarvestCrop(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Elimina el cultivo activo de un huerto.
        /// </summary>
        /// <param name="id">ID del huerto.</param>
        /// <returns>204 No Content si se elimina correctamente.</returns>
        [HttpDelete("{id}/crop")]
        public IActionResult RemoveCrop(int id)
        {
            try
            {
                _gardenPlotService.RemoveCrop(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Actualiza el estado de un cultivo activo en el huerto.
        /// </summary>
        /// <param name="id">ID del huerto.</param>
        /// <param name="dto"></param>
        /// <returns>204 No Content si se actualiza correctamente.</returns>
        [HttpPatch("{id}/crop/status")]
        public IActionResult UpdateStatus(int id, UpdatePlantedCropDto dto)
        {
            try
            {
                _gardenPlotService.UpdateStatus(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}/harvests")]
        public IActionResult GetHarvests(int id)
        {
            try
            {
                var harvests = _gardenPlotService.GetHarvests(id);
                var harvestsDto = harvests.Select(h => new HarvestDto
                {
                    PlantedCropId = h.PlantedCropId,
                    CropTypeId = h.CropTypeId,
                    GardenPlotId = h.GardenPlotId,
                    Quantity = h.Quantity,
                    Date = h.Date
                });
                return Ok(harvests);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}