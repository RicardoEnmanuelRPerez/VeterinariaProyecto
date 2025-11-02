using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Veterinaria_Genesis_DB.Models;

[Table("ProcedimientoMedico")]
public partial class ProcedimientoMedico
{
    [Key]
    public int IdProcedimiento { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string Tipo { get; set; } = null!; // 'Hospitalizacion', 'Cirugia', 'Otro'

    [Column(TypeName = "date")]
    public DateTime FechaInicio { get; set; }

    [Column(TypeName = "date")]
    public DateTime? FechaFin { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string? Descripcion { get; set; }

    public int IdHistoria { get; set; }

    public int IdVeterinario { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string Estado { get; set; } = "Programado"; // 'Programado', 'EnProgreso', 'Completado', 'Cancelado'

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? CostoEstimado { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? Observaciones { get; set; }

    // Campos específicos para hospitalización
    [StringLength(50)]
    [Unicode(false)]
    public string? Sala { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Cama { get; set; }

    // Campos específicos para cirugía
    [StringLength(50)]
    [Unicode(false)]
    public string? Quirofano { get; set; }

    public int? DuracionEstimada { get; set; } // en minutos

    [StringLength(100)]
    [Unicode(false)]
    public string? TipoAnestesia { get; set; }

    // Navegación
    [ForeignKey("IdHistoria")]
    [InverseProperty("ProcedimientosMedicos")]
    public virtual HistoriaClinica IdHistoriaNavigation { get; set; } = null!;

    [ForeignKey("IdVeterinario")]
    [InverseProperty("ProcedimientosMedicos")]
    public virtual Veterinario IdVeterinarioNavigation { get; set; } = null!;
}
