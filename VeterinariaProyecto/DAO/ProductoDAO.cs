using Microsoft.Data.SqlClient;
using VeterinariaProyecto.Models;
using VeterinariaProyecto.Data;

namespace VeterinariaProyecto.DAO
{
    public class ProductoDAO
    {
        private readonly ConexionDB _conexionDB;

        public ProductoDAO()
        {
            _conexionDB = new ConexionDB();
        }

        public async Task<List<Producto>> ObtenerTodosAsync()
        {
            var productos = new List<Producto>();
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "SELECT IdProducto, Nombre, Descripcion, Tipo, UnidadMedida, PrecioCompra, PrecioVenta, " +
                "StockActual, StockMinimo, FechaVencimiento, IdProveedor FROM Producto", cn);

            await cn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                productos.Add(new Producto
                {
                    IdProducto = reader.GetInt32(reader.GetOrdinal("IdProducto")),
                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                    Descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion")) ? null : reader.GetString(reader.GetOrdinal("Descripcion")),
                    Tipo = reader.IsDBNull(reader.GetOrdinal("Tipo")) ? null : reader.GetString(reader.GetOrdinal("Tipo")),
                    UnidadMedida = reader.IsDBNull(reader.GetOrdinal("UnidadMedida")) ? null : reader.GetString(reader.GetOrdinal("UnidadMedida")),
                    PrecioCompra = reader.GetDecimal(reader.GetOrdinal("PrecioCompra")),
                    PrecioVenta = reader.GetDecimal(reader.GetOrdinal("PrecioVenta")),
                    StockActual = reader.GetInt32(reader.GetOrdinal("StockActual")),
                    StockMinimo = reader.GetInt32(reader.GetOrdinal("StockMinimo")),
                    FechaVencimiento = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("FechaVencimiento"))),
                    IdProveedor = reader.GetInt32(reader.GetOrdinal("IdProveedor"))
                });
            }
            return productos;
        }

        public async Task<Producto?> ObtenerPorIdAsync(int id)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "SELECT IdProducto, Nombre, Descripcion, Tipo, UnidadMedida, PrecioCompra, PrecioVenta, " +
                "StockActual, StockMinimo, FechaVencimiento, IdProveedor FROM Producto WHERE IdProducto = @id", cn);
            cmd.Parameters.AddWithValue("@id", id);

            await cn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Producto
                {
                    IdProducto = reader.GetInt32(reader.GetOrdinal("IdProducto")),
                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                    Descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion")) ? null : reader.GetString(reader.GetOrdinal("Descripcion")),
                    Tipo = reader.IsDBNull(reader.GetOrdinal("Tipo")) ? null : reader.GetString(reader.GetOrdinal("Tipo")),
                    UnidadMedida = reader.IsDBNull(reader.GetOrdinal("UnidadMedida")) ? null : reader.GetString(reader.GetOrdinal("UnidadMedida")),
                    PrecioCompra = reader.GetDecimal(reader.GetOrdinal("PrecioCompra")),
                    PrecioVenta = reader.GetDecimal(reader.GetOrdinal("PrecioVenta")),
                    StockActual = reader.GetInt32(reader.GetOrdinal("StockActual")),
                    StockMinimo = reader.GetInt32(reader.GetOrdinal("StockMinimo")),
                    FechaVencimiento = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("FechaVencimiento"))),
                    IdProveedor = reader.GetInt32(reader.GetOrdinal("IdProveedor"))
                };
            }
            return null;
        }

        public async Task<int> InsertarAsync(Producto producto)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "INSERT INTO Producto (Nombre, Descripcion, Tipo, UnidadMedida, PrecioCompra, PrecioVenta, " +
                "StockActual, StockMinimo, FechaVencimiento, IdProveedor) " +
                "VALUES (@nombre, @descripcion, @tipo, @unidadMedida, @precioCompra, @precioVenta, " +
                "@stockActual, @stockMinimo, @fechaVencimiento, @idProveedor);" +
                "SELECT SCOPE_IDENTITY();", cn);

            cmd.Parameters.AddWithValue("@nombre", producto.Nombre);
            cmd.Parameters.AddWithValue("@descripcion", (object)producto.Descripcion ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@tipo", (object)producto.Tipo ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@unidadMedida", (object)producto.UnidadMedida ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@precioCompra", producto.PrecioCompra);
            cmd.Parameters.AddWithValue("@precioVenta", producto.PrecioVenta);
            cmd.Parameters.AddWithValue("@stockActual", producto.StockActual);
            cmd.Parameters.AddWithValue("@stockMinimo", producto.StockMinimo);
            cmd.Parameters.AddWithValue("@fechaVencimiento", producto.FechaVencimiento.ToDateTime(TimeOnly.MinValue));
            cmd.Parameters.AddWithValue("@idProveedor", producto.IdProveedor);

            await cn.OpenAsync();
            var id = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(id);
        }

        public async Task<bool> ActualizarAsync(Producto producto)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "UPDATE Producto SET Nombre = @nombre, Descripcion = @descripcion, Tipo = @tipo, " +
                "UnidadMedida = @unidadMedida, PrecioCompra = @precioCompra, PrecioVenta = @precioVenta, " +
                "StockActual = @stockActual, StockMinimo = @stockMinimo, FechaVencimiento = @fechaVencimiento, " +
                "IdProveedor = @idProveedor WHERE IdProducto = @idProducto", cn);

            cmd.Parameters.AddWithValue("@nombre", producto.Nombre);
            cmd.Parameters.AddWithValue("@descripcion", (object)producto.Descripcion ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@tipo", (object)producto.Tipo ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@unidadMedida", (object)producto.UnidadMedida ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@precioCompra", producto.PrecioCompra);
            cmd.Parameters.AddWithValue("@precioVenta", producto.PrecioVenta);
            cmd.Parameters.AddWithValue("@stockActual", producto.StockActual);
            cmd.Parameters.AddWithValue("@stockMinimo", producto.StockMinimo);
            cmd.Parameters.AddWithValue("@fechaVencimiento", producto.FechaVencimiento.ToDateTime(TimeOnly.MinValue));
            cmd.Parameters.AddWithValue("@idProveedor", producto.IdProveedor);
            cmd.Parameters.AddWithValue("@idProducto", producto.IdProducto);

            await cn.OpenAsync();
            int filasAfectadas = await cmd.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand("DELETE FROM Producto WHERE IdProducto = @id", cn);
            cmd.Parameters.AddWithValue("@id", id);

            await cn.OpenAsync();
            int filasAfectadas = await cmd.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }
    }
}

