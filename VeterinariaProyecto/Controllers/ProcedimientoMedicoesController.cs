using Microsoft.AspNetCore.Mvc;
using VeterinariaProyecto.Models;
using VeterinariaProyecto.DAO;

namespace VeterinariaProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcedimientoMedicoesController : ControllerBase
    {
        private readonly ProcedimientoMedicoDAO _procedimientoMedicoDAO;

        public ProcedimientoMedicoesController()
        {
            _procedimientoMedicoDAO = new ProcedimientoMedicoDAO();
        }

        // GET: api/ProcedimientoMedicoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProcedimientoMedico>>> GetProcedimientoMedicos()
        {
            var procedimientos = await _procedimientoMedicoDAO.ObtenerTodosAsync();
            return Ok(procedimientos);
        }

        // GET: api/ProcedimientoMedicoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProcedimientoMedico>> GetProcedimientoMedico(int id)
        {
            var procedimiento = await _procedimientoMedicoDAO.ObtenerPorIdAsync(id);

            if (procedimiento == null)
            {
                return NotFound();
            }

            return Ok(procedimiento);
        }

        // PUT: api/ProcedimientoMedicoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProcedimientoMedico(int id, ProcedimientoMedico procedimiento)
        {
            if (id != procedimiento.IdProcedimiento)
            {
                return BadRequest("El ID de la URL no coincide con el ID del objeto");
            }

            var resultado = await _procedimientoMedicoDAO.ActualizarAsync(procedimiento);
            if (!resultado)
            {
                return NotFound("No se pudo actualizar o no se encontró el procedimiento médico");
            }

            return NoContent();
        }

        // POST: api/ProcedimientoMedicoes
        [HttpPost]
        public async Task<ActionResult<ProcedimientoMedico>> PostProcedimientoMedico(ProcedimientoMedico procedimiento)
        {
            int nuevoId = await _procedimientoMedicoDAO.InsertarAsync(procedimiento);
            procedimiento.IdProcedimiento = nuevoId;

            return CreatedAtAction("GetProcedimientoMedico", new { id = procedimiento.IdProcedimiento }, procedimiento);
        }

        // DELETE: api/ProcedimientoMedicoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProcedimientoMedico(int id)
        {
            var resultado = await _procedimientoMedicoDAO.EliminarAsync(id);
            if (!resultado)
            {
                return NotFound("No se pudo eliminar o no se encontró el procedimiento médico");
            }

            return NoContent();
        }
    }
}
