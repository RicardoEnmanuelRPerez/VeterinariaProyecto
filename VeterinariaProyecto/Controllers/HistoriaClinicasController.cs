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
    public class HistoriaClinicasController : ControllerBase
    {
        private readonly VeterinariaContext _context;

        public HistoriaClinicasController(VeterinariaContext context)
        {
            _context = context;
        }

        // GET: api/HistoriaClinicas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistoriaClinica>>> GetHistoriaClinicas()
        {
            return await _context.HistoriaClinicas
                .Include(h => h.IdMascotaNavigation)
                .Include(h => h.ProcedimientosMedicos)
                .ToListAsync();
        }

        // GET: api/HistoriaClinicas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HistoriaClinica>> GetHistoriaClinica(int id)
        {
            var historiaClinica = await _context.HistoriaClinicas
                .Include(h => h.IdMascotaNavigation)
                .Include(h => h.ProcedimientosMedicos)
                .FirstOrDefaultAsync(h => h.IdHistoria == id);

            if (historiaClinica == null)
            {
                return NotFound();
            }

            return historiaClinica;
        }

        // GET: api/HistoriaClinicas/ByMascota/5
        [HttpGet("ByMascota/{idMascota}")]
        public async Task<ActionResult<HistoriaClinica>> GetHistoriaClinicaByMascota(int idMascota)
        {
            var historiaClinica = await _context.HistoriaClinicas
                .Include(h => h.IdMascotaNavigation)
                .Include(h => h.ProcedimientosMedicos)
                    .ThenInclude(p => p.IdVeterinarioNavigation)
                .FirstOrDefaultAsync(h => h.IdMascota == idMascota);

            if (historiaClinica == null)
            {
                return NotFound();
            }

            return historiaClinica;
        }

        // PUT: api/HistoriaClinicas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistoriaClinica(int id, HistoriaClinica historiaClinica)
        {
            if (id != historiaClinica.IdHistoria)
            {
                return BadRequest();
            }

            _context.Entry(historiaClinica).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistoriaClinicaExists(id))
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

        // POST: api/HistoriaClinicas
        [HttpPost]
        public async Task<ActionResult<HistoriaClinica>> PostHistoriaClinica(HistoriaClinica historiaClinica)
        {
            _context.HistoriaClinicas.Add(historiaClinica);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHistoriaClinica", new { id = historiaClinica.IdHistoria }, historiaClinica);
        }

        // DELETE: api/HistoriaClinicas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistoriaClinica(int id)
        {
            var historiaClinica = await _context.HistoriaClinicas.FindAsync(id);
            if (historiaClinica == null)
            {
                return NotFound();
            }

            _context.HistoriaClinicas.Remove(historiaClinica);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HistoriaClinicaExists(int id)
        {
            return _context.HistoriaClinicas.Any(e => e.IdHistoria == id);
        }
    }
}
