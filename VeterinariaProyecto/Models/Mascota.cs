using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VeterinariaProyecto.Models;

public partial class Mascota
{
    [Key]
    public int IdMascota { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    public int IdCliente { get; set; }

    public int IdEspecie { get; set; }

    public int IdRaza { get; set; }

    public int IdSexo { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal Peso { get; set; }

    public bool? Esterilizado { get; set; }

    [InverseProperty("IdMascotaNavigation")]
    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    [ForeignKey("IdCliente")]
    [InverseProperty("Mascota")]
    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    [ForeignKey("IdEspecie")]
    [InverseProperty("Mascota")]
    public virtual Especie IdEspecieNavigation { get; set; } = null!;

    [ForeignKey("IdRaza")]
    [InverseProperty("Mascota")]
    public virtual Raza IdRazaNavigation { get; set; } = null!;

    [ForeignKey("IdSexo")]
    [InverseProperty("Mascota")]
    public virtual Sexo IdSexoNavigation { get; set; } = null!;

    [InverseProperty("IdMascotaNavigation")]
    public virtual HistoriaClinica? HistoriaClinica { get; set; }
}
