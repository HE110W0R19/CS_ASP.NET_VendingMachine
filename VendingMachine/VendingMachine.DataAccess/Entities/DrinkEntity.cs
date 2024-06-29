namespace VendingMachine.DataAccess.Entities
{
    /// <summary>
    /// Entity drink object
    /// </summary>
    public class DrinkEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUri { get; set; } = string.Empty;
        public int Amount { get; set; }
        public decimal Price { get; set; }
    }
}
