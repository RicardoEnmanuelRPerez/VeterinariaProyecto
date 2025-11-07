using Microsoft.Data.SqlClient;
using VeterinariaProyecto.Models;
using VeterinariaProyecto.Data;

namespace VeterinariaProyecto.DAO
{
    public class TratamientoDAO
    {
        private readonly ConexionDB _conexionDB;

        public TratamientoDAO()
        {
            _conexionDB = new ConexionDB();
        }

        public async Task<List<Tratamiento>> ObtenerTodosAsync()
        {
            var tratamientos = new List<Tratamiento>();
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand("SELECT IdTratamiento, NombreTratamiento, Descripcion FROM Tratamiento", cn);

            await cn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                tratamientos.Add(new Tratamiento
                {
                    IdTratamiento = reader.GetInt32(reader.GetOrdinal("IdTratamiento")),
                    NombreTratamiento = reader.GetString(reader.GetOrdinal("NombreTratamiento")),
                    Descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion")) ? null : reader.GetString(reader.GetOrdinal("Descripcion"))
                });
            }
            return tratamientos;
        }

        public async Task<Tratamiento?> ObtenerPorIdAsync(int id)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand("SELECT IdTratamiento, NombreTratamiento, Descripcion FROM Tratamiento WHERE IdTratamiento = @id", cn);
            cmd.Parameters.AddWithValue("@id", id);

            await cn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Tratamiento
                {
                    IdTratamiento = reader.GetInt32(reader.GetOrdinal("IdTratamiento")),
                    NombreTratamiento = reader.GetString(reader.GetOrdinal("NombreTratamiento")),
                    Descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion")) ? null : reader.GetString(reader.GetOrdinal("Descripcion"))
                };
            }
            return null;
        }

        public async Task<int> InsertarAsync(Tratamiento tratamiento)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "INSERT INTO Tratamiento (NombreTratamiento, Descripcion) VALUES (@nombreTratamiento, @descripcion);" +
                "SELECT SCOPE_IDENTITY();", cn);

            cmd.Parameters.AddWithValue("@nombreTratamiento", tratamiento.NombreTratamiento);
            cmd.Parameters.AddWithValue("@descripcion", (object)tratamiento.Descripcion ?? DBNull.Value);

            await cn.OpenAsync();
            var id = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(id);
        }

        public async Task<bool> ActualizarAsync(Tratamiento tratamiento)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "UPDATE Tratamiento SET NombreTratamiento = @nombreTratamiento, Descripcion = @descripcion WHERE IdTratamiento = @idTratamiento", cn);

            cmd.Parameters.AddWithValue("@nombreTratamiento", tratamiento.NombreTratamiento);
            cmd.Parameters.AddWithValue("@descripcion", (object)tratamiento.Descripcion ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@idTratamiento", tratamiento.IdTratamiento);

            await cn.OpenAsync();
            int filasAfectadas = await cmd.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand("DELETE FROM Tratamiento WHERE IdTratamiento = @id", cn);
            cmd.Parameters.AddWithValue("@id", id);

            await cn.OpenAsync();
            int filasAfectadas = await cmd.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }
    }
}

