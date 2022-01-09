namespace Control_Dossier.Models;

public class Dossier
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Country { get; set; }
    public string Code { get; set; }
    public string Content { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime LastUpdateDate { get; set; }
    public User Author { get; set; }
}