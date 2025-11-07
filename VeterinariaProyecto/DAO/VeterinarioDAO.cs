using Microsoft.Data.SqlClient;
using VeterinariaProyecto.Models;
using VeterinariaProyecto.Data;

namespace VeterinariaProyecto.DAO
{
    public class VeterinarioDAO
    {
        private readonly ConexionDB _conexionDB;

        public VeterinarioDAO()
        {
            _conexionDB = new ConexionDB();
        }

        public async Task<List<Veterinario>> ObtenerTodosAsync()
        {
            var veterinarios = new List<Veterinario>();
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "SELECT IdVeterinario, Nombre, Especialidad, Telefono, Correo, NumeroColegiatura, Activo FROM Veterinario", cn);

            await cn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                veterinarios.Add(new Veterinario
                {
                    IdVeterinario = reader.GetInt32(reader.GetOrdinal("IdVeterinario")),
                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                    Especialidad = reader.IsDBNull(reader.GetOrdinal("Especialidad")) ? null : reader.GetString(reader.GetOrdinal("Especialidad")),
                    Telefono = reader.IsDBNull(reader.GetOrdinal("Telefono")) ? null : reader.GetString(reader.GetOrdinal("Telefono")),
                    Correo = reader.IsDBNull(reader.GetOrdinal("Correo")) ? null : reader.GetString(reader.GetOrdinal("Correo")),
                    NumeroColegiatura = reader.IsDBNull(reader.GetOrdinal("NumeroColegiatura")) ? null : reader.GetString(reader.GetOrdinal("NumeroColegiatura")),
                    Activo = reader.GetBoolean(reader.GetOrdinal("Activo"))
                });
            }
            return veterinarios;
        }

        public async Task<Veterinario?> ObtenerPorIdAsync(int id)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "SELECT IdVeterinario, Nombre, Especialidad, Telefono, Correo, NumeroColegiatura, Activo FROM Veterinario WHERE IdVeterinario = @id", cn);
            cmd.Parameters.AddWithValue("@id", id);

            await cn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Veterinario
                {
                    IdVeterinario = reader.GetInt32(reader.GetOrdinal("IdVeterinario")),
                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                    Especialidad = reader.IsDBNull(reader.GetOrdinal("Especialidad")) ? null : reader.GetString(reader.GetOrdinal("Especialidad")),
                    Telefono = reader.IsDBNull(reader.GetOrdinal("Telefono")) ? null : reader.GetString(reader.GetOrdinal("Telefono")),
                    Correo = reader.IsDBNull(reader.GetOrdinal("Correo")) ? null : reader.GetString(reader.GetOrdinal("Correo")),
                    NumeroColegiatura = reader.IsDBNull(reader.GetOrdinal("NumeroColegiatura")) ? null : reader.GetString(reader.GetOrdinal("NumeroColegiatura")),
                    Activo = reader.GetBoolean(reader.GetOrdinal("Activo"))
                };
            }
            return null;
        }

        public async Task<int> InsertarAsync(Veterinario veterinario)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "INSERT INTO Veterinario (Nombre, Especialidad, Telefono, Correo, NumeroColegiatura, Activo) " +
                "VALUES (@nombre, @especialidad, @telefono, @correo, @numeroColegiatura, @activo);" +
                "SELECT SCOPE_IDENTITY();", cn);

            cmd.Parameters.AddWithValue("@nombre", veterinario.Nombre);
            cmd.Parameters.AddWithValue("@especialidad", (object)veterinario.Especialidad ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@telefono", (object)veterinario.Telefono ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@correo", (object)veterinario.Correo ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@numeroColegiatura", (object)veterinario.NumeroColegiatura ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@activo", veterinario.Activo);

            await cn.OpenAsync();
            var id = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(id);
        }

        public async Task<bool> ActualizarAsync(Veterinario veterinario)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "UPDATE Veterinario SET Nombre = @nombre, Especialidad = @especialidad, Telefono = @telefono, " +
                "Correo = @correo, NumeroColegiatura = @numeroColegiatura, Activo = @activo " +
                "WHERE IdVeterinario = @idVeterinario", cn);

            cmd.Parameters.AddWithValue("@nombre", veterinario.Nombre);
            cmd.Parameters.AddWithValue("@especialidad", (object)veterinario.Especialidad ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@telefono", (object)veterinario.Telefono ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@correo", (object)veterinario.Correo ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@numeroColegiatura", (object)veterinario.NumeroColegiatura ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@activo", veterinario.Activo);
            cmd.Parameters.AddWithValue("@idVeterinario", veterinario.IdVeterinario);

            await cn.OpenAsync();
            int filasAfectadas = await cmd.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand("DELETE FROM Veterinario WHERE IdVeterinario = @id", cn);
            cmd.Parameters.AddWithValue("@id", id);

            await cn.OpenAsync();
            int filasAfectadas = await cmd.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }
    }
}

