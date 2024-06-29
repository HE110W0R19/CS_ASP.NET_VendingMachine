using VendingMachine.Core.Models;

namespace VendingMachine.DataAccess.Repositories
{
    public interface IDrinksRepository
    {
        Task<Guid> CreateDrink(Drink drink);
        Task<Guid> DeleteDrink(Guid id);
        Task<List<Drink>> GetDrinks();
        Task<Guid> UpdateDrink(Guid id, string name, string discription, string imageUri, int amount, decimal price);

        Task<Guid> UpdateDrinksAmount(Guid id, int amount);
    }
}