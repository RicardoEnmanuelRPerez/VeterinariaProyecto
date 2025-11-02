using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Veterinaria_Genesis_DB.Models;

[PrimaryKey("IdFactura", "IdProducto")]
[Table("FacturaProducto")]
public partial class FacturaProducto
{
    [Key]
    public int IdFactura { get; set; }

    [Key]
    public int IdProducto { get; set; }

    public int Cantidad { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal PrecioUnitario { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Total { get; set; }

    [ForeignKey("IdFactura")]
    [InverseProperty("FacturaProductos")]
    public virtual Factura IdFacturaNavigation { get; set; } = null!;

    [ForeignKey("IdProducto")]
    [InverseProperty("FacturaProductos")]
    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
