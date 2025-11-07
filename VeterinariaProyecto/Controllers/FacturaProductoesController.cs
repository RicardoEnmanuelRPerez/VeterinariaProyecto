using Microsoft.AspNetCore.Mvc;
using VeterinariaProyecto.Models;
using VeterinariaProyecto.DAO;

namespace VeterinariaProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaProductoesController : ControllerBase
    {
        private readonly FacturaProductoDAO _facturaProductoDAO;

        public FacturaProductoesController()
        {
            _facturaProductoDAO = new FacturaProductoDAO();
        }

        // GET: api/FacturaProductoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FacturaProducto>>> GetFacturaProductos()
        {
            var facturaProductos = await _facturaProductoDAO.ObtenerTodosAsync();
            return Ok(facturaProductos);
        }

        // GET: api/FacturaProductoes/5/3
        [HttpGet("{idFactura}/{idProducto}")]
        public async Task<ActionResult<FacturaProducto>> GetFacturaProducto(int idFactura, int idProducto)
        {
            var facturaProducto = await _facturaProductoDAO.ObtenerPorIdAsync(idFactura, idProducto);

            if (facturaProducto == null)
            {
                return NotFound();
            }

            return Ok(facturaProducto);
        }

        // GET: api/FacturaProductoes/factura/5
        [HttpGet("factura/{idFactura}")]
        public async Task<ActionResult<IEnumerable<FacturaProducto>>> GetFacturaProductosPorFactura(int idFactura)
        {
            var facturaProductos = await _facturaProductoDAO.ObtenerPorFacturaAsync(idFactura);
            return Ok(facturaProductos);
        }

        // PUT: api/FacturaProductoes/5/3
        [HttpPut("{idFactura}/{idProducto}")]
        public async Task<IActionResult> PutFacturaProducto(int idFactura, int idProducto, FacturaProducto facturaProducto)
        {
            if (idFactura != facturaProducto.IdFactura || idProducto != facturaProducto.IdProducto)
            {
                return BadRequest("El ID de la URL no coincide con el ID del objeto");
            }

            var resultado = await _facturaProductoDAO.ActualizarAsync(facturaProducto);
            if (!resultado)
            {
                return NotFound("No se pudo actualizar o no se encontró el factura producto");
            }

            return NoContent();
        }

        // POST: api/FacturaProductoes
        [HttpPost]
        public async Task<ActionResult<FacturaProducto>> PostFacturaProducto(FacturaProducto facturaProducto)
        {
            var resultado = await _facturaProductoDAO.InsertarAsync(facturaProducto);
            if (!resultado)
            {
                return BadRequest("No se pudo insertar el factura producto");
            }

            return CreatedAtAction("GetFacturaProducto", 
                new { idFactura = facturaProducto.IdFactura, idProducto = facturaProducto.IdProducto }, 
                facturaProducto);
        }

        // DELETE: api/FacturaProductoes/5/3
        [HttpDelete("{idFactura}/{idProducto}")]
        public async Task<IActionResult> DeleteFacturaProducto(int idFactura, int idProducto)
        {
            var resultado = await _facturaProductoDAO.EliminarAsync(idFactura, idProducto);
            if (!resultado)
            {
                return NotFound("No se pudo eliminar o no se encontró el factura producto");
            }

            return NoContent();
        }
    }
}
