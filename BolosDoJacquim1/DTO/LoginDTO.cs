using System.ComponentModel.DataAnnotations;

namespace BolosDoJacquin.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Informe o e-mail do usuário!")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido!")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe a senha do usuário!")]
        public string Senha { get; set; } = string.Empty;
    }
}