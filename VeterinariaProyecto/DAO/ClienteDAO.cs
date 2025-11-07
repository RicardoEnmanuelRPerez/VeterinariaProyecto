using Microsoft.Data.SqlClient;
using VeterinariaProyecto.Models;
using VeterinariaProyecto.Data;

namespace VeterinariaProyecto.DAO
{
    // En DAO/ClienteDAO.cs
    
    public class ClienteDAO
    {
        private readonly ConexionDB _conexionDB;

        public ClienteDAO()
        {
            _conexionDB = new ConexionDB(); // Usa la conexión que creamos
        }

        // --- OPERACIÓN READ (Obtener Todos) ---
        public async Task<List<Cliente>> ObtenerTodosAsync()
        {
            var clientes = new List<Cliente>();
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand("SELECT IdCliente, Nombres, Apellidos, Direccion, Telefono, Correo, Dni, FechaRegistro FROM Cliente", cn);

            await cn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                clientes.Add(new Cliente
                {
                    IdCliente = reader.GetInt32(reader.GetOrdinal("IdCliente")),
                    Nombres = reader.GetString(reader.GetOrdinal("Nombres")),
                    Apellidos = reader.GetString(reader.GetOrdinal("Apellidos")),
                    Direccion = reader.GetString(reader.GetOrdinal("Direccion")),
                    Telefono = reader.IsDBNull(reader.GetOrdinal("Telefono")) ? null : reader.GetString(reader.GetOrdinal("Telefono")),
                    Correo = reader.IsDBNull(reader.GetOrdinal("Correo")) ? null : reader.GetString(reader.GetOrdinal("Correo")),
                    Dni = reader.IsDBNull(reader.GetOrdinal("Dni")) ? null : reader.GetString(reader.GetOrdinal("Dni")),
                    FechaRegistro = reader.GetDateTime(reader.GetOrdinal("FechaRegistro"))
                });
            }
            return clientes;
        }

        // --- OPERACIÓN READ (Obtener por ID) ---
        public async Task<Cliente?> ObtenerPorIdAsync(int id)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand("SELECT IdCliente, Nombres, Apellidos, Direccion, Telefono, Correo, Dni, FechaRegistro FROM Cliente WHERE IdCliente = @id", cn);
            cmd.Parameters.AddWithValue("@id", id);

            await cn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Cliente
                {
                    IdCliente = reader.GetInt32(reader.GetOrdinal("IdCliente")),
                    Nombres = reader.GetString(reader.GetOrdinal("Nombres")),
                    Apellidos = reader.GetString(reader.GetOrdinal("Apellidos")),
                    Direccion = reader.GetString(reader.GetOrdinal("Direccion")),
                    Telefono = reader.IsDBNull(reader.GetOrdinal("Telefono")) ? null : reader.GetString(reader.GetOrdinal("Telefono")),
                    Correo = reader.IsDBNull(reader.GetOrdinal("Correo")) ? null : reader.GetString(reader.GetOrdinal("Correo")),
                    Dni = reader.IsDBNull(reader.GetOrdinal("Dni")) ? null : reader.GetString(reader.GetOrdinal("Dni")),
                    FechaRegistro = reader.GetDateTime(reader.GetOrdinal("FechaRegistro"))
                };
            }
            return null;
        }

        // --- OPERACIÓN CREATE (Insertar) ---
        public async Task<int> InsertarAsync(Cliente cliente)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "INSERT INTO Cliente (Nombres, Apellidos, Direccion, Telefono, Correo, Dni, FechaRegistro) " +
                "VALUES (@nombres, @apellidos, @direccion, @telefono, @correo, @dni, @fechaRegistro);" +
                "SELECT SCOPE_IDENTITY();", cn);

            cmd.Parameters.AddWithValue("@nombres", cliente.Nombres);
            cmd.Parameters.AddWithValue("@apellidos", cliente.Apellidos);
            cmd.Parameters.AddWithValue("@direccion", cliente.Direccion);
            cmd.Parameters.AddWithValue("@telefono", (object)cliente.Telefono ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@correo", (object)cliente.Correo ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@dni", (object)cliente.Dni ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@fechaRegistro", cliente.FechaRegistro);

            await cn.OpenAsync();
            var id = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(id);
        }

        // --- OPERACIÓN UPDATE (Actualizar) ---
        public async Task<bool> ActualizarAsync(Cliente cliente)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "UPDATE Cliente SET Nombres = @nombres, Apellidos = @apellidos, Direccion = @direccion, " +
                "Telefono = @telefono, Correo = @correo, Dni = @dni, FechaRegistro = @fechaRegistro " +
                "WHERE IdCliente = @idCliente", cn);

            cmd.Parameters.AddWithValue("@nombres", cliente.Nombres);
            cmd.Parameters.AddWithValue("@apellidos", cliente.Apellidos);
            cmd.Parameters.AddWithValue("@direccion", cliente.Direccion);
            cmd.Parameters.AddWithValue("@telefono", (object)cliente.Telefono ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@correo", (object)cliente.Correo ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@dni", (object)cliente.Dni ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@fechaRegistro", cliente.FechaRegistro);
            cmd.Parameters.AddWithValue("@idCliente", cliente.IdCliente);

            await cn.OpenAsync();
            int filasAfectadas = await cmd.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }

        // --- OPERACIÓN DELETE (Eliminar) ---
        public async Task<bool> EliminarAsync(int id)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand("DELETE FROM Cliente WHERE IdCliente = @id", cn);
            cmd.Parameters.AddWithValue("@id", id);

            await cn.OpenAsync();
            int filasAfectadas = await cmd.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }
    }
}
