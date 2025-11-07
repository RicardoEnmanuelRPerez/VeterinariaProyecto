using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VeterinariaProyecto.Models;

[Table("Especialidad")]
public partial class Especialidad
{
    [Key]
    public int IdEspecialidad { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string NombreEspecialidad { get; set; } = null!;

    [InverseProperty("IdEspecialidadNavigation")]
    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}

