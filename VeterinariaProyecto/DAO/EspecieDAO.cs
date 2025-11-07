using Microsoft.Data.SqlClient;
using VeterinariaProyecto.Models;
using VeterinariaProyecto.Data;

namespace VeterinariaProyecto.DAO
{
    public class EspecieDAO
    {
        private readonly ConexionDB _conexionDB;

        public EspecieDAO()
        {
            _conexionDB = new ConexionDB();
        }

        public async Task<List<Especie>> ObtenerTodosAsync()
        {
            var especies = new List<Especie>();
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand("SELECT IdEspecie, NombreEspecie FROM Especie", cn);

            await cn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                especies.Add(new Especie
                {
                    IdEspecie = reader.GetInt32(reader.GetOrdinal("IdEspecie")),
                    NombreEspecie = reader.GetString(reader.GetOrdinal("NombreEspecie"))
                });
            }
            return especies;
        }

        public async Task<Especie?> ObtenerPorIdAsync(int id)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand("SELECT IdEspecie, NombreEspecie FROM Especie WHERE IdEspecie = @id", cn);
            cmd.Parameters.AddWithValue("@id", id);

            await cn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Especie
                {
                    IdEspecie = reader.GetInt32(reader.GetOrdinal("IdEspecie")),
                    NombreEspecie = reader.GetString(reader.GetOrdinal("NombreEspecie"))
                };
            }
            return null;
        }

        public async Task<int> InsertarAsync(Especie especie)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "INSERT INTO Especie (NombreEspecie) VALUES (@nombreEspecie);" +
                "SELECT SCOPE_IDENTITY();", cn);

            cmd.Parameters.AddWithValue("@nombreEspecie", especie.NombreEspecie);

            await cn.OpenAsync();
            var id = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(id);
        }

        public async Task<bool> ActualizarAsync(Especie especie)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "UPDATE Especie SET NombreEspecie = @nombreEspecie WHERE IdEspecie = @idEspecie", cn);

            cmd.Parameters.AddWithValue("@nombreEspecie", especie.NombreEspecie);
            cmd.Parameters.AddWithValue("@idEspecie", especie.IdEspecie);

            await cn.OpenAsync();
            int filasAfectadas = await cmd.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand("DELETE FROM Especie WHERE IdEspecie = @id", cn);
            cmd.Parameters.AddWithValue("@id", id);

            await cn.OpenAsync();
            int filasAfectadas = await cmd.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }
    }
}

