using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Veterinaria_Genesis_DB.Models;

[Table("Factura")]
public partial class Factura
{
    [Key]
    public int IdFactura { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime FechaEmision { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaVencimiento { get; set; }

    public int IdCliente { get; set; }

    public int IdEmpleado { get; set; }

    [InverseProperty("IdFacturaNavigation")]
    public virtual ICollection<FacturaProducto> FacturaProductos { get; set; } = new List<FacturaProducto>();

    [ForeignKey("IdCliente")]
    [InverseProperty("Facturas")]
    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    [ForeignKey("IdEmpleado")]
    [InverseProperty("Facturas")]
    public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;
}
