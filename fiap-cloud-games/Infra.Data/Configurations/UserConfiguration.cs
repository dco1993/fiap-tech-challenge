using Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("USER");
            builder.HasKey(u => u.Id);

            builder
                .Property(u => u.Id)
                .HasColumnType("INT")
                .HasColumnName("ID")
                .UseIdentityColumn();

            builder
                .Property(u => u.Name)
                .HasMaxLength(100)
                .HasColumnName("NAME")
                .IsRequired();

            builder
                .Property(u => u.Email)
                .HasMaxLength(100)
                .HasColumnName("EMAIL")
                .IsRequired();

            builder
                .Property(u => u.Password)
                .HasColumnType("VARCHAR(MAX)")
                .HasColumnName("PASSWORD")
                .IsRequired();

            builder
                .Property(u => u.IdAccessLevel)
                .HasColumnType("INT")
                .HasColumnName("ID_ACCESS_LEVEL")
                .HasDefaultValueSql("2")
                .IsRequired();

            builder
                .Property(u => u.Status)
                .HasColumnType("BIT")
                .HasColumnName("STATUS")
                .HasDefaultValue(1)
                .IsRequired();

            builder
                .Property(u => u.DhTimestamp)
                .HasColumnType("DATETIME")
                .HasColumnName("DH_TIMESTAMP")
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            builder
                .HasOne(u => u.AccessLevel)
                .WithMany(al => al.User)
                .HasForeignKey(u => u.IdAccessLevel);

            builder
                .HasMany(u => u.UserGame)
                .WithOne(ug => ug.User)
                .HasForeignKey(ug => ug.IdUser)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(u => u.Email).IsUnique().HasDatabaseName("IX_User_Email");

            builder.HasIndex(u => u.IdAccessLevel).HasDatabaseName("IX_User_IdAccessLevel");
        }
    }
}
