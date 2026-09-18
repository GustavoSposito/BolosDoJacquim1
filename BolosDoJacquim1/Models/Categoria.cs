using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace BolosDoJacquin.Models;

[Index("Nome", Name = "UQ_Categoria_Nome", IsUnique = true)]
public partial class Categoria
{
    [Key]
    public Guid IdCategoria { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Nome { get; set; } = null!;


    [InverseProperty("IdCategoriaNavigation")]
    public virtual ICollection<Produto> Produto { get; set; } = new List<Produto>();
}
