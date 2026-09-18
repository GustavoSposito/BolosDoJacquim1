using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BolosDoJacquin.Models
{
    public partial class Avaliacao
    {
        [Key]
        public Guid IdAvaliacao { get; set; }

        public int Nota { get; set; }

        [StringLength(500)]
        [Unicode(false)]
        public string? Comentario { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.Now;

        // Chaves Estrangeiras
        public Guid IdProduto { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
        public virtual Produto IdProdutoNavigation { get; set; }
        public Guid IdUsuario { get; set; }

        // Propriedades de Navegação ajustadas para Produto e Usuario
        [ForeignKey(nameof(IdProduto))]
        public virtual Produto? Produto { get; set; }

        [ForeignKey(nameof(IdUsuario))]
        public virtual Usuario? Usuario { get; set; }
    }
}