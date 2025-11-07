using Microsoft.Data.SqlClient;
using VeterinariaProyecto.Models;
using VeterinariaProyecto.Data;

namespace VeterinariaProyecto.DAO
{
    // En DAO/CargoDAO.cs
    

    public class CargoDAO
    {
        private readonly ConexionDB _conexionDB;

        public CargoDAO()
        {
            _conexionDB = new ConexionDB();
        }

        // --- OPERACIÓN READ (Obtener Todos) ---
        public async Task<List<Cargo>> ObtenerTodosAsync()
        {
            var cargos = new List<Cargo>();
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand("SELECT IdCargo, NombreCargo FROM Cargo", cn);

            await cn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                cargos.Add(new Cargo
                {
                    IdCargo = reader.GetInt32(reader.GetOrdinal("IdCargo")),
                    NombreCargo = reader.GetString(reader.GetOrdinal("NombreCargo"))
                });
            }
            return cargos;
        }

        // --- OPERACIÓN READ (Obtener por ID) ---
        public async Task<Cargo?> ObtenerPorIdAsync(int id)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand("SELECT IdCargo, NombreCargo FROM Cargo WHERE IdCargo = @id", cn);
            cmd.Parameters.AddWithValue("@id", id);

            await cn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Cargo
                {
                    IdCargo = reader.GetInt32(reader.GetOrdinal("IdCargo")),
                    NombreCargo = reader.GetString(reader.GetOrdinal("NombreCargo"))
                };
            }
            return null; // No se encontró
        }

        // --- OPERACIÓN CREATE (Insertar) ---
        public async Task<int> InsertarAsync(Cargo cargo)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "INSERT INTO Cargo (NombreCargo) VALUES (@nombreCargo);" +
                "SELECT SCOPE_IDENTITY();", cn);

            cmd.Parameters.AddWithValue("@nombreCargo", cargo.NombreCargo);

            await cn.OpenAsync();
            var id = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(id);
        }

        // --- OPERACIÓN UPDATE (Actualizar) ---
        public async Task<bool> ActualizarAsync(Cargo cargo)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "UPDATE Cargo SET NombreCargo = @nombreCargo WHERE IdCargo = @idCargo", cn);

            cmd.Parameters.AddWithValue("@nombreCargo", cargo.NombreCargo);
            cmd.Parameters.AddWithValue("@idCargo", cargo.IdCargo);

            await cn.OpenAsync();
            int filasAfectadas = await cmd.ExecuteNonQueryAsync();
            return filasAfectadas > 0; // Devuelve true si se actualizó
        }

        // --- OPERACIÓN DELETE (Eliminar) ---
        public async Task<bool> EliminarAsync(int id)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand("DELETE FROM Cargo WHERE IdCargo = @id", cn);
            cmd.Parameters.AddWithValue("@id", id);

            await cn.OpenAsync();
            int filasAfectadas = await cmd.ExecuteNonQueryAsync();
            return filasAfectadas > 0; // Devuelve true si se eliminó
        }
    }
}
