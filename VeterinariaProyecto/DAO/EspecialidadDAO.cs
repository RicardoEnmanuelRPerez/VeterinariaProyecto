using Microsoft.Data.SqlClient;
using VeterinariaProyecto.Models;
using VeterinariaProyecto.Data;

namespace VeterinariaProyecto.DAO
{
    public class EspecialidadDAO
    {
        private readonly ConexionDB _conexionDB;

        public EspecialidadDAO()
        {
            _conexionDB = new ConexionDB();
        }

        public async Task<List<Especialidad>> ObtenerTodosAsync()
        {
            var especialidades = new List<Especialidad>();
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand("SELECT IdEspecialidad, NombreEspecialidad FROM Especialidad", cn);

            await cn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                especialidades.Add(new Especialidad
                {
                    IdEspecialidad = reader.GetInt32(reader.GetOrdinal("IdEspecialidad")),
                    NombreEspecialidad = reader.GetString(reader.GetOrdinal("NombreEspecialidad"))
                });
            }
            return especialidades;
        }

        public async Task<Especialidad?> ObtenerPorIdAsync(int id)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand("SELECT IdEspecialidad, NombreEspecialidad FROM Especialidad WHERE IdEspecialidad = @id", cn);
            cmd.Parameters.AddWithValue("@id", id);

            await cn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Especialidad
                {
                    IdEspecialidad = reader.GetInt32(reader.GetOrdinal("IdEspecialidad")),
                    NombreEspecialidad = reader.GetString(reader.GetOrdinal("NombreEspecialidad"))
                };
            }
            return null;
        }

        public async Task<int> InsertarAsync(Especialidad especialidad)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "INSERT INTO Especialidad (NombreEspecialidad) VALUES (@nombreEspecialidad);" +
                "SELECT SCOPE_IDENTITY();", cn);

            cmd.Parameters.AddWithValue("@nombreEspecialidad", especialidad.NombreEspecialidad);

            await cn.OpenAsync();
            var id = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(id);
        }

        public async Task<bool> ActualizarAsync(Especialidad especialidad)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "UPDATE Especialidad SET NombreEspecialidad = @nombreEspecialidad WHERE IdEspecialidad = @idEspecialidad", cn);

            cmd.Parameters.AddWithValue("@nombreEspecialidad", especialidad.NombreEspecialidad);
            cmd.Parameters.AddWithValue("@idEspecialidad", especialidad.IdEspecialidad);

            await cn.OpenAsync();
            int filasAfectadas = await cmd.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand("DELETE FROM Especialidad WHERE IdEspecialidad = @id", cn);
            cmd.Parameters.AddWithValue("@id", id);

            await cn.OpenAsync();
            int filasAfectadas = await cmd.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }
    }
}

