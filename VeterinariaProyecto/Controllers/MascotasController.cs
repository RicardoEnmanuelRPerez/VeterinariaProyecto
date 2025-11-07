using Microsoft.AspNetCore.Mvc;
using VeterinariaProyecto.Models;
using VeterinariaProyecto.DAO;

namespace VeterinariaProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MascotasController : ControllerBase
    {
        private readonly MascotaDAO _mascotaDAO;

        public MascotasController()
        {
            _mascotaDAO = new MascotaDAO();
        }

        // GET: api/Mascotas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mascota>>> GetMascota()
        {
            var mascotas = await _mascotaDAO.ObtenerTodosAsync();
            return Ok(mascotas);
        }

        // GET: api/Mascotas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Mascota>> GetMascota(int id)
        {
            var mascota = await _mascotaDAO.ObtenerPorIdAsync(id);

            if (mascota == null)
            {
                return NotFound();
            }

            return Ok(mascota);
        }

        // PUT: api/Mascotas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMascota(int id, Mascota mascota)
        {
            if (id != mascota.IdMascota)
            {
                return BadRequest("El ID de la URL no coincide con el ID del objeto");
            }

            var resultado = await _mascotaDAO.ActualizarAsync(mascota);
            if (!resultado)
            {
                return NotFound("No se pudo actualizar o no se encontró la mascota");
            }

            return NoContent();
        }

        // POST: api/Mascotas
        [HttpPost]
        public async Task<ActionResult<Mascota>> PostMascota(Mascota mascota)
        {
            int nuevoId = await _mascotaDAO.InsertarAsync(mascota);
            mascota.IdMascota = nuevoId;

            return CreatedAtAction("GetMascota", new { id = mascota.IdMascota }, mascota);
        }

        // DELETE: api/Mascotas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMascota(int id)
        {
            var resultado = await _mascotaDAO.EliminarAsync(id);
            if (!resultado)
            {
                return NotFound("No se pudo eliminar o no se encontró la mascota");
            }

            return NoContent();
        }
    }
}
