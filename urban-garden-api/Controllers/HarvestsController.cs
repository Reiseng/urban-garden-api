using Microsoft.AspNetCore.Mvc;
using UrbanGarden.Api.Models.Dtos;
using UrbanGarden.Api.Services;

namespace UrbanGarden.Api.Controllers
{
    /// <summary>
    /// Controlador para consultar el registro de cosechas.
    /// </summary>
    [ApiController]
    [Route("api/{version}/harvests")]
    public class HarvestsController : ControllerBase
    {
        private readonly IHarvestService _harvestService;
        /// <summary>
        /// Constructor del controlador de cosechas.
        /// </summary>
        /// <param name="harvestService">Servicio de huertos.</param>
        public HarvestsController(IHarvestService harvestService)
        {
            _harvestService = harvestService;
        }
    
        /// <summary>
        /// Obtiene todos las cosechas realizadas disponibles.
        /// </summary>
        /// <returns>Lista de cosechas.</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var harvests = _harvestService.GetAll();
            var harvestsDtos = harvests.Select(h => new HarvestDto
            {
                    PlantedCropId = h.PlantedCropId,
                    CropTypeId = h.CropTypeId,
                    GardenPlotId = h.GardenPlotId,
                    Quantity = h.Quantity,
                    Date = h.Date
            });
            return Ok(harvestsDtos);
        }
        /// <summary>
        /// Obtiene una cosecha específica por su ID.
        /// </summary>
        /// <param name="id">ID de la cosecha.</param>
        /// <returns>La cosecha encontrada o 404 si no existe.</returns>
        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            var harvest = _harvestService.GetById(id);
            var harvestDto = harvest != null
            ? new HarvestDto
            {
                PlantedCropId = harvest.PlantedCropId,
                CropTypeId = harvest.CropTypeId,
                GardenPlotId = harvest.GardenPlotId,
                Quantity = harvest.Quantity,
                Date = harvest.Date
            } : null;
            if(harvest == null) return NotFound();
            return Ok(harvestDto);
        }
    }
}