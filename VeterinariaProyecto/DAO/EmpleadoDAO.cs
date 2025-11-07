using Microsoft.Data.SqlClient;
using VeterinariaProyecto.Models;
using VeterinariaProyecto.Data;

namespace VeterinariaProyecto.DAO
{
    public class EmpleadoDAO
    {
        private readonly ConexionDB _conexionDB;

        public EmpleadoDAO()
        {
            _conexionDB = new ConexionDB();
        }

        public async Task<List<Empleado>> ObtenerTodosAsync()
        {
            var empleados = new List<Empleado>();
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "SELECT IdEmpleado, Nombres, Apellidos, IdEspecialidad, IdCargo, Telefono, Correo, " +
                "Direccion, HoraEntrada, HoraSalida, Estado FROM Empleado", cn);

            await cn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                empleados.Add(new Empleado
                {
                    IdEmpleado = reader.GetInt32(reader.GetOrdinal("IdEmpleado")),
                    Nombres = reader.GetString(reader.GetOrdinal("Nombres")),
                    Apellidos = reader.GetString(reader.GetOrdinal("Apellidos")),
                    IdEspecialidad = reader.IsDBNull(reader.GetOrdinal("IdEspecialidad")) ? null : reader.GetInt32(reader.GetOrdinal("IdEspecialidad")),
                    IdCargo = reader.IsDBNull(reader.GetOrdinal("IdCargo")) ? null : reader.GetInt32(reader.GetOrdinal("IdCargo")),
                    Telefono = reader.IsDBNull(reader.GetOrdinal("Telefono")) ? null : reader.GetString(reader.GetOrdinal("Telefono")),
                    Correo = reader.IsDBNull(reader.GetOrdinal("Correo")) ? null : reader.GetString(reader.GetOrdinal("Correo")),
                    Direccion = reader.GetString(reader.GetOrdinal("Direccion")),
                    HoraEntrada = TimeOnly.FromTimeSpan(reader.GetTimeSpan(reader.GetOrdinal("HoraEntrada"))),
                    HoraSalida = TimeOnly.FromTimeSpan(reader.GetTimeSpan(reader.GetOrdinal("HoraSalida"))),
                    Estado = reader.IsDBNull(reader.GetOrdinal("Estado")) ? null : reader.GetBoolean(reader.GetOrdinal("Estado"))
                });
            }
            return empleados;
        }

        public async Task<Empleado?> ObtenerPorIdAsync(int id)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "SELECT IdEmpleado, Nombres, Apellidos, IdEspecialidad, IdCargo, Telefono, Correo, " +
                "Direccion, HoraEntrada, HoraSalida, Estado FROM Empleado WHERE IdEmpleado = @id", cn);
            cmd.Parameters.AddWithValue("@id", id);

            await cn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Empleado
                {
                    IdEmpleado = reader.GetInt32(reader.GetOrdinal("IdEmpleado")),
                    Nombres = reader.GetString(reader.GetOrdinal("Nombres")),
                    Apellidos = reader.GetString(reader.GetOrdinal("Apellidos")),
                    IdEspecialidad = reader.IsDBNull(reader.GetOrdinal("IdEspecialidad")) ? null : reader.GetInt32(reader.GetOrdinal("IdEspecialidad")),
                    IdCargo = reader.IsDBNull(reader.GetOrdinal("IdCargo")) ? null : reader.GetInt32(reader.GetOrdinal("IdCargo")),
                    Telefono = reader.IsDBNull(reader.GetOrdinal("Telefono")) ? null : reader.GetString(reader.GetOrdinal("Telefono")),
                    Correo = reader.IsDBNull(reader.GetOrdinal("Correo")) ? null : reader.GetString(reader.GetOrdinal("Correo")),
                    Direccion = reader.GetString(reader.GetOrdinal("Direccion")),
                    HoraEntrada = TimeOnly.FromTimeSpan(reader.GetTimeSpan(reader.GetOrdinal("HoraEntrada"))),
                    HoraSalida = TimeOnly.FromTimeSpan(reader.GetTimeSpan(reader.GetOrdinal("HoraSalida"))),
                    Estado = reader.IsDBNull(reader.GetOrdinal("Estado")) ? null : reader.GetBoolean(reader.GetOrdinal("Estado"))
                };
            }
            return null;
        }

        public async Task<int> InsertarAsync(Empleado empleado)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "INSERT INTO Empleado (Nombres, Apellidos, IdEspecialidad, IdCargo, Telefono, Correo, " +
                "Direccion, HoraEntrada, HoraSalida, Estado) " +
                "VALUES (@nombres, @apellidos, @idEspecialidad, @idCargo, @telefono, @correo, " +
                "@direccion, @horaEntrada, @horaSalida, @estado);" +
                "SELECT SCOPE_IDENTITY();", cn);

            cmd.Parameters.AddWithValue("@nombres", empleado.Nombres);
            cmd.Parameters.AddWithValue("@apellidos", empleado.Apellidos);
            cmd.Parameters.AddWithValue("@idEspecialidad", (object)empleado.IdEspecialidad ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@idCargo", (object)empleado.IdCargo ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@telefono", (object)empleado.Telefono ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@correo", (object)empleado.Correo ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@direccion", empleado.Direccion);
            cmd.Parameters.AddWithValue("@horaEntrada", empleado.HoraEntrada.ToTimeSpan());
            cmd.Parameters.AddWithValue("@horaSalida", empleado.HoraSalida.ToTimeSpan());
            cmd.Parameters.AddWithValue("@estado", (object)empleado.Estado ?? DBNull.Value);

            await cn.OpenAsync();
            var id = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(id);
        }

        public async Task<bool> ActualizarAsync(Empleado empleado)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "UPDATE Empleado SET Nombres = @nombres, Apellidos = @apellidos, IdEspecialidad = @idEspecialidad, " +
                "IdCargo = @idCargo, Telefono = @telefono, Correo = @correo, Direccion = @direccion, " +
                "HoraEntrada = @horaEntrada, HoraSalida = @horaSalida, Estado = @estado " +
                "WHERE IdEmpleado = @idEmpleado", cn);

            cmd.Parameters.AddWithValue("@nombres", empleado.Nombres);
            cmd.Parameters.AddWithValue("@apellidos", empleado.Apellidos);
            cmd.Parameters.AddWithValue("@idEspecialidad", (object)empleado.IdEspecialidad ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@idCargo", (object)empleado.IdCargo ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@telefono", (object)empleado.Telefono ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@correo", (object)empleado.Correo ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@direccion", empleado.Direccion);
            cmd.Parameters.AddWithValue("@horaEntrada", empleado.HoraEntrada.ToTimeSpan());
            cmd.Parameters.AddWithValue("@horaSalida", empleado.HoraSalida.ToTimeSpan());
            cmd.Parameters.AddWithValue("@estado", (object)empleado.Estado ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@idEmpleado", empleado.IdEmpleado);

            await cn.OpenAsync();
            int filasAfectadas = await cmd.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand("DELETE FROM Empleado WHERE IdEmpleado = @id", cn);
            cmd.Parameters.AddWithValue("@id", id);

            await cn.OpenAsync();
            int filasAfectadas = await cmd.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }
    }
}

