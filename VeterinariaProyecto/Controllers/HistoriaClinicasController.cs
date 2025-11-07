using Microsoft.AspNetCore.Mvc;
using VeterinariaProyecto.Models;
using VeterinariaProyecto.DAO;

namespace VeterinariaProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoriaClinicasController : ControllerBase
    {
        private readonly HistoriaClinicaDAO _historiaClinicaDAO;

        public HistoriaClinicasController()
        {
            _historiaClinicaDAO = new HistoriaClinicaDAO();
        }

        // GET: api/HistoriaClinicas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistoriaClinica>>> GetHistoriaClinicas()
        {
            var historiasClinicas = await _historiaClinicaDAO.ObtenerTodosAsync();
            return Ok(historiasClinicas);
        }

        // GET: api/HistoriaClinicas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HistoriaClinica>> GetHistoriaClinica(int id)
        {
            var historiaClinica = await _historiaClinicaDAO.ObtenerPorIdAsync(id);

            if (historiaClinica == null)
            {
                return NotFound();
            }

            return Ok(historiaClinica);
        }

        // GET: api/HistoriaClinicas/mascota/5
        [HttpGet("mascota/{idMascota}")]
        public async Task<ActionResult<HistoriaClinica>> GetHistoriaClinicaPorMascota(int idMascota)
        {
            var historiaClinica = await _historiaClinicaDAO.ObtenerPorMascotaAsync(idMascota);

            if (historiaClinica == null)
            {
                return NotFound();
            }

            return Ok(historiaClinica);
        }

        // PUT: api/HistoriaClinicas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistoriaClinica(int id, HistoriaClinica historiaClinica)
        {
            if (id != historiaClinica.IdHistoria)
            {
                return BadRequest("El ID de la URL no coincide con el ID del objeto");
            }

            var resultado = await _historiaClinicaDAO.ActualizarAsync(historiaClinica);
            if (!resultado)
            {
                return NotFound("No se pudo actualizar o no se encontró la historia clínica");
            }

            return NoContent();
        }

        // POST: api/HistoriaClinicas
        [HttpPost]
        public async Task<ActionResult<HistoriaClinica>> PostHistoriaClinica(HistoriaClinica historiaClinica)
        {
            int nuevoId = await _historiaClinicaDAO.InsertarAsync(historiaClinica);
            historiaClinica.IdHistoria = nuevoId;

            return CreatedAtAction("GetHistoriaClinica", new { id = historiaClinica.IdHistoria }, historiaClinica);
        }

        // DELETE: api/HistoriaClinicas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistoriaClinica(int id)
        {
            var resultado = await _historiaClinicaDAO.EliminarAsync(id);
            if (!resultado)
            {
                return NotFound("No se pudo eliminar o no se encontró la historia clínica");
            }

            return NoContent();
        }
    }
}
