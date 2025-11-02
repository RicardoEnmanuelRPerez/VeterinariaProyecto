using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Veterinaria_Genesis_DB.Data;
using Veterinaria_Genesis_DB.Models;

namespace VeterinariaGenesis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcedimientoMedicoesController : ControllerBase
    {
        private readonly VeterinariaContext _context;

        public ProcedimientoMedicoesController(VeterinariaContext context)
        {
            _context = context;
        }

        // GET: api/ProcedimientoMedicoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProcedimientoMedico>>> GetProcedimientosMedicos()
        {
            return await _context.ProcedimientosMedicos
                .Include(p => p.IdHistoriaNavigation)
                    .ThenInclude(h => h.IdMascotaNavigation)
                .Include(p => p.IdVeterinarioNavigation)
                .ToListAsync();
        }

        // GET: api/ProcedimientoMedicoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProcedimientoMedico>> GetProcedimientoMedico(int id)
        {
            var procedimientoMedico = await _context.ProcedimientosMedicos
                .Include(p => p.IdHistoriaNavigation)
                    .ThenInclude(h => h.IdMascotaNavigation)
                .Include(p => p.IdVeterinarioNavigation)
                .FirstOrDefaultAsync(p => p.IdProcedimiento == id);

            if (procedimientoMedico == null)
            {
                return NotFound();
            }

            return procedimientoMedico;
        }

        // GET: api/ProcedimientoMedicoes/ByMascota/5
        [HttpGet("ByMascota/{idMascota}")]
        public async Task<ActionResult<IEnumerable<ProcedimientoMedico>>> GetProcedimientosByMascota(int idMascota)
        {
            var procedimientos = await _context.ProcedimientosMedicos
                .Include(p => p.IdHistoriaNavigation)
                    .ThenInclude(h => h.IdMascotaNavigation)
                .Include(p => p.IdVeterinarioNavigation)
                .Where(p => p.IdHistoriaNavigation.IdMascota == idMascota)
                .ToListAsync();

            return procedimientos;
        }

        // GET: api/ProcedimientoMedicoes/ByTipo/Hospitalizacion
        [HttpGet("ByTipo/{tipo}")]
        public async Task<ActionResult<IEnumerable<ProcedimientoMedico>>> GetProcedimientosByTipo(string tipo)
        {
            var procedimientos = await _context.ProcedimientosMedicos
                .Include(p => p.IdHistoriaNavigation)
                    .ThenInclude(h => h.IdMascotaNavigation)
                .Include(p => p.IdVeterinarioNavigation)
                .Where(p => p.Tipo == tipo)
                .ToListAsync();

            return procedimientos;
        }

        // PUT: api/ProcedimientoMedicoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProcedimientoMedico(int id, ProcedimientoMedico procedimientoMedico)
        {
            if (id != procedimientoMedico.IdProcedimiento)
            {
                return BadRequest();
            }

            _context.Entry(procedimientoMedico).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProcedimientoMedicoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ProcedimientoMedicoes
        [HttpPost]
        public async Task<ActionResult<ProcedimientoMedico>> PostProcedimientoMedico(ProcedimientoMedico procedimientoMedico)
        {
            _context.ProcedimientosMedicos.Add(procedimientoMedico);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProcedimientoMedico", new { id = procedimientoMedico.IdProcedimiento }, procedimientoMedico);
        }

        // DELETE: api/ProcedimientoMedicoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProcedimientoMedico(int id)
        {
            var procedimientoMedico = await _context.ProcedimientosMedicos.FindAsync(id);
            if (procedimientoMedico == null)
            {
                return NotFound();
            }

            _context.ProcedimientosMedicos.Remove(procedimientoMedico);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/ProcedimientoMedicoes/5/Estado
        [HttpPut("{id}/Estado")]
        public async Task<IActionResult> UpdateEstado(int id, [FromBody] string nuevoEstado)
        {
            var procedimiento = await _context.ProcedimientosMedicos.FindAsync(id);
            if (procedimiento == null)
            {
                return NotFound();
            }

            procedimiento.Estado = nuevoEstado;
            
            // Si se completa, establecer fecha fin
            if (nuevoEstado == "Completado" && procedimiento.FechaFin == null)
            {
                procedimiento.FechaFin = DateTime.Now;
            }

            await _context.SaveChangesAsync();
            return Ok(procedimiento);
        }

        private bool ProcedimientoMedicoExists(int id)
        {
            return _context.ProcedimientosMedicos.Any(e => e.IdProcedimiento == id);
        }
    }
}
