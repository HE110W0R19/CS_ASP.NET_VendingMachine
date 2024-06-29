using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Xml.Linq;
using VendingMachine.Core.Models;
using VendingMachine.DataAccess.Entities;

namespace VendingMachine.DataAccess.Repositories
{
    public class DrinksRepository : IDrinksRepository
    {
        private readonly VendingMachineDbContext _context;

        public DrinksRepository(VendingMachineDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Method for GET all drinks (entity request)
        /// </summary>
        /// <returns></returns>
        public async Task<List<Drink>> GetDrinks()
        {
            var drinkEntities = await _context.Drinks
                .AsNoTracking()
                .ToListAsync();

            var drinks = drinkEntities
                .Select(d => Drink.CreateDrink(d.Id, d.Name, d.Description, d.ImageUri, d.Amount, d.Price).Drink)
                .ToList();

            return drinks;
        }


        /// <summary>
        /// Method for create drink (entity request)
        /// </summary>
        /// <param name="drink"></param>
        /// <returns></returns>
        public async Task<Guid> CreateDrink(Drink drink)
        {
            var drinkEntity = new DrinkEntity
            {
                Id = drink.Id,
                Name = drink.Name,
                Description = drink.Description,
                ImageUri = drink.ImageUri,
                Amount = drink.Amount,
                Price = drink.Price
            };

            await _context.Drinks.AddAsync(drinkEntity);
            await _context.SaveChangesAsync();

            return drinkEntity.Id;
        }


        /// <summary>
        /// Method for update drink (entity request)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="discription"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public async Task<Guid> UpdateDrink(Guid id, string name, string discription, string imageUri, int Amount, decimal price)
        {
            await _context.Drinks
                .Where(d => d.Id == id)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(d => d.Name, d => name)
                    .SetProperty(d => d.Description, d => discription)
                    .SetProperty(d => d.ImageUri, d => imageUri)
                    .SetProperty(d => d.Amount, d => Amount)
                    .SetProperty(d => d.Price, d => price));
            return id;
        }

        /// <summary>
        /// Method for update drinks amount (entity request)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public async Task<Guid> UpdateDrinksAmount(Guid id, int amount)
        {
            await _context.Drinks
                .Where(d => d.Id == id)
            .ExecuteUpdateAsync(x => x
            .SetProperty(d => d.Amount, d => amount));
            return id;
        }

        /// <summary>
        /// Method for delete drink for ID (entity request)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Guid> DeleteDrink(Guid id)
        {
            await _context.Drinks
                .Where(d => d.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
