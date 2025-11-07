using Microsoft.Data.SqlClient;
using VeterinariaProyecto.Models;
using VeterinariaProyecto.Data;

namespace VeterinariaProyecto.DAO
{
    public class HistoriaClinicaDAO
    {
        private readonly ConexionDB _conexionDB;

        public HistoriaClinicaDAO()
        {
            _conexionDB = new ConexionDB();
        }

        public async Task<List<HistoriaClinica>> ObtenerTodosAsync()
        {
            var historiasClinicas = new List<HistoriaClinica>();
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "SELECT IdHistoria, FechaApertura, Observaciones, IdMascota FROM HistoriaClinica", cn);

            await cn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                historiasClinicas.Add(new HistoriaClinica
                {
                    IdHistoria = reader.GetInt32(reader.GetOrdinal("IdHistoria")),
                    FechaApertura = reader.GetDateTime(reader.GetOrdinal("FechaApertura")),
                    Observaciones = reader.IsDBNull(reader.GetOrdinal("Observaciones")) ? null : reader.GetString(reader.GetOrdinal("Observaciones")),
                    IdMascota = reader.GetInt32(reader.GetOrdinal("IdMascota"))
                });
            }
            return historiasClinicas;
        }

        public async Task<HistoriaClinica?> ObtenerPorIdAsync(int id)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "SELECT IdHistoria, FechaApertura, Observaciones, IdMascota FROM HistoriaClinica WHERE IdHistoria = @id", cn);
            cmd.Parameters.AddWithValue("@id", id);

            await cn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new HistoriaClinica
                {
                    IdHistoria = reader.GetInt32(reader.GetOrdinal("IdHistoria")),
                    FechaApertura = reader.GetDateTime(reader.GetOrdinal("FechaApertura")),
                    Observaciones = reader.IsDBNull(reader.GetOrdinal("Observaciones")) ? null : reader.GetString(reader.GetOrdinal("Observaciones")),
                    IdMascota = reader.GetInt32(reader.GetOrdinal("IdMascota"))
                };
            }
            return null;
        }

        public async Task<HistoriaClinica?> ObtenerPorMascotaAsync(int idMascota)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "SELECT IdHistoria, FechaApertura, Observaciones, IdMascota FROM HistoriaClinica WHERE IdMascota = @idMascota", cn);
            cmd.Parameters.AddWithValue("@idMascota", idMascota);

            await cn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new HistoriaClinica
                {
                    IdHistoria = reader.GetInt32(reader.GetOrdinal("IdHistoria")),
                    FechaApertura = reader.GetDateTime(reader.GetOrdinal("FechaApertura")),
                    Observaciones = reader.IsDBNull(reader.GetOrdinal("Observaciones")) ? null : reader.GetString(reader.GetOrdinal("Observaciones")),
                    IdMascota = reader.GetInt32(reader.GetOrdinal("IdMascota"))
                };
            }
            return null;
        }

        public async Task<int> InsertarAsync(HistoriaClinica historiaClinica)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "INSERT INTO HistoriaClinica (FechaApertura, Observaciones, IdMascota) " +
                "VALUES (@fechaApertura, @observaciones, @idMascota);" +
                "SELECT SCOPE_IDENTITY();", cn);

            cmd.Parameters.AddWithValue("@fechaApertura", historiaClinica.FechaApertura);
            cmd.Parameters.AddWithValue("@observaciones", (object)historiaClinica.Observaciones ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@idMascota", historiaClinica.IdMascota);

            await cn.OpenAsync();
            var id = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(id);
        }

        public async Task<bool> ActualizarAsync(HistoriaClinica historiaClinica)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "UPDATE HistoriaClinica SET FechaApertura = @fechaApertura, Observaciones = @observaciones, " +
                "IdMascota = @idMascota WHERE IdHistoria = @idHistoria", cn);

            cmd.Parameters.AddWithValue("@fechaApertura", historiaClinica.FechaApertura);
            cmd.Parameters.AddWithValue("@observaciones", (object)historiaClinica.Observaciones ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@idMascota", historiaClinica.IdMascota);
            cmd.Parameters.AddWithValue("@idHistoria", historiaClinica.IdHistoria);

            await cn.OpenAsync();
            int filasAfectadas = await cmd.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand("DELETE FROM HistoriaClinica WHERE IdHistoria = @id", cn);
            cmd.Parameters.AddWithValue("@id", id);

            await cn.OpenAsync();
            int filasAfectadas = await cmd.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }
    }
}

