using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VendingMachine.Core.Models;
using VendingMachine.DataAccess.Entities;

namespace VendingMachine.DataAccess.Configurations
{
    public class DrinkConfiguration : IEntityTypeConfiguration<DrinkEntity>
    {
        /// <summary>
        /// Configs for DrinkEF object
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<DrinkEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(Drink.MAX_NAME_LENGTH)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(Drink.MAX_DESCRIPTION_LENGTH)
                .IsRequired();

            builder.Property(x => x.ImageUri)
                .IsRequired();

            builder.Property(x => x.Amount)
                .IsRequired();

            builder.Property(x => x.Price)
                .IsRequired();

        }
    }
}
