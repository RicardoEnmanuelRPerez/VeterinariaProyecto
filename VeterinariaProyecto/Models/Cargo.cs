using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VeterinariaProyecto.Models;

[Table("Cargo")]
public partial class Cargo
{
    [Key]
    public int IdCargo { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string NombreCargo { get; set; } = null!;

    [InverseProperty("IdCargoNavigation")]
    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
