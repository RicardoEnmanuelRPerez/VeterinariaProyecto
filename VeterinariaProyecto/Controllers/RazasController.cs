using Microsoft.AspNetCore.Mvc;
using VeterinariaProyecto.Models;
using VeterinariaProyecto.DAO;

namespace VeterinariaProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RazasController : ControllerBase
    {
        private readonly RazaDAO _razaDAO;

        public RazasController()
        {
            _razaDAO = new RazaDAO();
        }

        // GET: api/Razas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Raza>>> GetRazas()
        {
            var razas = await _razaDAO.ObtenerTodosAsync();
            return Ok(razas);
        }

        // GET: api/Razas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Raza>> GetRaza(int id)
        {
            var raza = await _razaDAO.ObtenerPorIdAsync(id);

            if (raza == null)
            {
                return NotFound();
            }

            return Ok(raza);
        }

        // PUT: api/Razas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRaza(int id, Raza raza)
        {
            if (id != raza.IdRaza)
            {
                return BadRequest("El ID de la URL no coincide con el ID del objeto");
            }

            var resultado = await _razaDAO.ActualizarAsync(raza);
            if (!resultado)
            {
                return NotFound("No se pudo actualizar o no se encontró la raza");
            }

            return NoContent();
        }

        // POST: api/Razas
        [HttpPost]
        public async Task<ActionResult<Raza>> PostRaza(Raza raza)
        {
            int nuevoId = await _razaDAO.InsertarAsync(raza);
            raza.IdRaza = nuevoId;

            return CreatedAtAction("GetRaza", new { id = raza.IdRaza }, raza);
        }

        // DELETE: api/Razas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRaza(int id)
        {
            var resultado = await _razaDAO.EliminarAsync(id);
            if (!resultado)
            {
                return NotFound("No se pudo eliminar o no se encontró la raza");
            }

            return NoContent();
        }
    }
}
