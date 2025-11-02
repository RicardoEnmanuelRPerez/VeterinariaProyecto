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
    public class FacturaProductoesController : ControllerBase
    {
        private readonly VeterinariaContext _context;

        public FacturaProductoesController(VeterinariaContext context)
        {
            _context = context;
        }

        // GET: api/FacturaProductoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FacturaProducto>>> GetFacturaProductos()
        {
            return await _context.FacturaProductos.ToListAsync();
        }

        // GET: api/FacturaProductoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FacturaProducto>> GetFacturaProducto(int id)
        {
            var facturaProducto = await _context.FacturaProductos.FindAsync(id);

            if (facturaProducto == null)
            {
                return NotFound();
            }

            return facturaProducto;
        }

        // PUT: api/FacturaProductoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFacturaProducto(int id, FacturaProducto facturaProducto)
        {
            if (id != facturaProducto.IdFactura)
            {
                return BadRequest();
            }

            _context.Entry(facturaProducto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacturaProductoExists(id))
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

        // POST: api/FacturaProductoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FacturaProducto>> PostFacturaProducto(FacturaProducto facturaProducto)
        {
            _context.FacturaProductos.Add(facturaProducto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FacturaProductoExists(facturaProducto.IdFactura))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFacturaProducto", new { id = facturaProducto.IdFactura }, facturaProducto);
        }

        // DELETE: api/FacturaProductoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFacturaProducto(int id)
        {
            var facturaProducto = await _context.FacturaProductos.FindAsync(id);
            if (facturaProducto == null)
            {
                return NotFound();
            }

            _context.FacturaProductos.Remove(facturaProducto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FacturaProductoExists(int id)
        {
            return _context.FacturaProductos.Any(e => e.IdFactura == id);
        }
    }
}
