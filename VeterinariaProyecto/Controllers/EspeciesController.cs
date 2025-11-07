using Microsoft.AspNetCore.Mvc;
using VeterinariaProyecto.Models;
using VeterinariaProyecto.DAO;

namespace VeterinariaProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspeciesController : ControllerBase
    {
        private readonly EspecieDAO _especieDAO;

        public EspeciesController()
        {
            _especieDAO = new EspecieDAO();
        }

        // GET: api/Especies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Especie>>> GetEspecies()
        {
            var especies = await _especieDAO.ObtenerTodosAsync();
            return Ok(especies);
        }

        // GET: api/Especies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Especie>> GetEspecie(int id)
        {
            var especie = await _especieDAO.ObtenerPorIdAsync(id);

            if (especie == null)
            {
                return NotFound();
            }

            return Ok(especie);
        }

        // PUT: api/Especies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEspecie(int id, Especie especie)
        {
            if (id != especie.IdEspecie)
            {
                return BadRequest("El ID de la URL no coincide con el ID del objeto");
            }

            var resultado = await _especieDAO.ActualizarAsync(especie);
            if (!resultado)
            {
                return NotFound("No se pudo actualizar o no se encontró la especie");
            }

            return NoContent();
        }

        // POST: api/Especies
        [HttpPost]
        public async Task<ActionResult<Especie>> PostEspecie(Especie especie)
        {
            int nuevoId = await _especieDAO.InsertarAsync(especie);
            especie.IdEspecie = nuevoId;

            return CreatedAtAction("GetEspecie", new { id = especie.IdEspecie }, especie);
        }

        // DELETE: api/Especies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEspecie(int id)
        {
            var resultado = await _especieDAO.EliminarAsync(id);
            if (!resultado)
            {
                return NotFound("No se pudo eliminar o no se encontró la especie");
            }

            return NoContent();
        }
    }
}
