using Microsoft.Data.SqlClient;
using VeterinariaProyecto.Models;
using VeterinariaProyecto.Data;

namespace VeterinariaProyecto.DAO
{
    public class SexoDAO
    {
        private readonly ConexionDB _conexionDB;

        public SexoDAO()
        {
            _conexionDB = new ConexionDB();
        }

        public async Task<List<Sexo>> ObtenerTodosAsync()
        {
            var sexos = new List<Sexo>();
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand("SELECT IdSexo, NombreSexo FROM Sexo", cn);

            await cn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                sexos.Add(new Sexo
                {
                    IdSexo = reader.GetInt32(reader.GetOrdinal("IdSexo")),
                    NombreSexo = reader.GetString(reader.GetOrdinal("NombreSexo"))
                });
            }
            return sexos;
        }

        public async Task<Sexo?> ObtenerPorIdAsync(int id)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand("SELECT IdSexo, NombreSexo FROM Sexo WHERE IdSexo = @id", cn);
            cmd.Parameters.AddWithValue("@id", id);

            await cn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Sexo
                {
                    IdSexo = reader.GetInt32(reader.GetOrdinal("IdSexo")),
                    NombreSexo = reader.GetString(reader.GetOrdinal("NombreSexo"))
                };
            }
            return null;
        }

        public async Task<int> InsertarAsync(Sexo sexo)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "INSERT INTO Sexo (NombreSexo) VALUES (@nombreSexo);" +
                "SELECT SCOPE_IDENTITY();", cn);

            cmd.Parameters.AddWithValue("@nombreSexo", sexo.NombreSexo);

            await cn.OpenAsync();
            var id = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(id);
        }

        public async Task<bool> ActualizarAsync(Sexo sexo)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "UPDATE Sexo SET NombreSexo = @nombreSexo WHERE IdSexo = @idSexo", cn);

            cmd.Parameters.AddWithValue("@nombreSexo", sexo.NombreSexo);
            cmd.Parameters.AddWithValue("@idSexo", sexo.IdSexo);

            await cn.OpenAsync();
            int filasAfectadas = await cmd.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand("DELETE FROM Sexo WHERE IdSexo = @id", cn);
            cmd.Parameters.AddWithValue("@id", id);

            await cn.OpenAsync();
            int filasAfectadas = await cmd.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }
    }
}

