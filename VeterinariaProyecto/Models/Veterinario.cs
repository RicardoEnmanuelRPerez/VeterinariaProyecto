using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VeterinariaProyecto.Models;

[Table("Veterinario")]
public partial class Veterinario
{
    [Key]
    public int IdVeterinario { get; set; }

    [StringLength(120)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string? Especialidad { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? Telefono { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Correo { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? NumeroColegiatura { get; set; }

    public bool Activo { get; set; } = true;

    // Navegación
    [InverseProperty("IdVeterinarioNavigation")]
    public virtual ICollection<ProcedimientoMedico> ProcedimientosMedicos { get; set; } = new List<ProcedimientoMedico>();
}
