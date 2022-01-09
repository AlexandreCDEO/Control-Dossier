namespace Control_Dossier.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public IList<Dossier> Dossiers { get; set; }
    public IList<Role> Roles { get; set; }
}