using Microsoft.AspNetCore.Mvc;
using VeterinariaProyecto.Models;
using VeterinariaProyecto.DAO;

namespace VeterinariaProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoesController : ControllerBase
    {
        private readonly ProductoDAO _productoDAO;

        public ProductoesController()
        {
            _productoDAO = new ProductoDAO();
        }

        // GET: api/Productoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            var productos = await _productoDAO.ObtenerTodosAsync();
            return Ok(productos);
        }

        // GET: api/Productoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _productoDAO.ObtenerPorIdAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
        }

        // PUT: api/Productoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, Producto producto)
        {
            if (id != producto.IdProducto)
            {
                return BadRequest("El ID de la URL no coincide con el ID del objeto");
            }

            var resultado = await _productoDAO.ActualizarAsync(producto);
            if (!resultado)
            {
                return NotFound("No se pudo actualizar o no se encontró el producto");
            }

            return NoContent();
        }

        // POST: api/Productoes
        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            int nuevoId = await _productoDAO.InsertarAsync(producto);
            producto.IdProducto = nuevoId;

            return CreatedAtAction("GetProducto", new { id = producto.IdProducto }, producto);
        }

        // DELETE: api/Productoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var resultado = await _productoDAO.EliminarAsync(id);
            if (!resultado)
            {
                return NotFound("No se pudo eliminar o no se encontró el producto");
            }

            return NoContent();
        }
    }
}
