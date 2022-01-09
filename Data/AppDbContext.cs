using Control_Dossier.Data.Mapping;
using Control_Dossier.Models;
using Microsoft.EntityFrameworkCore;

namespace Control_Dossier.Data;

public class AppDbContext : DbContext 
{
    public DbSet<Dossier> Dossiers { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer("Server=localhost,1433;Database=ControlDossier;User ID=sa;Password=Password.1");
        options.LogTo(Console.WriteLine);

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DossierMap());
        modelBuilder.ApplyConfiguration(new RoleMap());
        modelBuilder.ApplyConfiguration(new UserMap());
    }
}