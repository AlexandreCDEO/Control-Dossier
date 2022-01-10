using System.ComponentModel.DataAnnotations;

namespace Control_Dossier.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "O E-mail é obrigatório")]
    [EmailAddress (ErrorMessage = "E-mail inválido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória")]
    [MinLength(6, ErrorMessage = "Esse campo requer no mínimo seis caracteres")]
    [MaxLength(20, ErrorMessage = "Esse campo pode ter no máximo 20 caracteres")]
    public string Password { get; set; }
}