using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace Control_Dossier.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "O E-mail é obrigatório")]
    [EmailAddress(ErrorMessage = "E-mail inválido")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Digite uma senha")]
    public string Password { get; set; }
}