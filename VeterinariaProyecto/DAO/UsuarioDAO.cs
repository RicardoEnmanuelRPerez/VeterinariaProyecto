using Microsoft.Data.SqlClient;
using VeterinariaProyecto.Models;
using VeterinariaProyecto.Data;

namespace VeterinariaProyecto.DAO
{
    public class UsuarioDAO
    {
        private readonly ConexionDB _conexionDB;

        public UsuarioDAO()
        {
            _conexionDB = new ConexionDB();
        }

        public async Task<List<Usuario>> ObtenerTodosAsync()
        {
            var usuarios = new List<Usuario>();
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "SELECT IdUsuario, NombreUsuario, Contraseña, Rol, Activo, IdEmpleado FROM Usuario", cn);

            await cn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                usuarios.Add(new Usuario
                {
                    IdUsuario = reader.GetInt32(reader.GetOrdinal("IdUsuario")),
                    NombreUsuario = reader.GetString(reader.GetOrdinal("NombreUsuario")),
                    Contraseña = reader.GetString(reader.GetOrdinal("Contraseña")),
                    Rol = reader.IsDBNull(reader.GetOrdinal("Rol")) ? null : reader.GetString(reader.GetOrdinal("Rol")),
                    Activo = reader.IsDBNull(reader.GetOrdinal("Activo")) ? null : reader.GetBoolean(reader.GetOrdinal("Activo")),
                    IdEmpleado = reader.GetInt32(reader.GetOrdinal("IdEmpleado"))
                });
            }
            return usuarios;
        }

        public async Task<Usuario?> ObtenerPorIdAsync(int id)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "SELECT IdUsuario, NombreUsuario, Contraseña, Rol, Activo, IdEmpleado FROM Usuario WHERE IdUsuario = @id", cn);
            cmd.Parameters.AddWithValue("@id", id);

            await cn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Usuario
                {
                    IdUsuario = reader.GetInt32(reader.GetOrdinal("IdUsuario")),
                    NombreUsuario = reader.GetString(reader.GetOrdinal("NombreUsuario")),
                    Contraseña = reader.GetString(reader.GetOrdinal("Contraseña")),
                    Rol = reader.IsDBNull(reader.GetOrdinal("Rol")) ? null : reader.GetString(reader.GetOrdinal("Rol")),
                    Activo = reader.IsDBNull(reader.GetOrdinal("Activo")) ? null : reader.GetBoolean(reader.GetOrdinal("Activo")),
                    IdEmpleado = reader.GetInt32(reader.GetOrdinal("IdEmpleado"))
                };
            }
            return null;
        }

        public async Task<int> InsertarAsync(Usuario usuario)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "INSERT INTO Usuario (NombreUsuario, Contraseña, Rol, Activo, IdEmpleado) " +
                "VALUES (@nombreUsuario, @contraseña, @rol, @activo, @idEmpleado);" +
                "SELECT SCOPE_IDENTITY();", cn);

            cmd.Parameters.AddWithValue("@nombreUsuario", usuario.NombreUsuario);
            cmd.Parameters.AddWithValue("@contraseña", usuario.Contraseña);
            cmd.Parameters.AddWithValue("@rol", (object)usuario.Rol ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@activo", (object)usuario.Activo ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@idEmpleado", usuario.IdEmpleado);

            await cn.OpenAsync();
            var id = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(id);
        }

        public async Task<bool> ActualizarAsync(Usuario usuario)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "UPDATE Usuario SET NombreUsuario = @nombreUsuario, Contraseña = @contraseña, " +
                "Rol = @rol, Activo = @activo, IdEmpleado = @idEmpleado WHERE IdUsuario = @idUsuario", cn);

            cmd.Parameters.AddWithValue("@nombreUsuario", usuario.NombreUsuario);
            cmd.Parameters.AddWithValue("@contraseña", usuario.Contraseña);
            cmd.Parameters.AddWithValue("@rol", (object)usuario.Rol ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@activo", (object)usuario.Activo ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@idEmpleado", usuario.IdEmpleado);
            cmd.Parameters.AddWithValue("@idUsuario", usuario.IdUsuario);

            await cn.OpenAsync();
            int filasAfectadas = await cmd.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand("DELETE FROM Usuario WHERE IdUsuario = @id", cn);
            cmd.Parameters.AddWithValue("@id", id);

            await cn.OpenAsync();
            int filasAfectadas = await cmd.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }
    }
}

