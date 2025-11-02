using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Veterinaria_Genesis_DB.Models;

namespace Veterinaria_Genesis_DB.Data;

public partial class VeterinariaContext : DbContext
{
    public VeterinariaContext()
    {
    }

    public VeterinariaContext(DbContextOptions<VeterinariaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cargo> Cargos { get; set; }

    public virtual DbSet<Cita> Cita { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Especialidad> Especialidads { get; set; }

    public virtual DbSet<Especie> Especies { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<FacturaProducto> FacturaProductos { get; set; }

    public virtual DbSet<Mascota> Mascota { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Proveedor> Proveedors { get; set; }

    public virtual DbSet<Raza> Razas { get; set; }

    public virtual DbSet<Sexo> Sexos { get; set; }

    public virtual DbSet<Tratamiento> Tratamientos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<ProcedimientoMedico> ProcedimientosMedicos { get; set; }

    public virtual DbSet<HistoriaClinica> HistoriaClinicas { get; set; }

    public virtual DbSet<Veterinario> Veterinarios { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseSqlServer("Server=MONTOYA\\SQL2019;Database=DBVeterinaria;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.HasKey(e => e.IdCargo).HasName("PK__Cargo__6C9856250ACFF26F");
        });

        modelBuilder.Entity<Cita>(entity =>
        {
            entity.HasKey(e => e.IdCita).HasName("PK__Cita__394B02024E0CB2EA");

            entity.Property(e => e.Fecha).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdMascotaNavigation).WithMany(p => p.Cita)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cita_Mascota");

            entity.HasOne(d => d.IdTratamientoNavigation).WithMany(p => p.Cita).HasConstraintName("FK_Cita_Tratamiento");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__Cliente__D594664230906588");

            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK__Empleado__CE6D8B9E84A6F89E");

            entity.Property(e => e.Estado).HasDefaultValue(true);

            entity.HasOne(d => d.IdCargoNavigation).WithMany(p => p.Empleados).HasConstraintName("FK_Empleado_Cargo");

            entity.HasOne(d => d.IdEspecialidadNavigation).WithMany(p => p.Empleados).HasConstraintName("FK_Empleado_Especialidad");
        });

        modelBuilder.Entity<Especialidad>(entity =>
        {
            entity.HasKey(e => e.IdEspecialidad).HasName("PK__Especial__693FA0AFF6CB8E08");
        });

        modelBuilder.Entity<Especie>(entity =>
        {
            entity.HasKey(e => e.IdEspecie).HasName("PK__Especie__08BEEA3E94138236");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.IdFactura).HasName("PK__Factura__50E7BAF142100119");

            entity.Property(e => e.FechaEmision).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Facturas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Factura_Cliente");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Facturas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Factura_Empleado");
        });

        modelBuilder.Entity<FacturaProducto>(entity =>
        {
            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.FacturaProductos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FacturaProducto_Factura");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.FacturaProductos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FacturaProducto_Producto");
        });

        modelBuilder.Entity<Mascota>(entity =>
        {
            entity.HasKey(e => e.IdMascota).HasName("PK__Mascota__5C9C26F0D5B38D71");

            entity.Property(e => e.Esterilizado).HasDefaultValue(false);

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Mascota)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Mascota_Cliente");

            entity.HasOne(d => d.IdEspecieNavigation).WithMany(p => p.Mascota)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Mascota_Especie");

            entity.HasOne(d => d.IdRazaNavigation).WithMany(p => p.Mascota)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Mascota_Raza");

            entity.HasOne(d => d.IdSexoNavigation).WithMany(p => p.Mascota)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Mascota_Sexo");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__09889210617ED440");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Productos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Producto_Proveedor");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.IdProveedor).HasName("PK__Proveedo__E8B631AFC041219D");

            entity.Property(e => e.Activo).HasDefaultValue(true);
        });

        modelBuilder.Entity<Raza>(entity =>
        {
            entity.HasKey(e => e.IdRaza).HasName("PK__Raza__8F06EB28D553DF48");

            entity.HasOne(d => d.IdEspecieNavigation).WithMany(p => p.Razas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Raza_Especie");
        });

        modelBuilder.Entity<Sexo>(entity =>
        {
            entity.HasKey(e => e.IdSexo).HasName("PK__Sexo__A7739FA2A5E6C3EC");
        });

        modelBuilder.Entity<Tratamiento>(entity =>
        {
            entity.HasKey(e => e.IdTratamiento).HasName("PK__Tratamie__5CB7E7532A06EE80");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF97C9D4AFC5");

            entity.Property(e => e.Activo).HasDefaultValue(true);

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Usuarios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Empleado");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
