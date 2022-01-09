namespace Control_Dossier.Models;

public class Role
{
    public int Id { get; set; }
    public string Title { get; set; }
    public IList<User> Users { get; set; }
}