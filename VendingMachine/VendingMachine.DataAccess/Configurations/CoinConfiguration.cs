using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Core.Models;
using VendingMachine.DataAccess.Entities;

namespace VendingMachine.DataAccess.Configurations
{
    public class CoinConfiguration : IEntityTypeConfiguration<CoinEntity>
    {
        /// <summary>
        /// Configs for Coin EF object
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<CoinEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Amount)
                .HasMaxLength(int.MaxValue)
                .IsRequired();

            builder.Property(x => x.Denomination)
                .HasMaxLength(Coin.DENOMINATION_ONE)
                .HasMaxLength(Coin.DENOMINATION_TWO)
                .HasMaxLength(Coin.DENOMINATION_FIVE)
                .HasMaxLength(Coin.DENOMINATION_TEN)
                .IsRequired();

            builder.Property(x => x.isAvailable)
                .IsRequired();
        }
    }
}
