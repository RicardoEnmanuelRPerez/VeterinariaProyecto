using Microsoft.AspNetCore.Mvc;
using VeterinariaProyecto.Models;
using VeterinariaProyecto.DAO;

namespace VeterinariaProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {
        private readonly FacturaDAO _facturaDAO;

        public FacturasController()
        {
            _facturaDAO = new FacturaDAO();
        }

        // GET: api/Facturas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Factura>>> GetFacturas()
        {
            var facturas = await _facturaDAO.ObtenerTodosAsync();
            return Ok(facturas);
        }

        // GET: api/Facturas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Factura>> GetFactura(int id)
        {
            var factura = await _facturaDAO.ObtenerPorIdAsync(id);

            if (factura == null)
            {
                return NotFound();
            }

            return Ok(factura);
        }

        // PUT: api/Facturas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFactura(int id, Factura factura)
        {
            if (id != factura.IdFactura)
            {
                return BadRequest("El ID de la URL no coincide con el ID del objeto");
            }

            var resultado = await _facturaDAO.ActualizarAsync(factura);
            if (!resultado)
            {
                return NotFound("No se pudo actualizar o no se encontró la factura");
            }

            return NoContent();
        }

        // POST: api/Facturas
        [HttpPost]
        public async Task<ActionResult<Factura>> PostFactura(Factura factura)
        {
            int nuevoId = await _facturaDAO.InsertarAsync(factura);
            factura.IdFactura = nuevoId;

            return CreatedAtAction("GetFactura", new { id = factura.IdFactura }, factura);
        }

        // DELETE: api/Facturas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFactura(int id)
        {
            var resultado = await _facturaDAO.EliminarAsync(id);
            if (!resultado)
            {
                return NotFound("No se pudo eliminar o no se encontró la factura");
            }

            return NoContent();
        }
    }
}
