using Microsoft.AspNetCore.Mvc;
using VeterinariaProyecto.Models;
using VeterinariaProyecto.DAO;

namespace VeterinariaProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SexoesController : ControllerBase
    {
        private readonly SexoDAO _sexoDAO;

        public SexoesController()
        {
            _sexoDAO = new SexoDAO();
        }

        // GET: api/Sexoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sexo>>> GetSexoes()
        {
            var sexos = await _sexoDAO.ObtenerTodosAsync();
            return Ok(sexos);
        }

        // GET: api/Sexoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sexo>> GetSexo(int id)
        {
            var sexo = await _sexoDAO.ObtenerPorIdAsync(id);

            if (sexo == null)
            {
                return NotFound();
            }

            return Ok(sexo);
        }

        // PUT: api/Sexoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSexo(int id, Sexo sexo)
        {
            if (id != sexo.IdSexo)
            {
                return BadRequest("El ID de la URL no coincide con el ID del objeto");
            }

            var resultado = await _sexoDAO.ActualizarAsync(sexo);
            if (!resultado)
            {
                return NotFound("No se pudo actualizar o no se encontró el sexo");
            }

            return NoContent();
        }

        // POST: api/Sexoes
        [HttpPost]
        public async Task<ActionResult<Sexo>> PostSexo(Sexo sexo)
        {
            int nuevoId = await _sexoDAO.InsertarAsync(sexo);
            sexo.IdSexo = nuevoId;

            return CreatedAtAction("GetSexo", new { id = sexo.IdSexo }, sexo);
        }

        // DELETE: api/Sexoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSexo(int id)
        {
            var resultado = await _sexoDAO.EliminarAsync(id);
            if (!resultado)
            {
                return NotFound("No se pudo eliminar o no se encontró el sexo");
            }

            return NoContent();
        }
    }
}
