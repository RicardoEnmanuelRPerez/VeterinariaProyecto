using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Veterinaria_Genesis_DB.Models;

[Table("Tratamiento")]
public partial class Tratamiento
{
    [Key]
    public int IdTratamiento { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string NombreTratamiento { get; set; } = null!;

    [StringLength(500)]
    [Unicode(false)]
    public string? Descripcion { get; set; }

    [InverseProperty("IdTratamientoNavigation")]
    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();
}
