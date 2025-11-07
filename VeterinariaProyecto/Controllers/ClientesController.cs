using Microsoft.AspNetCore.Mvc;
using VeterinariaProyecto.Models;
using VeterinariaProyecto.DAO;

namespace VeterinariaProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ClienteDAO _clienteDAO;

        public ClientesController()
        {
            _clienteDAO = new ClienteDAO();
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            var clientes = await _clienteDAO.ObtenerTodosAsync();
            return Ok(clientes);
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _clienteDAO.ObtenerPorIdAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        // PUT: api/Clientes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.IdCliente)
            {
                return BadRequest("El ID de la URL no coincide con el ID del objeto");
            }

            var resultado = await _clienteDAO.ActualizarAsync(cliente);
            if (!resultado)
            {
                return NotFound("No se pudo actualizar o no se encontró el cliente");
            }

            return NoContent();
        }

        // POST: api/Clientes
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            int nuevoId = await _clienteDAO.InsertarAsync(cliente);
            cliente.IdCliente = nuevoId;

            return CreatedAtAction("GetCliente", new { id = cliente.IdCliente }, cliente);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var resultado = await _clienteDAO.EliminarAsync(id);
            if (!resultado)
            {
                return NotFound("No se pudo eliminar o no se encontró el cliente");
            }

            return NoContent();
        }
    }
}