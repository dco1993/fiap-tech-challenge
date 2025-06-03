using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Configurations
{
    public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.ToTable("DISCOUNT");

            builder.HasKey(d => d.Id);

            builder
                .Property(d => d.IdGame)
                .IsRequired()
                .HasColumnName("ID_GAME");

            builder
                .Property(d => d.StartDiscount)
                .IsRequired()
                .HasColumnName("START_DISCOUNT");

            builder
                .Property(d => d.EndDiscount)
                .IsRequired()
                .HasColumnName("END_DISCOUNT");

            builder
                .Property(d => d.PercentOff)
                .IsRequired()
                .HasColumnName("PERCENT_OFF");

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
                .HasOne(d => d.Game)
                .WithMany(g => g.Discounts)
                .HasForeignKey(d => d.IdGame)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasIndex(d => d.IdGame).HasDatabaseName("IX_Discount_IdGame");
        }
    }
}