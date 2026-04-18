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
    public class CultiveController : ControllerBase
    {
        private readonly ICultiveService _cultiveService;

        public CultiveController(ICultiveService cultiveService)
        {
            _cultiveService = cultiveService;
        }

        /// <summary>
        /// Obtiene una lista de todos los cultivos disponibles.
        /// </summary>
        /// <returns>Lista de cultivos en formato DTO.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<CultivoDto>> GetAll()
        {
            var cultives = _cultiveService.GetAll();
            var cultivesDto = cultives.Select(c => new CultivoDto
            {
                ID = c.ID,
                Name = c.Name,
                Season = c.Season,
                Disponible = c.Disponible
            }).ToList();

            return Ok(cultivesDto);
        }

        /// <summary>
        /// Obtiene un cultivo específico por su ID.
        /// </summary>
        /// <param name="id">ID del cultivo a buscar.</param>
        /// <returns>El cultivo encontrado o 404 si no existe.</returns>
        [HttpGet("{id}")]
        public ActionResult<CultivoDto> GetById(int id)
        {
            var cultive = _cultiveService.GetById(id);
            if (cultive == null)
            {
                return NotFound();
            }

            var cultiveDto = new CultivoDto
            {
                ID = cultive.ID,
                Name = cultive.Name,
                Season = cultive.Season,
                Disponible = cultive.Disponible
            };

            return Ok(cultiveDto);
        }

        /// <summary>
        /// Crea un nuevo cultivo.
        /// </summary>
        /// <param name="createDto">Datos del cultivo a crear.</param>
        /// <returns>El cultivo creado con su ID asignado.</returns>
        [HttpPost]
        public ActionResult Create([FromBody] CultiveCreateDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cultive = new Cultive
            {
                Name = createDto.Name,
                Season = createDto.Season,
                Disponible = createDto.Disponible ?? true
            };

            _cultiveService.Add(cultive);

            return CreatedAtAction(nameof(GetById), new { version = RouteData.Values["version"], id = cultive.ID }, cultive);
        }

        /// <summary>
        /// Actualiza un cultivo existente.
        /// </summary>
        /// <param name="id">ID del cultivo a actualizar.</param>
        /// <param name="updateDto">Datos actualizados del cultivo.</param>
        /// <returns>204 No Content si se actualiza correctamente, o 404 si no existe.</returns>
        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] CultivoDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingCultive = _cultiveService.GetById(id);
            if (existingCultive == null)
            {
                return NotFound();
            }

            existingCultive.Name = updateDto.Name;
            existingCultive.Season = updateDto.Season;
            existingCultive.Disponible = updateDto.Disponible ?? existingCultive.Disponible;

            _cultiveService.Update(existingCultive);

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
            var existingCultive = _cultiveService.GetById(id);
            if (existingCultive == null)
            {
                return NotFound();
            }

            _cultiveService.Delete(id);

            return NoContent();
        }
    }
}