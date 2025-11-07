using Microsoft.AspNetCore.Mvc;
using VeterinariaProyecto.Models;
using VeterinariaProyecto.DAO;

namespace VeterinariaProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TratamientoesController : ControllerBase
    {
        private readonly TratamientoDAO _tratamientoDAO;

        public TratamientoesController()
        {
            _tratamientoDAO = new TratamientoDAO();
        }

        // GET: api/Tratamientoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tratamiento>>> GetTratamientoes()
        {
            var tratamientos = await _tratamientoDAO.ObtenerTodosAsync();
            return Ok(tratamientos);
        }

        // GET: api/Tratamientoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tratamiento>> GetTratamiento(int id)
        {
            var tratamiento = await _tratamientoDAO.ObtenerPorIdAsync(id);

            if (tratamiento == null)
            {
                return NotFound();
            }

            return Ok(tratamiento);
        }

        // PUT: api/Tratamientoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTratamiento(int id, Tratamiento tratamiento)
        {
            if (id != tratamiento.IdTratamiento)
            {
                return BadRequest("El ID de la URL no coincide con el ID del objeto");
            }

            var resultado = await _tratamientoDAO.ActualizarAsync(tratamiento);
            if (!resultado)
            {
                return NotFound("No se pudo actualizar o no se encontró el tratamiento");
            }

            return NoContent();
        }

        // POST: api/Tratamientoes
        [HttpPost]
        public async Task<ActionResult<Tratamiento>> PostTratamiento(Tratamiento tratamiento)
        {
            int nuevoId = await _tratamientoDAO.InsertarAsync(tratamiento);
            tratamiento.IdTratamiento = nuevoId;

            return CreatedAtAction("GetTratamiento", new { id = tratamiento.IdTratamiento }, tratamiento);
        }

        // DELETE: api/Tratamientoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTratamiento(int id)
        {
            var resultado = await _tratamientoDAO.EliminarAsync(id);
            if (!resultado)
            {
                return NotFound("No se pudo eliminar o no se encontró el tratamiento");
            }

            return NoContent();
        }
    }
}
