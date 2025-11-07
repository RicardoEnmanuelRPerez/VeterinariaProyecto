using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VeterinariaProyecto.Models;

[Table("Producto")]
public partial class Producto
{
    [Key]
    public int IdProducto { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [StringLength(500)]
    [Unicode(false)]
    public string? Descripcion { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Tipo { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? UnidadMedida { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal PrecioCompra { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal PrecioVenta { get; set; }

    public int StockActual { get; set; }

    public int StockMinimo { get; set; }

    public DateOnly FechaVencimiento { get; set; }

    public int IdProveedor { get; set; }

    [InverseProperty("IdProductoNavigation")]
    public virtual ICollection<FacturaProducto> FacturaProductos { get; set; } = new List<FacturaProducto>();

    [ForeignKey("IdProveedor")]
    [InverseProperty("Productos")]
    public virtual Proveedor IdProveedorNavigation { get; set; } = null!;
}
