using HomeBookkeeping.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketManager.Infrastructure.Persistence.Configurations;
public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.Property(user => user.Amount)
            .IsRequired();
        builder.Property(user => user.Comment)
        .HasMaxLength(40)
        .IsRequired();
        builder.Property(user => user.CreatedAt)
            .IsRequired();
        builder.Property(user => user.CategoryId).IsRequired();


    }
}
