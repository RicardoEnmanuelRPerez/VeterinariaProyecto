using Microsoft.AspNetCore.Mvc;
using VeterinariaProyecto.Models;
using VeterinariaProyecto.DAO;

namespace VeterinariaProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitasController : ControllerBase
    {
        private readonly CitaDAO _citaDAO;

        public CitasController()
        {
            _citaDAO = new CitaDAO();
        }

        // GET: api/Citas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cita>>> GetCita()
        {
            var citas = await _citaDAO.ObtenerTodosAsync();
            return Ok(citas);
        }

        // GET: api/Citas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cita>> GetCita(int id)
        {
            var cita = await _citaDAO.ObtenerPorIdAsync(id);

            if (cita == null)
            {
                return NotFound();
            }

            return Ok(cita);
        }

        // PUT: api/Citas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCita(int id, Cita cita)
        {
            if (id != cita.IdCita)
            {
                return BadRequest("El ID de la URL no coincide con el ID del objeto");
            }

            var resultado = await _citaDAO.ActualizarAsync(cita);
            if (!resultado)
            {
                return NotFound("No se pudo actualizar o no se encontró la cita");
            }

            return NoContent();
        }

        // POST: api/Citas
        [HttpPost]
        public async Task<ActionResult<Cita>> PostCita(Cita cita)
        {
            int nuevoId = await _citaDAO.InsertarAsync(cita);
            cita.IdCita = nuevoId;

            return CreatedAtAction("GetCita", new { id = cita.IdCita }, cita);
        }

        // DELETE: api/Citas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCita(int id)
        {
            var resultado = await _citaDAO.EliminarAsync(id);
            if (!resultado)
            {
                return NotFound("No se pudo eliminar o no se encontró la cita");
            }

            return NoContent();
        }
    }
}
