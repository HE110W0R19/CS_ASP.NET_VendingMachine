using VendingMachine.Core.Abstractions;
using VendingMachine.Core.Models;

namespace VendingMachine.Application.Services
{
    public class CoinsService : ICoinService
    {
        private readonly ICoinsRepository _coinsRepository;
        public CoinsService(ICoinsRepository coinsRepository)
        {
            _coinsRepository = coinsRepository;
        }

        public async Task<List<Coin>> GetAllCoins()
        {
            return await _coinsRepository.GetCoins();
        }

        public async Task<Guid> CreateCoin(Coin coin)
        {
            return await _coinsRepository.CreateCoin(coin);
        }

        public async Task<Guid> UpdateCoin(Guid id, int amount, int denomination, bool isAvailable)
        {
            return await _coinsRepository.UpdateCoin(id, amount, denomination, isAvailable);
        }

        public async Task<Guid> DeleteCoin(Guid id)
        {
            return await _coinsRepository.DeleteCoin(id);
        }

        public async Task<Guid> UpdateCoinAmount(Guid id, int amount)
        {
            return await (_coinsRepository.UpdateCoinAmount(id, amount));
        }
    }
}
