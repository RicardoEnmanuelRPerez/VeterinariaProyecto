using Microsoft.Data.SqlClient;
using VeterinariaProyecto.Models;
using VeterinariaProyecto.Data;

namespace VeterinariaProyecto.DAO
{
    public class ProveedorDAO
    {
        private readonly ConexionDB _conexionDB;

        public ProveedorDAO()
        {
            _conexionDB = new ConexionDB();
        }

        public async Task<List<Proveedor>> ObtenerTodosAsync()
        {
            var proveedores = new List<Proveedor>();
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand("SELECT IdProveedor, Nombre, Contacto, Telefono, Correo, Direccion, Activo FROM Proveedor", cn);

            await cn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                proveedores.Add(new Proveedor
                {
                    IdProveedor = reader.GetInt32(reader.GetOrdinal("IdProveedor")),
                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                    Contacto = reader.IsDBNull(reader.GetOrdinal("Contacto")) ? null : reader.GetString(reader.GetOrdinal("Contacto")),
                    Telefono = reader.IsDBNull(reader.GetOrdinal("Telefono")) ? null : reader.GetString(reader.GetOrdinal("Telefono")),
                    Correo = reader.IsDBNull(reader.GetOrdinal("Correo")) ? null : reader.GetString(reader.GetOrdinal("Correo")),
                    Direccion = reader.IsDBNull(reader.GetOrdinal("Direccion")) ? null : reader.GetString(reader.GetOrdinal("Direccion")),
                    Activo = reader.IsDBNull(reader.GetOrdinal("Activo")) ? null : reader.GetBoolean(reader.GetOrdinal("Activo"))
                });
            }
            return proveedores;
        }

        public async Task<Proveedor?> ObtenerPorIdAsync(int id)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand("SELECT IdProveedor, Nombre, Contacto, Telefono, Correo, Direccion, Activo FROM Proveedor WHERE IdProveedor = @id", cn);
            cmd.Parameters.AddWithValue("@id", id);

            await cn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Proveedor
                {
                    IdProveedor = reader.GetInt32(reader.GetOrdinal("IdProveedor")),
                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                    Contacto = reader.IsDBNull(reader.GetOrdinal("Contacto")) ? null : reader.GetString(reader.GetOrdinal("Contacto")),
                    Telefono = reader.IsDBNull(reader.GetOrdinal("Telefono")) ? null : reader.GetString(reader.GetOrdinal("Telefono")),
                    Correo = reader.IsDBNull(reader.GetOrdinal("Correo")) ? null : reader.GetString(reader.GetOrdinal("Correo")),
                    Direccion = reader.IsDBNull(reader.GetOrdinal("Direccion")) ? null : reader.GetString(reader.GetOrdinal("Direccion")),
                    Activo = reader.IsDBNull(reader.GetOrdinal("Activo")) ? null : reader.GetBoolean(reader.GetOrdinal("Activo"))
                };
            }
            return null;
        }

        public async Task<int> InsertarAsync(Proveedor proveedor)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "INSERT INTO Proveedor (Nombre, Contacto, Telefono, Correo, Direccion, Activo) " +
                "VALUES (@nombre, @contacto, @telefono, @correo, @direccion, @activo);" +
                "SELECT SCOPE_IDENTITY();", cn);

            cmd.Parameters.AddWithValue("@nombre", proveedor.Nombre);
            cmd.Parameters.AddWithValue("@contacto", (object)proveedor.Contacto ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@telefono", (object)proveedor.Telefono ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@correo", (object)proveedor.Correo ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@direccion", (object)proveedor.Direccion ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@activo", (object)proveedor.Activo ?? DBNull.Value);

            await cn.OpenAsync();
            var id = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(id);
        }

        public async Task<bool> ActualizarAsync(Proveedor proveedor)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "UPDATE Proveedor SET Nombre = @nombre, Contacto = @contacto, Telefono = @telefono, " +
                "Correo = @correo, Direccion = @direccion, Activo = @activo WHERE IdProveedor = @idProveedor", cn);

            cmd.Parameters.AddWithValue("@nombre", proveedor.Nombre);
            cmd.Parameters.AddWithValue("@contacto", (object)proveedor.Contacto ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@telefono", (object)proveedor.Telefono ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@correo", (object)proveedor.Correo ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@direccion", (object)proveedor.Direccion ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@activo", (object)proveedor.Activo ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@idProveedor", proveedor.IdProveedor);

            await cn.OpenAsync();
            int filasAfectadas = await cmd.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand("DELETE FROM Proveedor WHERE IdProveedor = @id", cn);
            cmd.Parameters.AddWithValue("@id", id);

            await cn.OpenAsync();
            int filasAfectadas = await cmd.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }
    }
}

