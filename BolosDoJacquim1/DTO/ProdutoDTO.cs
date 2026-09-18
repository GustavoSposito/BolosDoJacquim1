using System;
using System.ComponentModel.DataAnnotations;

namespace BolosDoJacquin.DTOs
{
    public class ProdutoDTO
    {
        [Required(ErrorMessage = "O nome do produto é obrigatório!")]
        [StringLength(150, ErrorMessage = "O nome do produto deve ter no máximo 150 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O preço é obrigatório!")]
        [Range(0.01, 999999.99, ErrorMessage = "Informe um preço válido.")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "O endereço da imagem é obrigatório!")]
        [StringLength(500, ErrorMessage = "O endereço da imagem deve ter no máximo 500 caracteres.")]
        public string EnderecoImagem { get; set; } = string.Empty;

        [Required(ErrorMessage = "A descrição é obrigatória!")]
        public string Descricao { get; set; } = string.Empty;

        public bool Disponibilidade { get; set; } = true;

        [Required(ErrorMessage = "A categoria é obrigatória!")]
        public Guid IdCategoria { get; set; }
    }
}