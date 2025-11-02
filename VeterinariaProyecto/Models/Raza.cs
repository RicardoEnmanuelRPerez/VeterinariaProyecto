using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Veterinaria_Genesis_DB.Models;

[Table("Raza")]
[Index("NombreRaza", "IdEspecie", Name = "UQ_Raza_Especie", IsUnique = true)]
public partial class Raza
{
    [Key]
    public int IdRaza { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string NombreRaza { get; set; } = null!;

    public int IdEspecie { get; set; }

    [ForeignKey("IdEspecie")]
    [InverseProperty("Razas")]
    public virtual Especie IdEspecieNavigation { get; set; } = null!;

    [InverseProperty("IdRazaNavigation")]
    public virtual ICollection<Mascota> Mascota { get; set; } = new List<Mascota>();
}
