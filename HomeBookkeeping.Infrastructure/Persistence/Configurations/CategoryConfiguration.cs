using HomeBookkeeping.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketManager.Infrastructure.Persistence.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(supplier => supplier.CategoryName)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(supplier => supplier.ExpenceIncomeType)
            .HasMaxLength(40)
            .IsRequired();
        }
    }
}
