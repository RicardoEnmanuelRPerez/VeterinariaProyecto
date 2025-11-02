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
    public class VeterinariosController : ControllerBase
    {
        private readonly VeterinariaContext _context;

        public VeterinariosController(VeterinariaContext context)
        {
            _context = context;
        }

        // GET: api/Veterinarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Veterinario>>> GetVeterinarios()
        {
            return await _context.Veterinarios.Where(v => v.Activo).ToListAsync();
        }

        // GET: api/Veterinarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Veterinario>> GetVeterinario(int id)
        {
            var veterinario = await _context.Veterinarios.FindAsync(id);

            if (veterinario == null)
            {
                return NotFound();
            }

            return veterinario;
        }

        // PUT: api/Veterinarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVeterinario(int id, Veterinario veterinario)
        {
            if (id != veterinario.IdVeterinario)
            {
                return BadRequest();
            }

            _context.Entry(veterinario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VeterinarioExists(id))
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

        // POST: api/Veterinarios
        [HttpPost]
        public async Task<ActionResult<Veterinario>> PostVeterinario(Veterinario veterinario)
        {
            _context.Veterinarios.Add(veterinario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVeterinario", new { id = veterinario.IdVeterinario }, veterinario);
        }

        // DELETE: api/Veterinarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVeterinario(int id)
        {
            var veterinario = await _context.Veterinarios.FindAsync(id);
            if (veterinario == null)
            {
                return NotFound();
            }

            veterinario.Activo = false;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VeterinarioExists(int id)
        {
            return _context.Veterinarios.Any(e => e.IdVeterinario == id);
        }
    }
}
