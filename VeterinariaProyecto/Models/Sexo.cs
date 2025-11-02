using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Veterinaria_Genesis_DB.Models;

[Table("Sexo")]
public partial class Sexo
{
    [Key]
    public int IdSexo { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string NombreSexo { get; set; } = null!;

    [InverseProperty("IdSexoNavigation")]
    public virtual ICollection<Mascota> Mascota { get; set; } = new List<Mascota>();
}
