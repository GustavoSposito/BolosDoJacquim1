using System.ComponentModel.DataAnnotations;

namespace BolosDoJacquin.DTO;

/// <summary>
/// Data transfer object (DTO) para cadastro e atualização do tipo de usuário.
/// </summary>
public class TipoUsuarioDTO
{
    /// <summary>
    /// Titulo do tipo de usuario
    /// </summary>
    [Required(ErrorMessage = "O titulo do tipo de usuario é obrigatorio.")]
    [StringLength(100, ErrorMessage = "O titulo poder ter no maximo 100 caracteres.")]
    public string Titulo { get; set; } = string.Empty;
}