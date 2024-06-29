using VendingMachine.Core.Models;

namespace VendingMachine.Application.Services
{
    public interface ICoinService
    {
        Task<Guid> CreateCoin(Coin coin);
        Task<Guid> DeleteCoin(Guid id);
        Task<List<Coin>> GetAllCoins();
        Task<Guid> UpdateCoin(Guid id, int amount, int denomination, bool isAvailable);
        Task<Guid> UpdateCoinAmount(Guid id, int amount);
    }
}