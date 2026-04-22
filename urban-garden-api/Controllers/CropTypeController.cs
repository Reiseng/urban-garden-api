namespace UrbanGarden.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using UrbanGarden.Api.Models.Dtos;
    using UrbanGarden.Api.Models.Entities;
    using UrbanGarden.Api.Services;

    /// <summary>
    /// Controlador para gestionar operaciones CRUD de cultivos.
    /// </summary>
    [ApiController]
    [Route("api/{version}/[controller]")]
    public class CropTypeController : ControllerBase
    {
        private readonly ICropTypeService _cropTypeService;

        public CropTypeController(ICropTypeService cropTypeService)
        {
            _cropTypeService = cropTypeService;
        }

        /// <summary>
        /// Obtiene una lista de todos los cultivos disponibles.
        /// </summary>
        /// <returns>Lista de cultivos en formato DTO.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<CropTypeDto>> GetAll()
        {
            var cropTypes = _cropTypeService.GetAll();
            var cropTypesDto = cropTypes.Select(c => new CropTypeDto
            {
                ID = c.ID,
                Name = c.Name,
                Season = c.Season,
                Disponible = c.Disponible
            }).ToList();

            return Ok(cropTypesDto);
        }

        /// <summary>
        /// Obtiene un cultivo específico por su ID.
        /// </summary>
        /// <param name="id">ID del cultivo a buscar.</param>
        /// <returns>El cultivo encontrado o 404 si no existe.</returns>
        [HttpGet("{id}")]
        public ActionResult<CropTypeDto> GetById(int id)
        {
            var cropType = _cropTypeService.GetById(id);
            if (cropType == null)
            {
                return NotFound();
            }

            var cropTypeDto = new CropTypeDto
            {
                ID = cropType.ID,
                Name = cropType.Name,
                Season = cropType.Season,
                Disponible = cropType.Disponible
            };

            return Ok(cropTypeDto);
        }

        /// <summary>
        /// Crea un nuevo cultivo.
        /// </summary>
        /// <param name="createDto">Datos del cultivo a crear.</param>
        /// <returns>El cultivo creado con su ID asignado.</returns>
        [HttpPost]
        public ActionResult Create([FromBody] CreateCropTypeDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cropType = new CropType
            {
                Name = createDto.Name,
                Season = createDto.Season,
                Disponible = createDto.Disponible ?? true
            };

            _cropTypeService.Add(cropType);

            return CreatedAtAction(nameof(GetById), new { version = RouteData.Values["version"], id = cropType.ID }, cropType);
        }

        /// <summary>
        /// Actualiza un cultivo existente.
        /// </summary>
        /// <param name="id">ID del cultivo a actualizar.</param>
        /// <param name="updateDto">Datos actualizados del cultivo.</param>
        /// <returns>204 No Content si se actualiza correctamente, o 404 si no existe.</returns>
        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] CropTypeDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingCropType = _cropTypeService.GetById(id);
            if (existingCropType == null)
            {
                return NotFound();
            }

            existingCropType.Name = updateDto.Name;
            existingCropType.Season = updateDto.Season;
            existingCropType.Disponible = updateDto.Disponible ?? existingCropType.Disponible;

            _cropTypeService.Update(existingCropType);

            return NoContent();
        }
        
        /// <summary>
        /// Elimina un cultivo por su ID.
        /// </summary>
        /// <param name="id">ID del cultivo a eliminar.</param>
        /// <returns>204 No Content si se elimina correctamente, o 404 si no existe.</returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var existingCropType = _cropTypeService.GetById(id);
            if (existingCropType == null)
            {
                return NotFound();
            }

            _cropTypeService.Delete(id);

            return NoContent();
        }
    }
}