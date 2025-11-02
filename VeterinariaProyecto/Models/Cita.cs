using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Veterinaria_Genesis_DB.Models;

public partial class Cita
{
    [Key]
    public int IdCita { get; set; }

    public int IdMascota { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Fecha { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string Motivo { get; set; } = null!;

    [StringLength(500)]
    [Unicode(false)]
    public string? Diagnostico { get; set; }

    public int? IdTratamiento { get; set; }

    [ForeignKey("IdMascota")]
    [InverseProperty("Cita")]
    public virtual Mascota IdMascotaNavigation { get; set; } = null!;

    [ForeignKey("IdTratamiento")]
    [InverseProperty("Cita")]
    public virtual Tratamiento? IdTratamientoNavigation { get; set; }
}
