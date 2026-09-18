using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BolosDoJacquin.Models
{
    public partial class Produto
    {
        [Key]
        public Guid IdProduto { get; set; }

        [StringLength(150)]
        [Unicode(false)]
        public string Nome { get; set; } = null!;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Preco { get; set; }
        

        [StringLength(500)]
        [Unicode(false)]
        public string EnderecoImagem { get; set; } = null!;

        [Unicode(false)]
        public string Descricao { get; set; } = null!;

        public bool Disponibilidade { get; set; }

        // Chave Estrangeira
        public Guid IdCategoria { get; set; }
        public virtual Categoria IdCategoriaNavigation { get; set; }

        // Propriedade de Navegação ajustada para 'Categoria'
        [ForeignKey(nameof(IdCategoria))]
        public virtual Categoria? Categoria { get; set; }

        // Relacionamento com Avaliações
        public virtual ICollection<Avaliacao> Avaliacao { get; set; } = new List<Avaliacao>();
    }
}