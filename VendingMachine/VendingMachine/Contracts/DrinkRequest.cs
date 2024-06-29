namespace VendingMachine.Contracts
{
    /// <summary>
    /// For requests with Drinks
    /// </summary>
    /// <param name="Name"></param>
    /// <param name="Discription"></param>
    /// <param name="ImageUri"></param>
    /// <param name="Price"></param>
    public record DrinkRequest
    (
        string Name,
        string Discription,
        string ImageUri,
        int Amount,
        decimal Price
    );
}
