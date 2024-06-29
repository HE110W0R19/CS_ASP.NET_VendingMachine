using VendingMachine.Core.Models;
using VendingMachine.DataAccess.Repositories;

namespace VendingMachine.Application.Services
{
    public class DrinksService : IDrinksService
    {
        private readonly IDrinksRepository _drinksRepository;
        public DrinksService(IDrinksRepository drinksRepository)
        {
            _drinksRepository = drinksRepository;
        }

        public async Task<List<Drink>> GetAllDrinks()
        {
            return await _drinksRepository.GetDrinks();
        }

        public async Task<Guid> CreateDrink(Drink drink)
        {
            return await _drinksRepository.CreateDrink(drink);
        }

        public async Task<Guid> UpdateDrink(Guid Id, string name, string discription, string imageUri, int amount, decimal price)
        {
            return await _drinksRepository.UpdateDrink(Id, name, discription, imageUri, amount, price);
        }

        public async Task<Guid> DeleteDrink(Guid Id)
        {
            return await _drinksRepository.DeleteDrink(Id);
        }

        public async Task<Guid> UpdateDrinksAmount(Guid id, int amount)
        {
            return await _drinksRepository.UpdateDrinksAmount(id, amount);
        }
    }
}
