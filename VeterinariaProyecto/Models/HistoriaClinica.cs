using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VeterinariaProyecto.Models;

[Table("HistoriaClinica")]
public partial class HistoriaClinica
{
    [Key]
    public int IdHistoria { get; set; }

    [Column(TypeName = "date")]
    public DateTime FechaApertura { get; set; } = DateTime.Now;

    [StringLength(300)]
    [Unicode(false)]
    public string? Observaciones { get; set; }

    public int IdMascota { get; set; }

    // Navegación
    [ForeignKey("IdMascota")]
    [InverseProperty("HistoriaClinica")]
    public virtual Mascota IdMascotaNavigation { get; set; } = null!;

    [InverseProperty("IdHistoriaNavigation")]
    public virtual ICollection<ProcedimientoMedico> ProcedimientosMedicos { get; set; } = new List<ProcedimientoMedico>();
}
