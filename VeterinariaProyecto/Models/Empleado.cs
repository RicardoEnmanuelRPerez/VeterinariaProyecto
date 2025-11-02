using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Veterinaria_Genesis_DB.Models;

[Table("Empleado")]
public partial class Empleado
{
    [Key]
    public int IdEmpleado { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Nombres { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Apellidos { get; set; } = null!;

    public int? IdEspecialidad { get; set; }

    public int? IdCargo { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string? Telefono { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Correo { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Direccion { get; set; } = null!;

    public TimeOnly HoraEntrada { get; set; }

    public TimeOnly HoraSalida { get; set; }

    public bool? Estado { get; set; }

    [InverseProperty("IdEmpleadoNavigation")]
    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    [ForeignKey("IdCargo")]
    [InverseProperty("Empleados")]
    public virtual Cargo? IdCargoNavigation { get; set; }

    [ForeignKey("IdEspecialidad")]
    [InverseProperty("Empleados")]
    public virtual Especialidad? IdEspecialidadNavigation { get; set; }

    [InverseProperty("IdEmpleadoNavigation")]
    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}

