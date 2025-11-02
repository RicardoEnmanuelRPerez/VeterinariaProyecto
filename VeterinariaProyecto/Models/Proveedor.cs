using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Veterinaria_Genesis_DB.Models;

[Table("Proveedor")]
public partial class Proveedor
{
    [Key]
    public int IdProveedor { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string? Contacto { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? Telefono { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Correo { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? Direccion { get; set; }

    public bool? Activo { get; set; }

    [InverseProperty("IdProveedorNavigation")]
    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
