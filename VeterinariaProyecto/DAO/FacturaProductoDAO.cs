using Microsoft.Data.SqlClient;
using VeterinariaProyecto.Models;
using VeterinariaProyecto.Data;

namespace VeterinariaProyecto.DAO
{
    public class FacturaProductoDAO
    {
        private readonly ConexionDB _conexionDB;

        public FacturaProductoDAO()
        {
            _conexionDB = new ConexionDB();
        }

        public async Task<List<FacturaProducto>> ObtenerTodosAsync()
        {
            var facturaProductos = new List<FacturaProducto>();
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "SELECT IdFactura, IdProducto, Cantidad, PrecioUnitario, Total FROM FacturaProducto", cn);

            await cn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                facturaProductos.Add(new FacturaProducto
                {
                    IdFactura = reader.GetInt32(reader.GetOrdinal("IdFactura")),
                    IdProducto = reader.GetInt32(reader.GetOrdinal("IdProducto")),
                    Cantidad = reader.GetInt32(reader.GetOrdinal("Cantidad")),
                    PrecioUnitario = reader.GetDecimal(reader.GetOrdinal("PrecioUnitario")),
                    Total = reader.GetDecimal(reader.GetOrdinal("Total"))
                });
            }
            return facturaProductos;
        }

        public async Task<FacturaProducto?> ObtenerPorIdAsync(int idFactura, int idProducto)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "SELECT IdFactura, IdProducto, Cantidad, PrecioUnitario, Total FROM FacturaProducto " +
                "WHERE IdFactura = @idFactura AND IdProducto = @idProducto", cn);
            cmd.Parameters.AddWithValue("@idFactura", idFactura);
            cmd.Parameters.AddWithValue("@idProducto", idProducto);

            await cn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new FacturaProducto
                {
                    IdFactura = reader.GetInt32(reader.GetOrdinal("IdFactura")),
                    IdProducto = reader.GetInt32(reader.GetOrdinal("IdProducto")),
                    Cantidad = reader.GetInt32(reader.GetOrdinal("Cantidad")),
                    PrecioUnitario = reader.GetDecimal(reader.GetOrdinal("PrecioUnitario")),
                    Total = reader.GetDecimal(reader.GetOrdinal("Total"))
                };
            }
            return null;
        }

        public async Task<List<FacturaProducto>> ObtenerPorFacturaAsync(int idFactura)
        {
            var facturaProductos = new List<FacturaProducto>();
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "SELECT IdFactura, IdProducto, Cantidad, PrecioUnitario, Total FROM FacturaProducto WHERE IdFactura = @idFactura", cn);
            cmd.Parameters.AddWithValue("@idFactura", idFactura);

            await cn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                facturaProductos.Add(new FacturaProducto
                {
                    IdFactura = reader.GetInt32(reader.GetOrdinal("IdFactura")),
                    IdProducto = reader.GetInt32(reader.GetOrdinal("IdProducto")),
                    Cantidad = reader.GetInt32(reader.GetOrdinal("Cantidad")),
                    PrecioUnitario = reader.GetDecimal(reader.GetOrdinal("PrecioUnitario")),
                    Total = reader.GetDecimal(reader.GetOrdinal("Total"))
                });
            }
            return facturaProductos;
        }

        public async Task<bool> InsertarAsync(FacturaProducto facturaProducto)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "INSERT INTO FacturaProducto (IdFactura, IdProducto, Cantidad, PrecioUnitario, Total) " +
                "VALUES (@idFactura, @idProducto, @cantidad, @precioUnitario, @total)", cn);

            cmd.Parameters.AddWithValue("@idFactura", facturaProducto.IdFactura);
            cmd.Parameters.AddWithValue("@idProducto", facturaProducto.IdProducto);
            cmd.Parameters.AddWithValue("@cantidad", facturaProducto.Cantidad);
            cmd.Parameters.AddWithValue("@precioUnitario", facturaProducto.PrecioUnitario);
            cmd.Parameters.AddWithValue("@total", facturaProducto.Total);

            await cn.OpenAsync();
            int filasAfectadas = await cmd.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }

        public async Task<bool> ActualizarAsync(FacturaProducto facturaProducto)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "UPDATE FacturaProducto SET Cantidad = @cantidad, PrecioUnitario = @precioUnitario, " +
                "Total = @total WHERE IdFactura = @idFactura AND IdProducto = @idProducto", cn);

            cmd.Parameters.AddWithValue("@cantidad", facturaProducto.Cantidad);
            cmd.Parameters.AddWithValue("@precioUnitario", facturaProducto.PrecioUnitario);
            cmd.Parameters.AddWithValue("@total", facturaProducto.Total);
            cmd.Parameters.AddWithValue("@idFactura", facturaProducto.IdFactura);
            cmd.Parameters.AddWithValue("@idProducto", facturaProducto.IdProducto);

            await cn.OpenAsync();
            int filasAfectadas = await cmd.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }

        public async Task<bool> EliminarAsync(int idFactura, int idProducto)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "DELETE FROM FacturaProducto WHERE IdFactura = @idFactura AND IdProducto = @idProducto", cn);
            cmd.Parameters.AddWithValue("@idFactura", idFactura);
            cmd.Parameters.AddWithValue("@idProducto", idProducto);

            await cn.OpenAsync();
            int filasAfectadas = await cmd.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }
    }
}

