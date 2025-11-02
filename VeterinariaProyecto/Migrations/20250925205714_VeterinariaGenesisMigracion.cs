using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeterinariaProyecto.Migrations
{
    /// <inheritdoc />
    public partial class VeterinariaGenesisMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cargo",
                columns: table => new
                {
                    IdCargo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCargo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cargo__6C9856250ACFF26F", x => x.IdCargo);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombres = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Apellidos = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Direccion = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Telefono = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    Correo = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Dni = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cliente__D594664230906588", x => x.IdCliente);
                });

            migrationBuilder.CreateTable(
                name: "Especialidad",
                columns: table => new
                {
                    IdEspecialidad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreEspecialidad = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Especial__693FA0AFF6CB8E08", x => x.IdEspecialidad);
                });

            migrationBuilder.CreateTable(
                name: "Especie",
                columns: table => new
                {
                    IdEspecie = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreEspecie = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Especie__08BEEA3E94138236", x => x.IdEspecie);
                });

            migrationBuilder.CreateTable(
                name: "Proveedor",
                columns: table => new
                {
                    IdProveedor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Contacto = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Telefono = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Correo = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Direccion = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Proveedo__E8B631AFC041219D", x => x.IdProveedor);
                });

            migrationBuilder.CreateTable(
                name: "Sexo",
                columns: table => new
                {
                    IdSexo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreSexo = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Sexo__A7739FA2A5E6C3EC", x => x.IdSexo);
                });

            migrationBuilder.CreateTable(
                name: "Tratamiento",
                columns: table => new
                {
                    IdTratamiento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreTratamiento = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tratamie__5CB7E7532A06EE80", x => x.IdTratamiento);
                });

            migrationBuilder.CreateTable(
                name: "Empleado",
                columns: table => new
                {
                    IdEmpleado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombres = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Apellidos = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    IdEspecialidad = table.Column<int>(type: "int", nullable: true),
                    IdCargo = table.Column<int>(type: "int", nullable: true),
                    Telefono = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    Correo = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Direccion = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    HoraEntrada = table.Column<TimeOnly>(type: "time", nullable: false),
                    HoraSalida = table.Column<TimeOnly>(type: "time", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Empleado__CE6D8B9E84A6F89E", x => x.IdEmpleado);
                    table.ForeignKey(
                        name: "FK_Empleado_Cargo",
                        column: x => x.IdCargo,
                        principalTable: "Cargo",
                        principalColumn: "IdCargo");
                    table.ForeignKey(
                        name: "FK_Empleado_Especialidad",
                        column: x => x.IdEspecialidad,
                        principalTable: "Especialidad",
                        principalColumn: "IdEspecialidad");
                });

            migrationBuilder.CreateTable(
                name: "Raza",
                columns: table => new
                {
                    IdRaza = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreRaza = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    IdEspecie = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Raza__8F06EB28D553DF48", x => x.IdRaza);
                    table.ForeignKey(
                        name: "FK_Raza_Especie",
                        column: x => x.IdEspecie,
                        principalTable: "Especie",
                        principalColumn: "IdEspecie");
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    IdProducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    Tipo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    UnidadMedida = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    PrecioCompra = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PrecioVenta = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    StockActual = table.Column<int>(type: "int", nullable: false),
                    StockMinimo = table.Column<int>(type: "int", nullable: false),
                    FechaVencimiento = table.Column<DateOnly>(type: "date", nullable: false),
                    IdProveedor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Producto__09889210617ED440", x => x.IdProducto);
                    table.ForeignKey(
                        name: "FK_Producto_Proveedor",
                        column: x => x.IdProveedor,
                        principalTable: "Proveedor",
                        principalColumn: "IdProveedor");
                });

            migrationBuilder.CreateTable(
                name: "Factura",
                columns: table => new
                {
                    IdFactura = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaEmision = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    FechaVencimiento = table.Column<DateTime>(type: "datetime", nullable: true),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    IdEmpleado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Factura__50E7BAF142100119", x => x.IdFactura);
                    table.ForeignKey(
                        name: "FK_Factura_Cliente",
                        column: x => x.IdCliente,
                        principalTable: "Cliente",
                        principalColumn: "IdCliente");
                    table.ForeignKey(
                        name: "FK_Factura_Empleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado");
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreUsuario = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Contraseña = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Rol = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    IdEmpleado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Usuario__5B65BF97C9D4AFC5", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_Usuario_Empleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado");
                });

            migrationBuilder.CreateTable(
                name: "Mascota",
                columns: table => new
                {
                    IdMascota = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    IdEspecie = table.Column<int>(type: "int", nullable: false),
                    IdRaza = table.Column<int>(type: "int", nullable: false),
                    IdSexo = table.Column<int>(type: "int", nullable: false),
                    FechaNacimiento = table.Column<DateOnly>(type: "date", nullable: true),
                    Peso = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Esterilizado = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Mascota__5C9C26F0D5B38D71", x => x.IdMascota);
                    table.ForeignKey(
                        name: "FK_Mascota_Cliente",
                        column: x => x.IdCliente,
                        principalTable: "Cliente",
                        principalColumn: "IdCliente");
                    table.ForeignKey(
                        name: "FK_Mascota_Especie",
                        column: x => x.IdEspecie,
                        principalTable: "Especie",
                        principalColumn: "IdEspecie");
                    table.ForeignKey(
                        name: "FK_Mascota_Raza",
                        column: x => x.IdRaza,
                        principalTable: "Raza",
                        principalColumn: "IdRaza");
                    table.ForeignKey(
                        name: "FK_Mascota_Sexo",
                        column: x => x.IdSexo,
                        principalTable: "Sexo",
                        principalColumn: "IdSexo");
                });

            migrationBuilder.CreateTable(
                name: "FacturaProducto",
                columns: table => new
                {
                    IdFactura = table.Column<int>(type: "int", nullable: false),
                    IdProducto = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacturaProducto", x => new { x.IdFactura, x.IdProducto });
                    table.ForeignKey(
                        name: "FK_FacturaProducto_Factura",
                        column: x => x.IdFactura,
                        principalTable: "Factura",
                        principalColumn: "IdFactura");
                    table.ForeignKey(
                        name: "FK_FacturaProducto_Producto",
                        column: x => x.IdProducto,
                        principalTable: "Producto",
                        principalColumn: "IdProducto");
                });

            migrationBuilder.CreateTable(
                name: "Cita",
                columns: table => new
                {
                    IdCita = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdMascota = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Motivo = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    Diagnostico = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    IdTratamiento = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cita__394B02024E0CB2EA", x => x.IdCita);
                    table.ForeignKey(
                        name: "FK_Cita_Mascota",
                        column: x => x.IdMascota,
                        principalTable: "Mascota",
                        principalColumn: "IdMascota");
                    table.ForeignKey(
                        name: "FK_Cita_Tratamiento",
                        column: x => x.IdTratamiento,
                        principalTable: "Tratamiento",
                        principalColumn: "IdTratamiento");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cita_IdMascota",
                table: "Cita",
                column: "IdMascota");

            migrationBuilder.CreateIndex(
                name: "IX_Cita_IdTratamiento",
                table: "Cita",
                column: "IdTratamiento");

            migrationBuilder.CreateIndex(
                name: "UQ__Cliente__C0308575D20E1AB0",
                table: "Cliente",
                column: "Dni",
                unique: true,
                filter: "[Dni] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_IdCargo",
                table: "Empleado",
                column: "IdCargo");

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_IdEspecialidad",
                table: "Empleado",
                column: "IdEspecialidad");

            migrationBuilder.CreateIndex(
                name: "IX_Factura_IdCliente",
                table: "Factura",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Factura_IdEmpleado",
                table: "Factura",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_FacturaProducto_IdProducto",
                table: "FacturaProducto",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_Mascota_IdCliente",
                table: "Mascota",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Mascota_IdEspecie",
                table: "Mascota",
                column: "IdEspecie");

            migrationBuilder.CreateIndex(
                name: "IX_Mascota_IdRaza",
                table: "Mascota",
                column: "IdRaza");

            migrationBuilder.CreateIndex(
                name: "IX_Mascota_IdSexo",
                table: "Mascota",
                column: "IdSexo");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_IdProveedor",
                table: "Producto",
                column: "IdProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_Raza_IdEspecie",
                table: "Raza",
                column: "IdEspecie");

            migrationBuilder.CreateIndex(
                name: "UQ_Raza_Especie",
                table: "Raza",
                columns: new[] { "NombreRaza", "IdEspecie" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_IdEmpleado",
                table: "Usuario",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "UQ__Usuario__6B0F5AE0B6A027EA",
                table: "Usuario",
                column: "NombreUsuario",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cita");

            migrationBuilder.DropTable(
                name: "FacturaProducto");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Mascota");

            migrationBuilder.DropTable(
                name: "Tratamiento");

            migrationBuilder.DropTable(
                name: "Factura");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Raza");

            migrationBuilder.DropTable(
                name: "Sexo");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Empleado");

            migrationBuilder.DropTable(
                name: "Proveedor");

            migrationBuilder.DropTable(
                name: "Especie");

            migrationBuilder.DropTable(
                name: "Cargo");

            migrationBuilder.DropTable(
                name: "Especialidad");
        }
    }
}
