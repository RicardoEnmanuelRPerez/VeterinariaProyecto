using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VeterinariaProyecto.Models;

[Table("Especie")]
public partial class Especie
{
    [Key]
    public int IdEspecie { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string NombreEspecie { get; set; } = null!;

    [InverseProperty("IdEspecieNavigation")]
    public virtual ICollection<Mascota> Mascota { get; set; } = new List<Mascota>();

    [InverseProperty("IdEspecieNavigation")]
    public virtual ICollection<Raza> Razas { get; set; } = new List<Raza>();
}
