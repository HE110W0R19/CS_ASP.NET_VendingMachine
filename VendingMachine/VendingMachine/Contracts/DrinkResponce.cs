namespace VendingMachine.Contracts
{
    /// <summary>
    /// For responces with drinks
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Name"></param>
    /// <param name="Discription"></param>
    /// <param name="ImageUri"></param>
    /// <param name="Amount"></param>
    /// <param name="Price"></param>
    public record DrinkResponce
    (
        Guid Id,
        string Name,
        string Discription,
        string ImageUri,
        int Amount,
        decimal Price
    );
}
