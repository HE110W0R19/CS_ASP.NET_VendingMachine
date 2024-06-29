using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Core.Abstractions;
using VendingMachine.Core.Models;
using VendingMachine.DataAccess.Entities;

namespace VendingMachine.DataAccess.Repositories
{
    /// <summary>
    /// Entity coin methods
    /// </summary>
    public class CoinsRepository : ICoinsRepository
    {
        private readonly VendingMachineDbContext _context;
        public CoinsRepository(VendingMachineDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method for CREATE coin (entity request)
        /// </summary>
        /// <param name="coin"></param>
        /// <returns></returns>
        public async Task<Guid> CreateCoin(Coin coin)
        {
            var coinEntity = new CoinEntity
            {
                Id = coin.Id,
                Amount = coin.Amount,
                Denomination = coin.Denomination,
                isAvailable = coin.IsAvailable
            };

            await _context.Coins.AddAsync(coinEntity);
            await _context.SaveChangesAsync();

            return coinEntity.Id;
        }

        /// <summary>
        /// Method for DELETE coin (entity request)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Guid> DeleteCoin(Guid id)
        {
            await _context.Coins
                .Where(c => c.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }

        /// <summary>
        /// Method for GET coins (entity request)
        /// </summary>
        /// <returns></returns>
        public async Task<List<Coin>> GetCoins()
        {
            var coinEntities = await _context.Coins
                .AsNoTracking() 
                .ToListAsync();

            var coins = coinEntities
                .Select(c => Coin.CreateCoin(c.Id, c.Amount, c.Denomination, c.isAvailable).Coin)
                .ToList();

            return coins;

        }

        /// <summary>
        /// Method for UPDATE coin (entity request)
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="amount"></param>
        /// <param name="denomination"></param>
        /// <param name="isAvailable"></param>
        /// <returns></returns>
        public async Task<Guid> UpdateCoin(Guid Id, int amount, int denomination ,bool isAvailable)
        {
            await _context.Coins
                .Where(c => c.Id == Id)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(c => c.Amount, c => amount)
                    .SetProperty(c => c.Denomination, c => denomination)
                    .SetProperty(c => c.isAvailable, c => isAvailable));
            return Id;
        }

        public async Task<Guid> UpdateCoinAmount(Guid id, int amount)
        {
            await _context.Coins
                .Where(d => d.Id == id)
            .ExecuteUpdateAsync(x => x
            .SetProperty(d => d.Amount, d => amount));
            return id;
        }
    }
}
