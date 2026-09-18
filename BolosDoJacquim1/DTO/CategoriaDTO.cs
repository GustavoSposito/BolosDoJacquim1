using System.ComponentModel.DataAnnotations;

namespace BolosDoJacquin.DTOs // ou BolosDoJacquin.ViewModels
{
    public class CategoriaDTO
    {
        [Required(ErrorMessage = "O nome da categoria é obrigatório!")]
        [StringLength(100, ErrorMessage = "O nome da categoria deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;
    }
}