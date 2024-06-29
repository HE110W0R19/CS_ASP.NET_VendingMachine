using VendingMachine.Core.Models;

namespace VendingMachine.Core.Abstractions
{
    public interface ICoinsRepository
    {
        Task<Guid> CreateCoin(Coin coin);
        Task<Guid> DeleteCoin(Guid id);
        Task<List<Coin>> GetCoins();
        Task<Guid> UpdateCoin(Guid Id, int amount, int denomination, bool isAvailable);
        Task<Guid> UpdateCoinAmount(Guid id, int amount);
    }
}
