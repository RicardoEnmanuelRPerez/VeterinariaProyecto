// En Data/ConexionDB.cs
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace VeterinariaProyecto.Data
{
    public class ConexionDB
    {
        private readonly string connectionString;

        public ConexionDB()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            
            var configuration = builder.Build();
            connectionString = configuration.GetConnectionString("DefaultConnection") 
                ?? "Server=DESKTOP-ND3SPV7;Database=VeterinariaGenesisDB;Integrated Security=True;TrustServerCertificate=true;";
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}