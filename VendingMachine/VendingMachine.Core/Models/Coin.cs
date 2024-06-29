namespace VendingMachine.Core.Models
{

    public class Coin
    {
        //Coin components
        public Guid Id { get; }
        public int Amount { get; } = 0;
        public int Denomination { get; }
        public bool IsAvailable { get; }

        private Coin(Guid id, int amount, int denomination, bool isAvailable)
        {
            Id = id;
            Amount = amount;
            Denomination = denomination;
            IsAvailable = isAvailable;
        }

        /// <summary>
        /// Method for create Coin, or error
        /// </summary>
        /// <param name="id"></param>
        /// <param name="amount"></param>
        /// <param name="denomination"></param>
        /// <param name="isAvailable"></param>
        /// <returns></returns>
        public static (Coin Coin, string Error) CreateCoin(Guid id, int amount, int denomination, bool isAvailable)
        {
            var error = string.Empty;

            error = CoinIsExist(id, amount, denomination, isAvailable);

            var coin = new Coin(id, amount, denomination, isAvailable);

            return (coin, error);
        }

        /// <summary>
        /// Checking coin exist
        /// </summary>
        /// <param name="id"></param>
        /// <param name="amount"></param>
        /// <param name="denomination"></param>
        /// <param name="isAvailable"></param>
        /// <returns></returns>
        public static string CoinIsExist(Guid id, int amount, int denomination, bool isAvailable)
        {
            if (amount > int.MaxValue)
            {
                return $"{nameof(amount)} cant be over {int.MaxValue}";
            }

            if (denomination != (int)CoinValidation.DENOMINATION_ONE
                & denomination != (int)CoinValidation.DENOMINATION_TWO
                & denomination != (int)CoinValidation.DENOMINATION_FIVE
                & denomination != (int)CoinValidation.DENOMINATION_TEN)
            {
                return $"{nameof(denomination)} should be {(int)CoinValidation.DENOMINATION_ONE}, {(int)CoinValidation.DENOMINATION_TWO}, " +
                    $"{(int)CoinValidation.DENOMINATION_FIVE}, {(int)CoinValidation.DENOMINATION_TEN}";
            }

            return "";
        }
    }

    //Named constants for validation
    enum CoinValidation : int
    {
        DENOMINATION_ONE = 1,
        DENOMINATION_TWO = 2,
        DENOMINATION_FIVE = 5,
        DENOMINATION_TEN = 10,
    }
}
