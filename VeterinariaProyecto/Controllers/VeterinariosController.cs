using Microsoft.AspNetCore.Mvc;
using VeterinariaProyecto.Models;
using VeterinariaProyecto.DAO;

namespace VeterinariaProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeterinariosController : ControllerBase
    {
        private readonly VeterinarioDAO _veterinarioDAO;

        public VeterinariosController()
        {
            _veterinarioDAO = new VeterinarioDAO();
        }

        // GET: api/Veterinarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Veterinario>>> GetVeterinarios()
        {
            var veterinarios = await _veterinarioDAO.ObtenerTodosAsync();
            return Ok(veterinarios);
        }

        // GET: api/Veterinarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Veterinario>> GetVeterinario(int id)
        {
            var veterinario = await _veterinarioDAO.ObtenerPorIdAsync(id);

            if (veterinario == null)
            {
                return NotFound();
            }

            return Ok(veterinario);
        }

        // PUT: api/Veterinarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVeterinario(int id, Veterinario veterinario)
        {
            if (id != veterinario.IdVeterinario)
            {
                return BadRequest("El ID de la URL no coincide con el ID del objeto");
            }

            var resultado = await _veterinarioDAO.ActualizarAsync(veterinario);
            if (!resultado)
            {
                return NotFound("No se pudo actualizar o no se encontró el veterinario");
            }

            return NoContent();
        }

        // POST: api/Veterinarios
        [HttpPost]
        public async Task<ActionResult<Veterinario>> PostVeterinario(Veterinario veterinario)
        {
            int nuevoId = await _veterinarioDAO.InsertarAsync(veterinario);
            veterinario.IdVeterinario = nuevoId;

            return CreatedAtAction("GetVeterinario", new { id = veterinario.IdVeterinario }, veterinario);
        }

        // DELETE: api/Veterinarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVeterinario(int id)
        {
            var resultado = await _veterinarioDAO.EliminarAsync(id);
            if (!resultado)
            {
                return NotFound("No se pudo eliminar o no se encontró el veterinario");
            }

            return NoContent();
        }
    }
}
