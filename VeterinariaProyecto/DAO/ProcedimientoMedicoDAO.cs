using Microsoft.Data.SqlClient;
using VeterinariaProyecto.Models;
using VeterinariaProyecto.Data;

namespace VeterinariaProyecto.DAO
{
    public class ProcedimientoMedicoDAO
    {
        private readonly ConexionDB _conexionDB;

        public ProcedimientoMedicoDAO()
        {
            _conexionDB = new ConexionDB();
        }

        public async Task<List<ProcedimientoMedico>> ObtenerTodosAsync()
        {
            var procedimientos = new List<ProcedimientoMedico>();
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "SELECT IdProcedimiento, Tipo, FechaInicio, FechaFin, Descripcion, IdHistoria, IdVeterinario, " +
                "Estado, CostoEstimado, Observaciones, Sala, Cama, Quirofano, DuracionEstimada, TipoAnestesia " +
                "FROM ProcedimientoMedico", cn);

            await cn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                procedimientos.Add(new ProcedimientoMedico
                {
                    IdProcedimiento = reader.GetInt32(reader.GetOrdinal("IdProcedimiento")),
                    Tipo = reader.GetString(reader.GetOrdinal("Tipo")),
                    FechaInicio = reader.GetDateTime(reader.GetOrdinal("FechaInicio")),
                    FechaFin = reader.IsDBNull(reader.GetOrdinal("FechaFin")) ? null : reader.GetDateTime(reader.GetOrdinal("FechaFin")),
                    Descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion")) ? null : reader.GetString(reader.GetOrdinal("Descripcion")),
                    IdHistoria = reader.GetInt32(reader.GetOrdinal("IdHistoria")),
                    IdVeterinario = reader.GetInt32(reader.GetOrdinal("IdVeterinario")),
                    Estado = reader.GetString(reader.GetOrdinal("Estado")),
                    CostoEstimado = reader.IsDBNull(reader.GetOrdinal("CostoEstimado")) ? null : reader.GetDecimal(reader.GetOrdinal("CostoEstimado")),
                    Observaciones = reader.IsDBNull(reader.GetOrdinal("Observaciones")) ? null : reader.GetString(reader.GetOrdinal("Observaciones")),
                    Sala = reader.IsDBNull(reader.GetOrdinal("Sala")) ? null : reader.GetString(reader.GetOrdinal("Sala")),
                    Cama = reader.IsDBNull(reader.GetOrdinal("Cama")) ? null : reader.GetString(reader.GetOrdinal("Cama")),
                    Quirofano = reader.IsDBNull(reader.GetOrdinal("Quirofano")) ? null : reader.GetString(reader.GetOrdinal("Quirofano")),
                    DuracionEstimada = reader.IsDBNull(reader.GetOrdinal("DuracionEstimada")) ? null : reader.GetInt32(reader.GetOrdinal("DuracionEstimada")),
                    TipoAnestesia = reader.IsDBNull(reader.GetOrdinal("TipoAnestesia")) ? null : reader.GetString(reader.GetOrdinal("TipoAnestesia"))
                });
            }
            return procedimientos;
        }

        public async Task<ProcedimientoMedico?> ObtenerPorIdAsync(int id)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "SELECT IdProcedimiento, Tipo, FechaInicio, FechaFin, Descripcion, IdHistoria, IdVeterinario, " +
                "Estado, CostoEstimado, Observaciones, Sala, Cama, Quirofano, DuracionEstimada, TipoAnestesia " +
                "FROM ProcedimientoMedico WHERE IdProcedimiento = @id", cn);
            cmd.Parameters.AddWithValue("@id", id);

            await cn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new ProcedimientoMedico
                {
                    IdProcedimiento = reader.GetInt32(reader.GetOrdinal("IdProcedimiento")),
                    Tipo = reader.GetString(reader.GetOrdinal("Tipo")),
                    FechaInicio = reader.GetDateTime(reader.GetOrdinal("FechaInicio")),
                    FechaFin = reader.IsDBNull(reader.GetOrdinal("FechaFin")) ? null : reader.GetDateTime(reader.GetOrdinal("FechaFin")),
                    Descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion")) ? null : reader.GetString(reader.GetOrdinal("Descripcion")),
                    IdHistoria = reader.GetInt32(reader.GetOrdinal("IdHistoria")),
                    IdVeterinario = reader.GetInt32(reader.GetOrdinal("IdVeterinario")),
                    Estado = reader.GetString(reader.GetOrdinal("Estado")),
                    CostoEstimado = reader.IsDBNull(reader.GetOrdinal("CostoEstimado")) ? null : reader.GetDecimal(reader.GetOrdinal("CostoEstimado")),
                    Observaciones = reader.IsDBNull(reader.GetOrdinal("Observaciones")) ? null : reader.GetString(reader.GetOrdinal("Observaciones")),
                    Sala = reader.IsDBNull(reader.GetOrdinal("Sala")) ? null : reader.GetString(reader.GetOrdinal("Sala")),
                    Cama = reader.IsDBNull(reader.GetOrdinal("Cama")) ? null : reader.GetString(reader.GetOrdinal("Cama")),
                    Quirofano = reader.IsDBNull(reader.GetOrdinal("Quirofano")) ? null : reader.GetString(reader.GetOrdinal("Quirofano")),
                    DuracionEstimada = reader.IsDBNull(reader.GetOrdinal("DuracionEstimada")) ? null : reader.GetInt32(reader.GetOrdinal("DuracionEstimada")),
                    TipoAnestesia = reader.IsDBNull(reader.GetOrdinal("TipoAnestesia")) ? null : reader.GetString(reader.GetOrdinal("TipoAnestesia"))
                };
            }
            return null;
        }

        public async Task<int> InsertarAsync(ProcedimientoMedico procedimiento)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "INSERT INTO ProcedimientoMedico (Tipo, FechaInicio, FechaFin, Descripcion, IdHistoria, IdVeterinario, " +
                "Estado, CostoEstimado, Observaciones, Sala, Cama, Quirofano, DuracionEstimada, TipoAnestesia) " +
                "VALUES (@tipo, @fechaInicio, @fechaFin, @descripcion, @idHistoria, @idVeterinario, " +
                "@estado, @costoEstimado, @observaciones, @sala, @cama, @quirofano, @duracionEstimada, @tipoAnestesia);" +
                "SELECT SCOPE_IDENTITY();", cn);

            cmd.Parameters.AddWithValue("@tipo", procedimiento.Tipo);
            cmd.Parameters.AddWithValue("@fechaInicio", procedimiento.FechaInicio);
            cmd.Parameters.AddWithValue("@fechaFin", (object)procedimiento.FechaFin ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@descripcion", (object)procedimiento.Descripcion ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@idHistoria", procedimiento.IdHistoria);
            cmd.Parameters.AddWithValue("@idVeterinario", procedimiento.IdVeterinario);
            cmd.Parameters.AddWithValue("@estado", procedimiento.Estado);
            cmd.Parameters.AddWithValue("@costoEstimado", (object)procedimiento.CostoEstimado ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@observaciones", (object)procedimiento.Observaciones ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@sala", (object)procedimiento.Sala ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@cama", (object)procedimiento.Cama ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@quirofano", (object)procedimiento.Quirofano ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@duracionEstimada", (object)procedimiento.DuracionEstimada ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@tipoAnestesia", (object)procedimiento.TipoAnestesia ?? DBNull.Value);

            await cn.OpenAsync();
            var id = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(id);
        }

        public async Task<bool> ActualizarAsync(ProcedimientoMedico procedimiento)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "UPDATE ProcedimientoMedico SET Tipo = @tipo, FechaInicio = @fechaInicio, FechaFin = @fechaFin, " +
                "Descripcion = @descripcion, IdHistoria = @idHistoria, IdVeterinario = @idVeterinario, " +
                "Estado = @estado, CostoEstimado = @costoEstimado, Observaciones = @observaciones, " +
                "Sala = @sala, Cama = @cama, Quirofano = @quirofano, DuracionEstimada = @duracionEstimada, " +
                "TipoAnestesia = @tipoAnestesia WHERE IdProcedimiento = @idProcedimiento", cn);

            cmd.Parameters.AddWithValue("@tipo", procedimiento.Tipo);
            cmd.Parameters.AddWithValue("@fechaInicio", procedimiento.FechaInicio);
            cmd.Parameters.AddWithValue("@fechaFin", (object)procedimiento.FechaFin ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@descripcion", (object)procedimiento.Descripcion ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@idHistoria", procedimiento.IdHistoria);
            cmd.Parameters.AddWithValue("@idVeterinario", procedimiento.IdVeterinario);
            cmd.Parameters.AddWithValue("@estado", procedimiento.Estado);
            cmd.Parameters.AddWithValue("@costoEstimado", (object)procedimiento.CostoEstimado ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@observaciones", (object)procedimiento.Observaciones ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@sala", (object)procedimiento.Sala ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@cama", (object)procedimiento.Cama ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@quirofano", (object)procedimiento.Quirofano ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@duracionEstimada", (object)procedimiento.DuracionEstimada ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@tipoAnestesia", (object)procedimiento.TipoAnestesia ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@idProcedimiento", procedimiento.IdProcedimiento);

            await cn.OpenAsync();
            int filasAfectadas = await cmd.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand("DELETE FROM ProcedimientoMedico WHERE IdProcedimiento = @id", cn);
            cmd.Parameters.AddWithValue("@id", id);

            await cn.OpenAsync();
            int filasAfectadas = await cmd.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }
    }
}

