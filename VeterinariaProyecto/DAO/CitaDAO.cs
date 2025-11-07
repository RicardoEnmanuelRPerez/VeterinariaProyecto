using Microsoft.Data.SqlClient;
using VeterinariaProyecto.Models;
using VeterinariaProyecto.Data;

namespace VeterinariaProyecto.DAO
{
    public class CitaDAO
    {
        private readonly ConexionDB _conexionDB;

        public CitaDAO()
        {
            _conexionDB = new ConexionDB();
        }

        public async Task<List<Cita>> ObtenerTodosAsync()
        {
            var citas = new List<Cita>();
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "SELECT IdCita, IdMascota, Fecha, Motivo, Diagnostico, IdTratamiento FROM Cita", cn);

            await cn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                citas.Add(new Cita
                {
                    IdCita = reader.GetInt32(reader.GetOrdinal("IdCita")),
                    IdMascota = reader.GetInt32(reader.GetOrdinal("IdMascota")),
                    Fecha = reader.GetDateTime(reader.GetOrdinal("Fecha")),
                    Motivo = reader.GetString(reader.GetOrdinal("Motivo")),
                    Diagnostico = reader.IsDBNull(reader.GetOrdinal("Diagnostico")) ? null : reader.GetString(reader.GetOrdinal("Diagnostico")),
                    IdTratamiento = reader.IsDBNull(reader.GetOrdinal("IdTratamiento")) ? null : reader.GetInt32(reader.GetOrdinal("IdTratamiento"))
                });
            }
            return citas;
        }

        public async Task<Cita?> ObtenerPorIdAsync(int id)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "SELECT IdCita, IdMascota, Fecha, Motivo, Diagnostico, IdTratamiento FROM Cita WHERE IdCita = @id", cn);
            cmd.Parameters.AddWithValue("@id", id);

            await cn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Cita
                {
                    IdCita = reader.GetInt32(reader.GetOrdinal("IdCita")),
                    IdMascota = reader.GetInt32(reader.GetOrdinal("IdMascota")),
                    Fecha = reader.GetDateTime(reader.GetOrdinal("Fecha")),
                    Motivo = reader.GetString(reader.GetOrdinal("Motivo")),
                    Diagnostico = reader.IsDBNull(reader.GetOrdinal("Diagnostico")) ? null : reader.GetString(reader.GetOrdinal("Diagnostico")),
                    IdTratamiento = reader.IsDBNull(reader.GetOrdinal("IdTratamiento")) ? null : reader.GetInt32(reader.GetOrdinal("IdTratamiento"))
                };
            }
            return null;
        }

        public async Task<int> InsertarAsync(Cita cita)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "INSERT INTO Cita (IdMascota, Fecha, Motivo, Diagnostico, IdTratamiento) " +
                "VALUES (@idMascota, @fecha, @motivo, @diagnostico, @idTratamiento);" +
                "SELECT SCOPE_IDENTITY();", cn);

            cmd.Parameters.AddWithValue("@idMascota", cita.IdMascota);
            cmd.Parameters.AddWithValue("@fecha", cita.Fecha);
            cmd.Parameters.AddWithValue("@motivo", cita.Motivo);
            cmd.Parameters.AddWithValue("@diagnostico", (object)cita.Diagnostico ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@idTratamiento", (object)cita.IdTratamiento ?? DBNull.Value);

            await cn.OpenAsync();
            var id = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(id);
        }

        public async Task<bool> ActualizarAsync(Cita cita)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "UPDATE Cita SET IdMascota = @idMascota, Fecha = @fecha, Motivo = @motivo, " +
                "Diagnostico = @diagnostico, IdTratamiento = @idTratamiento WHERE IdCita = @idCita", cn);

            cmd.Parameters.AddWithValue("@idMascota", cita.IdMascota);
            cmd.Parameters.AddWithValue("@fecha", cita.Fecha);
            cmd.Parameters.AddWithValue("@motivo", cita.Motivo);
            cmd.Parameters.AddWithValue("@diagnostico", (object)cita.Diagnostico ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@idTratamiento", (object)cita.IdTratamiento ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@idCita", cita.IdCita);

            await cn.OpenAsync();
            int filasAfectadas = await cmd.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand("DELETE FROM Cita WHERE IdCita = @id", cn);
            cmd.Parameters.AddWithValue("@id", id);

            await cn.OpenAsync();
            int filasAfectadas = await cmd.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }
    }
}

