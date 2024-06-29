namespace VendingMachine.Contracts
{
    /// <summary>
    /// For requests with Coins
    /// </summary>
    /// <param name="Amount"></param>
    /// <param name="Denomination"></param>
    /// <param name="isAvailable"></param>
    public record CoinRequest
    (
        int Amount,
        int Denomination,
        bool isAvailable
    );
}
