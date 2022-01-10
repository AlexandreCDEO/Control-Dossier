using System.ComponentModel.DataAnnotations;

namespace Control_Dossier.ViewModels.Dossier;

public class UpdateDossierViewModel
{
    [Required(ErrorMessage = "Este campo é obrigatório")]
    [MinLength (3,ErrorMessage = "Este campo exige ao menos 3 caracteres")]
    public string Title { get; set; }
    
    [Required(ErrorMessage = "Este campo é obrigatório")]
    [MinLength (3,ErrorMessage = "Este campo exige ao menos 3 caracteres")]
    public string Country { get; set; }
    
    [Required(ErrorMessage = "Este campo é obrigatório")]
    public string Code { get; set; }
    
    [Required(ErrorMessage = "Este campo é obrigatório")]
    [MinLength (3,ErrorMessage = "Este campo exige ao menos 3 caracteres")]
    public string Content { get; set; }
}