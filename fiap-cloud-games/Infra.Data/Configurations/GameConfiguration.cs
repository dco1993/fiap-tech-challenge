using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Configurations
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.ToTable("GAME");
            builder.HasKey(g => g.Id);

            builder
                .Property(g => g.Id)
                .HasColumnType("INT")
                .HasColumnName("ID")
                .UseIdentityColumn();

            builder
                .Property(g => g.Title)
                .HasMaxLength(100)
                .HasColumnName("TITLE")
                .IsRequired();

            builder
                .Property(g => g.Genre)
                .HasMaxLength(50)
                .HasColumnName("GENRE")
                .IsRequired();

            builder
                .Property(g => g.Metacritic)
                .HasColumnType("DECIMAL(3,1)")
                .HasColumnName("METACRITIC");

            builder
                .Property(g => g.Developer)
                .HasMaxLength(50)
                .HasColumnName("DEVELOPER")
                .IsRequired();

            builder
                .Property(g => g.Publisher)
                .HasMaxLength(50)
                .HasColumnName("PUBLISHER")
                .IsRequired();

            builder
                .Property(g => g.ReleaseDate)
                .HasColumnType("DATETIME")
                .HasColumnName("RELEASE_DATE");

            builder
                .Property(g => g.About)
                .HasColumnType("VARCHAR(MAX)")
                .HasColumnName("ABOUT")
                .IsRequired();

            builder
                .Property(g => g.Price)
                .IsRequired()
                .HasColumnType("DECIMAL(10,2)")
                .HasColumnName("PRICE");

            builder
                .Property(g => g.Status)
                .HasColumnType("BIT")
                .HasColumnName("STATUS")
                .HasDefaultValue(1)
                .IsRequired();

            builder
                .Property(g => g.DhTimestamp)
                .HasColumnType("DATETIME")
                .HasColumnName("DH_TIMESTAMP")
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            builder
                .HasMany(g => g.UserGame)
                .WithOne(ug => ug.Game)
                .HasForeignKey(ug => ug.IdGame)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(g => g.Title).HasDatabaseName("IX_Game_Title");
            
            builder.HasIndex(g => g.Genre).HasDatabaseName("IX_Game_Genre");
        }
    }
}
