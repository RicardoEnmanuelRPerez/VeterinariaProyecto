using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Veterinaria_Genesis_DB.Models;

[Table("Usuario")]
[Index("NombreUsuario", Name = "UQ__Usuario__6B0F5AE0B6A027EA", IsUnique = true)]
public partial class Usuario
{
    [Key]
    public int IdUsuario { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string NombreUsuario { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Contraseña { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string? Rol { get; set; }

    public bool? Activo { get; set; }

    public int IdEmpleado { get; set; }

    [ForeignKey("IdEmpleado")]
    [InverseProperty("Usuarios")]
    public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;
}
