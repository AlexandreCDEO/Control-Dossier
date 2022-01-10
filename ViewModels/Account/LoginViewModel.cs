using System.ComponentModel.DataAnnotations;

namespace Control_Dossier.ViewModels.Account;

public class LoginViewModel
{
    [Required(ErrorMessage = "O E-mail é obrigatório")]
    [EmailAddress(ErrorMessage = "E-mail inválido")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Digite uma senha")]
    public string Password { get; set; }
}