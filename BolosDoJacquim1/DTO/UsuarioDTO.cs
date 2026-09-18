using System;
using System.ComponentModel.DataAnnotations;

namespace BolosDoJacquin.DTOs
{
    public class UsuarioDTO
    {
        [Required(ErrorMessage = "O nome é obrigatório!")]
        [StringLength(150, ErrorMessage = "O nome deve ter no máximo 150 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O email é obrigatório!")]
        [EmailAddress(ErrorMessage = "Informe um email válido.")]
        [StringLength(150, ErrorMessage = "O email deve ter no máximo 150 caracteres.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória!")]
        [StringLength(255, MinimumLength = 6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
        public string Senha { get; set; } = string.Empty;

        public Guid? IdTipoUsuario { get; set; }
    }
}