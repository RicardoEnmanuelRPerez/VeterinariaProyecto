using Microsoft.Data.SqlClient;
using VeterinariaProyecto.Models;
using VeterinariaProyecto.Data;

namespace VeterinariaProyecto.DAO
{
    public class MascotaDAO
    {
        private readonly ConexionDB _conexionDB;

        public MascotaDAO()
        {
            _conexionDB = new ConexionDB();
        }

        public async Task<List<Mascota>> ObtenerTodosAsync()
        {
            var mascotas = new List<Mascota>();
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "SELECT IdMascota, Nombre, IdCliente, IdEspecie, IdRaza, IdSexo, FechaNacimiento, Peso, Esterilizado FROM Mascota", cn);

            await cn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                mascotas.Add(new Mascota
                {
                    IdMascota = reader.GetInt32(reader.GetOrdinal("IdMascota")),
                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                    IdCliente = reader.GetInt32(reader.GetOrdinal("IdCliente")),
                    IdEspecie = reader.GetInt32(reader.GetOrdinal("IdEspecie")),
                    IdRaza = reader.GetInt32(reader.GetOrdinal("IdRaza")),
                    IdSexo = reader.GetInt32(reader.GetOrdinal("IdSexo")),
                    FechaNacimiento = reader.IsDBNull(reader.GetOrdinal("FechaNacimiento")) ? null : DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("FechaNacimiento"))),
                    Peso = reader.GetDecimal(reader.GetOrdinal("Peso")),
                    Esterilizado = reader.IsDBNull(reader.GetOrdinal("Esterilizado")) ? null : reader.GetBoolean(reader.GetOrdinal("Esterilizado"))
                });
            }
            return mascotas;
        }

        public async Task<Mascota?> ObtenerPorIdAsync(int id)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "SELECT IdMascota, Nombre, IdCliente, IdEspecie, IdRaza, IdSexo, FechaNacimiento, Peso, Esterilizado FROM Mascota WHERE IdMascota = @id", cn);
            cmd.Parameters.AddWithValue("@id", id);

            await cn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Mascota
                {
                    IdMascota = reader.GetInt32(reader.GetOrdinal("IdMascota")),
                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                    IdCliente = reader.GetInt32(reader.GetOrdinal("IdCliente")),
                    IdEspecie = reader.GetInt32(reader.GetOrdinal("IdEspecie")),
                    IdRaza = reader.GetInt32(reader.GetOrdinal("IdRaza")),
                    IdSexo = reader.GetInt32(reader.GetOrdinal("IdSexo")),
                    FechaNacimiento = reader.IsDBNull(reader.GetOrdinal("FechaNacimiento")) ? null : DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("FechaNacimiento"))),
                    Peso = reader.GetDecimal(reader.GetOrdinal("Peso")),
                    Esterilizado = reader.IsDBNull(reader.GetOrdinal("Esterilizado")) ? null : reader.GetBoolean(reader.GetOrdinal("Esterilizado"))
                };
            }
            return null;
        }

        public async Task<int> InsertarAsync(Mascota mascota)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "INSERT INTO Mascota (Nombre, IdCliente, IdEspecie, IdRaza, IdSexo, FechaNacimiento, Peso, Esterilizado) " +
                "VALUES (@nombre, @idCliente, @idEspecie, @idRaza, @idSexo, @fechaNacimiento, @peso, @esterilizado);" +
                "SELECT SCOPE_IDENTITY();", cn);

            cmd.Parameters.AddWithValue("@nombre", mascota.Nombre);
            cmd.Parameters.AddWithValue("@idCliente", mascota.IdCliente);
            cmd.Parameters.AddWithValue("@idEspecie", mascota.IdEspecie);
            cmd.Parameters.AddWithValue("@idRaza", mascota.IdRaza);
            cmd.Parameters.AddWithValue("@idSexo", mascota.IdSexo);
            cmd.Parameters.AddWithValue("@fechaNacimiento", (object)(mascota.FechaNacimiento?.ToDateTime(TimeOnly.MinValue)) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@peso", mascota.Peso);
            cmd.Parameters.AddWithValue("@esterilizado", (object)mascota.Esterilizado ?? DBNull.Value);

            await cn.OpenAsync();
            var id = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(id);
        }

        public async Task<bool> ActualizarAsync(Mascota mascota)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "UPDATE Mascota SET Nombre = @nombre, IdCliente = @idCliente, IdEspecie = @idEspecie, " +
                "IdRaza = @idRaza, IdSexo = @idSexo, FechaNacimiento = @fechaNacimiento, Peso = @peso, " +
                "Esterilizado = @esterilizado WHERE IdMascota = @idMascota", cn);

            cmd.Parameters.AddWithValue("@nombre", mascota.Nombre);
            cmd.Parameters.AddWithValue("@idCliente", mascota.IdCliente);
            cmd.Parameters.AddWithValue("@idEspecie", mascota.IdEspecie);
            cmd.Parameters.AddWithValue("@idRaza", mascota.IdRaza);
            cmd.Parameters.AddWithValue("@idSexo", mascota.IdSexo);
            cmd.Parameters.AddWithValue("@fechaNacimiento", (object)(mascota.FechaNacimiento?.ToDateTime(TimeOnly.MinValue)) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@peso", mascota.Peso);
            cmd.Parameters.AddWithValue("@esterilizado", (object)mascota.Esterilizado ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@idMascota", mascota.IdMascota);

            await cn.OpenAsync();
            int filasAfectadas = await cmd.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand("DELETE FROM Mascota WHERE IdMascota = @id", cn);
            cmd.Parameters.AddWithValue("@id", id);

            await cn.OpenAsync();
            int filasAfectadas = await cmd.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }
    }
}

