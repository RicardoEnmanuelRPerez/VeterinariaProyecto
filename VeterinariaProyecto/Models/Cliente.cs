using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Veterinaria_Genesis_DB.Models;

[Table("Cliente")]
[Index("Dni", Name = "UQ__Cliente__C0308575D20E1AB0", IsUnique = true)]
public partial class Cliente
{
    [Key]
    public int IdCliente { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Nombres { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Apellidos { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Direccion { get; set; } = null!;

    [StringLength(15)]
    [Unicode(false)]
    public string? Telefono { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Correo { get; set; }

    [StringLength(16)]
    [Unicode(false)]
    public string? Dni { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime FechaRegistro { get; set; }

    [InverseProperty("IdClienteNavigation")]
    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    [InverseProperty("IdClienteNavigation")]
    public virtual ICollection<Mascota> Mascota { get; set; } = new List<Mascota>();
}
