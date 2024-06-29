using Microsoft.EntityFrameworkCore;
using VendingMachine.DataAccess.Entities;

namespace VendingMachine.DataAccess
{
    public class VendingMachineDbContext : DbContext
    {
        public VendingMachineDbContext(DbContextOptions<VendingMachineDbContext> options) 
            : base(options)
        {
        }

        /// <summary>
        /// Entity data objects (tables)
        /// </summary>
        public DbSet<DrinkEntity> Drinks { get; set; }
        public DbSet<CoinEntity> Coins { get; set; }
    }
}
