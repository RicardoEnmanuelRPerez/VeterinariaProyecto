using Microsoft.Data.SqlClient;
using VeterinariaProyecto.Models;
using VeterinariaProyecto.Data;

namespace VeterinariaProyecto.DAO
{
    public class FacturaDAO
    {
        private readonly ConexionDB _conexionDB;

        public FacturaDAO()
        {
            _conexionDB = new ConexionDB();
        }

        public async Task<List<Factura>> ObtenerTodosAsync()
        {
            var facturas = new List<Factura>();
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "SELECT IdFactura, FechaEmision, FechaVencimiento, IdCliente, IdEmpleado FROM Factura", cn);

            await cn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                facturas.Add(new Factura
                {
                    IdFactura = reader.GetInt32(reader.GetOrdinal("IdFactura")),
                    FechaEmision = reader.GetDateTime(reader.GetOrdinal("FechaEmision")),
                    FechaVencimiento = reader.IsDBNull(reader.GetOrdinal("FechaVencimiento")) ? null : reader.GetDateTime(reader.GetOrdinal("FechaVencimiento")),
                    IdCliente = reader.GetInt32(reader.GetOrdinal("IdCliente")),
                    IdEmpleado = reader.GetInt32(reader.GetOrdinal("IdEmpleado"))
                });
            }
            return facturas;
        }

        public async Task<Factura?> ObtenerPorIdAsync(int id)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "SELECT IdFactura, FechaEmision, FechaVencimiento, IdCliente, IdEmpleado FROM Factura WHERE IdFactura = @id", cn);
            cmd.Parameters.AddWithValue("@id", id);

            await cn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Factura
                {
                    IdFactura = reader.GetInt32(reader.GetOrdinal("IdFactura")),
                    FechaEmision = reader.GetDateTime(reader.GetOrdinal("FechaEmision")),
                    FechaVencimiento = reader.IsDBNull(reader.GetOrdinal("FechaVencimiento")) ? null : reader.GetDateTime(reader.GetOrdinal("FechaVencimiento")),
                    IdCliente = reader.GetInt32(reader.GetOrdinal("IdCliente")),
                    IdEmpleado = reader.GetInt32(reader.GetOrdinal("IdEmpleado"))
                };
            }
            return null;
        }

        public async Task<int> InsertarAsync(Factura factura)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "INSERT INTO Factura (FechaEmision, FechaVencimiento, IdCliente, IdEmpleado) " +
                "VALUES (@fechaEmision, @fechaVencimiento, @idCliente, @idEmpleado);" +
                "SELECT SCOPE_IDENTITY();", cn);

            cmd.Parameters.AddWithValue("@fechaEmision", factura.FechaEmision);
            cmd.Parameters.AddWithValue("@fechaVencimiento", (object)factura.FechaVencimiento ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@idCliente", factura.IdCliente);
            cmd.Parameters.AddWithValue("@idEmpleado", factura.IdEmpleado);

            await cn.OpenAsync();
            var id = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(id);
        }

        public async Task<bool> ActualizarAsync(Factura factura)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand(
                "UPDATE Factura SET FechaEmision = @fechaEmision, FechaVencimiento = @fechaVencimiento, " +
                "IdCliente = @idCliente, IdEmpleado = @idEmpleado WHERE IdFactura = @idFactura", cn);

            cmd.Parameters.AddWithValue("@fechaEmision", factura.FechaEmision);
            cmd.Parameters.AddWithValue("@fechaVencimiento", (object)factura.FechaVencimiento ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@idCliente", factura.IdCliente);
            cmd.Parameters.AddWithValue("@idEmpleado", factura.IdEmpleado);
            cmd.Parameters.AddWithValue("@idFactura", factura.IdFactura);

            await cn.OpenAsync();
            int filasAfectadas = await cmd.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            using var cn = _conexionDB.GetConnection();
            using var cmd = new SqlCommand("DELETE FROM Factura WHERE IdFactura = @id", cn);
            cmd.Parameters.AddWithValue("@id", id);

            await cn.OpenAsync();
            int filasAfectadas = await cmd.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }
    }
}

