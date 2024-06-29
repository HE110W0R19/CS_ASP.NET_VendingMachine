using VendingMachine.Core.Models;

namespace VendingMachine.Application.Services
{
    public interface IDrinksService
    {
        Task<Guid> CreateDrink(Drink drink);
        Task<Guid> DeleteDrink(Guid Id);
        Task<List<Drink>> GetAllDrinks();
        Task<Guid> UpdateDrink(Guid Id, string name, string discription, string imageUri, int amount, decimal price);

        Task<Guid> UpdateDrinksAmount(Guid id, int amount);
    }
}