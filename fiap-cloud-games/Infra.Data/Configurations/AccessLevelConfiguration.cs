using Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Configurations
{
    public class AccessLevelConfiguration : IEntityTypeConfiguration<AccessLevel>
    {
        public void Configure(EntityTypeBuilder<AccessLevel> builder)
        {
            builder.ToTable("ACCESS_LEVEL");
            builder.HasKey(a => a.Id);

            builder
                .Property(a => a.Id)
                .HasColumnType("INT")
                .HasColumnName("ID")
                .UseIdentityColumn();

            builder
                .Property(a => a.Name)
                .HasMaxLength(50)
                .HasColumnName("NAME")
                .IsRequired();

            builder
                .Property(a => a.Status)
                .HasColumnType("BIT")
                .HasColumnName("STATUS")
                .HasDefaultValue(1)
                .IsRequired();

            builder
                .Property(a => a.DhTimestamp)
                .HasColumnType("DATETIME")
                .HasColumnName("DH_TIMESTAMP")
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            builder
                .HasMany(al => al.User)
                .WithOne(u => u.AccessLevel)
                .HasForeignKey(u => u.IdAccessLevel);
        }
    }
}
