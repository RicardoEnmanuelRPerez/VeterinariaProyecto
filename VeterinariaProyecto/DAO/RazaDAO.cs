using Microsoft.Data.SqlClient;
using VeterinariaProyecto.Models;
using VeterinariaProyecto.Data;

namespace VeterinariaProyecto.DAO
{
    public class RazaDAO
    {
        private readonly ConexionDB _conexionDB;

        public RazaDAO()
        {
            _conexionDB = new ConexionDB();
        }

        public async Task<List<Raza>> ObtenerTodosAsync()
        {
            var razas = new List<Raza>();
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand("SELECT IdRaza, NombreRaza, IdEspecie FROM Raza", cn);

            await cn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                razas.Add(new Raza
                {
                    IdRaza = reader.GetInt32(reader.GetOrdinal("IdRaza")),
                    NombreRaza = reader.GetString(reader.GetOrdinal("NombreRaza")),
                    IdEspecie = reader.GetInt32(reader.GetOrdinal("IdEspecie"))
                });
            }
            return razas;
        }

        public async Task<Raza?> ObtenerPorIdAsync(int id)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand("SELECT IdRaza, NombreRaza, IdEspecie FROM Raza WHERE IdRaza = @id", cn);
            cmd.Parameters.AddWithValue("@id", id);

            await cn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Raza
                {
                    IdRaza = reader.GetInt32(reader.GetOrdinal("IdRaza")),
                    NombreRaza = reader.GetString(reader.GetOrdinal("NombreRaza")),
                    IdEspecie = reader.GetInt32(reader.GetOrdinal("IdEspecie"))
                };
            }
            return null;
        }

        public async Task<int> InsertarAsync(Raza raza)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "INSERT INTO Raza (NombreRaza, IdEspecie) VALUES (@nombreRaza, @idEspecie);" +
                "SELECT SCOPE_IDENTITY();", cn);

            cmd.Parameters.AddWithValue("@nombreRaza", raza.NombreRaza);
            cmd.Parameters.AddWithValue("@idEspecie", raza.IdEspecie);

            await cn.OpenAsync();
            var id = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(id);
        }

        public async Task<bool> ActualizarAsync(Raza raza)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "UPDATE Raza SET NombreRaza = @nombreRaza, IdEspecie = @idEspecie WHERE IdRaza = @idRaza", cn);

            cmd.Parameters.AddWithValue("@nombreRaza", raza.NombreRaza);
            cmd.Parameters.AddWithValue("@idEspecie", raza.IdEspecie);
            cmd.Parameters.AddWithValue("@idRaza", raza.IdRaza);

            await cn.OpenAsync();
            int filasAfectadas = await cmd.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand("DELETE FROM Raza WHERE IdRaza = @id", cn);
            cmd.Parameters.AddWithValue("@id", id);

            await cn.OpenAsync();
            int filasAfectadas = await cmd.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }
    }
}

