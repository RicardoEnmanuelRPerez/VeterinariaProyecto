using Microsoft.AspNetCore.Mvc;
using VeterinariaProyecto.Models;
using VeterinariaProyecto.DAO;

namespace VeterinariaProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadsController : ControllerBase
    {
        private readonly EspecialidadDAO _especialidadDAO;

        public EspecialidadsController()
        {
            _especialidadDAO = new EspecialidadDAO();
        }

        // GET: api/Especialidads
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Especialidad>>> GetEspecialidads()
        {
            var especialidades = await _especialidadDAO.ObtenerTodosAsync();
            return Ok(especialidades);
        }

        // GET: api/Especialidads/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Especialidad>> GetEspecialidad(int id)
        {
            var especialidad = await _especialidadDAO.ObtenerPorIdAsync(id);

            if (especialidad == null)
            {
                return NotFound();
            }

            return Ok(especialidad);
        }

        // PUT: api/Especialidads/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEspecialidad(int id, Especialidad especialidad)
        {
            if (id != especialidad.IdEspecialidad)
            {
                return BadRequest("El ID de la URL no coincide con el ID del objeto");
            }

            var resultado = await _especialidadDAO.ActualizarAsync(especialidad);
            if (!resultado)
            {
                return NotFound("No se pudo actualizar o no se encontró la especialidad");
            }

            return NoContent();
        }

        // POST: api/Especialidads
        [HttpPost]
        public async Task<ActionResult<Especialidad>> PostEspecialidad(Especialidad especialidad)
        {
            int nuevoId = await _especialidadDAO.InsertarAsync(especialidad);
            especialidad.IdEspecialidad = nuevoId;

            return CreatedAtAction("GetEspecialidad", new { id = especialidad.IdEspecialidad }, especialidad);
        }

        // DELETE: api/Especialidads/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEspecialidad(int id)
        {
            var resultado = await _especialidadDAO.EliminarAsync(id);
            if (!resultado)
            {
                return NotFound("No se pudo eliminar o no se encontró la especialidad");
            }

            return NoContent();
        }
    }
}
