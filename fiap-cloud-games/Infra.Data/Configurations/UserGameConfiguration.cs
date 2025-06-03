using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Configurations
{
    public class UserGameConfiguration : IEntityTypeConfiguration<UserGame>
    {
        public void Configure(EntityTypeBuilder<UserGame> builder)
        {
            builder.ToTable("USER_GAME");
            builder.HasKey(ug => ug.Id);

            builder
                .Property(g => g.Id)
                .HasColumnType("INT")
                .HasColumnName("ID")
                .UseIdentityColumn();

            builder
                .Property(ug => ug.IdUser)
                .HasColumnType("INT")
                .HasColumnName("ID_USER")
                .IsRequired();

            builder
                .Property(ug => ug.IdGame)
                .HasColumnType("INT")
                .HasColumnName("ID_GAME")
                .IsRequired();

            builder
                .Property(ug => ug.Status)
                .HasColumnType("BIT")
                .HasColumnName("STATUS")
                .HasDefaultValue(1)
                .IsRequired();

            builder
                .Property(ug => ug.DhTimestamp)
                .HasColumnType("DATETIME")
                .HasColumnName("DH_TIMESTAMP")
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            builder
                .HasOne(ug => ug.User)
                .WithMany(u => u.UserGame)
                .HasForeignKey(ug => ug.IdUser);

            builder
                .HasOne(ug => ug.Game)
                .WithMany(g => g.UserGame)
                .HasForeignKey(ug => ug.IdGame);

            builder.HasIndex(ug => ug.IdUser).HasDatabaseName("IX_UserGame_IdUser");

            builder.HasIndex(ug => ug.IdGame).HasDatabaseName("IX_UserGame_IdGame");
        }
    }
}
