namespace VendingMachine.Contracts
{
    /// <summary>
    /// For responces with Coins
    /// </summary>
    /// <param name="Amount"></param>
    /// <param name="Denomination"></param>
    /// <param name="isAvailable"></param>
    public record CoinResponce
    (
        Guid Id,
        int Amount,
        int Denomination,
        bool isAvailable
    );
}
