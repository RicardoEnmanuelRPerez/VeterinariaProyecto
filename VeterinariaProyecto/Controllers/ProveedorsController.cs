using Microsoft.AspNetCore.Mvc;
using VeterinariaProyecto.Models;
using VeterinariaProyecto.DAO;

namespace VeterinariaProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorsController : ControllerBase
    {
        private readonly ProveedorDAO _proveedorDAO;

        public ProveedorsController()
        {
            _proveedorDAO = new ProveedorDAO();
        }

        // GET: api/Proveedors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proveedor>>> GetProveedors()
        {
            var proveedores = await _proveedorDAO.ObtenerTodosAsync();
            return Ok(proveedores);
        }

        // GET: api/Proveedors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Proveedor>> GetProveedor(int id)
        {
            var proveedor = await _proveedorDAO.ObtenerPorIdAsync(id);

            if (proveedor == null)
            {
                return NotFound();
            }

            return Ok(proveedor);
        }

        // PUT: api/Proveedors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProveedor(int id, Proveedor proveedor)
        {
            if (id != proveedor.IdProveedor)
            {
                return BadRequest("El ID de la URL no coincide con el ID del objeto");
            }

            var resultado = await _proveedorDAO.ActualizarAsync(proveedor);
            if (!resultado)
            {
                return NotFound("No se pudo actualizar o no se encontró el proveedor");
            }

            return NoContent();
        }

        // POST: api/Proveedors
        [HttpPost]
        public async Task<ActionResult<Proveedor>> PostProveedor(Proveedor proveedor)
        {
            int nuevoId = await _proveedorDAO.InsertarAsync(proveedor);
            proveedor.IdProveedor = nuevoId;

            return CreatedAtAction("GetProveedor", new { id = proveedor.IdProveedor }, proveedor);
        }

        // DELETE: api/Proveedors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProveedor(int id)
        {
            var resultado = await _proveedorDAO.EliminarAsync(id);
            if (!resultado)
            {
                return NotFound("No se pudo eliminar o no se encontró el proveedor");
            }

            return NoContent();
        }
    }
}
