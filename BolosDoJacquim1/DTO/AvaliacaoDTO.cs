using System;
using System.ComponentModel.DataAnnotations;

namespace BolosDoJacquin.DTOs
{
    public class AvaliacaoDTO
    {
        [Required(ErrorMessage = "A nota é obrigatória!")]
        [Range(1, 5, ErrorMessage = "A nota deve ser entre 1 e 5.")]
        public int Nota { get; set; }

        [StringLength(500, ErrorMessage = "O comentário pode ter no máximo 500 caracteres.")]
        public string? Comentario { get; set; }

        [Required(ErrorMessage = "O ID do produto é obrigatório!")]
        public Guid IdProduto { get; set; }

        [Required(ErrorMessage = "O ID do usuário é obrigatório!")]
        public Guid IdUsuario { get; set; }
    }
}