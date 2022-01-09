using Control_Dossier.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Control_Dossier.Data.Mapping;

public class DossierMap : IEntityTypeConfiguration<Dossier>
{
    public void Configure(EntityTypeBuilder<Dossier> builder)
    {
        builder.ToTable("Dossier");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasColumnName("Title")
            .HasColumnType("VARCHAR")
            .HasMaxLength(80);

        builder.Property(x => x.Country)
            .IsRequired()
            .HasColumnName("Country")
            .HasColumnType("VARCHAR")
            .HasMaxLength(80);

        builder.Property(x => x.Content)
            .IsRequired()
            .HasColumnName("Content")
            .HasColumnType("VARCHAR")
            .HasMaxLength(255);

        builder.Property(x => x.Code)
            .IsRequired()
            .HasColumnName("Code")
            .HasColumnType("VARCHAR")
            .HasMaxLength(10);

        builder.Property(x => x.CreateDate)
            .HasColumnName("CreateDate")
            .HasColumnType("SMALLDATETIME")
            .HasDefaultValue(DateTime.Now.ToUniversalTime());

        builder.Property(x => x.LastUpdateDate)
            .HasColumnName("LastUpdateDate")
            .HasColumnType("SMALLDATETIME")
            .HasDefaultValue(DateTime.Now.ToUniversalTime());

        builder.HasIndex(x => x.Code, "IX_Dossier_Code");

        builder.HasOne(x => x.Author)
            .WithMany(x => x.Dossiers)
            .HasConstraintName("FK_Dossier_Author")
            .OnDelete(DeleteBehavior.Cascade);
        

    }
}