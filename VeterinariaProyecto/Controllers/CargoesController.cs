// En Controllers/CargoesController.cs (CÓDIGO NUEVO)
using Microsoft.AspNetCore.Mvc;
using VeterinariaProyecto.Models;
using VeterinariaProyecto.DAO; // <-- ¡NUEVO! Importa tu carpeta DAO

namespace VeterinariaProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoesController : ControllerBase
    {
        private readonly CargoDAO _cargoDAO; // <-- Depende del DAO

        public CargoesController() // <-- Constructor simplificado
        {
            _cargoDAO = new CargoDAO(); // <-- Crea el DAO manualmente
        }

        // GET: api/Cargoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cargo>>> GetCargoes()
        {
            var cargos = await _cargoDAO.ObtenerTodosAsync();
            return Ok(cargos);
        }

        // GET: api/Cargoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cargo>> GetCargo(int id)
        {
            var cargo = await _cargoDAO.ObtenerPorIdAsync(id);

            if (cargo == null)
            {
                return NotFound(); // Error 404 si no se encuentra
            }

            return Ok(cargo);
        }

        // PUT: api/Cargoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCargo(int id, Cargo cargo)
        {
            if (id != cargo.IdCargo)
            {
                return BadRequest("El ID de la URL no coincide con el ID del objeto");
            }

            var resultado = await _cargoDAO.ActualizarAsync(cargo);
            if (!resultado)
            {
                // Podríamos verificar si existe primero, pero por ahora esto funciona
                return NotFound("No se pudo actualizar o no se encontró el cargo");
            }

            return NoContent(); // Éxito 204
        }

        // POST: api/Cargoes
        [HttpPost]
        public async Task<ActionResult<Cargo>> PostCargo(Cargo cargo)
        {
            int nuevoId = await _cargoDAO.InsertarAsync(cargo);
            cargo.IdCargo = nuevoId;

            return CreatedAtAction("GetCargo", new { id = cargo.IdCargo }, cargo); // Éxito 201
        }

        // DELETE: api/Cargoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCargo(int id)
        {
            var resultado = await _cargoDAO.EliminarAsync(id);
            if (!resultado)
            {
                return NotFound("No se pudo eliminar o no se encontró el cargo");
            }

            return NoContent(); // Éxito 204
        }
    }
}