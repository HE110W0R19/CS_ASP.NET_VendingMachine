namespace VendingMachine.Core.Models
{
    //Паттерн Фабричный метод
    public class Drink
    {
        //Drink components
        public Guid Id { get; }
        public string Name { get; } = string.Empty;
        public string Description { get; } = string.Empty;
        public string ImageUri { get; } = string.Empty;
        public int Amount { get; }
        public decimal Price { get; }

        private Drink(Guid id, string name, string discription, string imageUri, int amount, decimal price)
        {
            Id = id;
            Name = name;
            Description = discription;
            ImageUri = imageUri;
            Amount = amount;
            Price = price;
        }

        /// <summary>
        /// Method for create drink or error if drink have invalid datas
        /// </summary>
        /// <param name="Id"> Id for drink</param>
        /// <param name="Name">Name (35 > name.length > 0)</param>
        /// <param name="Discription">Discription (100 > discription.length > 0)</param>
        /// <param name="ImageUri">Image Uri cant be empty</param>
        /// <param name="Price"> Price for name as decimal</param>
        /// <returns></returns>
        public static (Drink Drink, string Error) CreateDrink(Guid Id, string Name, string Discription, string ImageUri, int Amount, decimal Price)
        {
            var error = string.Empty;

            error = DrinkIsExist(Id, Name, Discription, ImageUri, Amount, Price);

            var drink = new Drink(Id, Name, Discription, ImageUri, Amount, Price);

            return (drink, error);
        }


        /// <summary>
        /// Method for check exist a drink
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <param name="Discription"></param>
        /// <param name="ImageUri"></param>
        /// <param name="Amount"></param>
        /// <param name="Price"></param>
        /// <returns></returns>
        public static string DrinkIsExist(Guid Id, string Name, string Discription, string ImageUri, int Amount, decimal Price)
        {
            //Cheking validation for name
            if (string.IsNullOrEmpty(Name.Trim()) || Name.Length > (int)DrinkValidation.MAX_NAME_LENGTH)
            {
                return $"{nameof(Name)} cant be empty and length cant be longer then {(int)DrinkValidation.MAX_NAME_LENGTH} symbols";
            }

            //Checking validation  for discription
            if (string.IsNullOrEmpty(Discription.Trim()) || Discription.Length > (int)DrinkValidation.MAX_DESCRIPTION_LENGTH)
            {
                return $"{nameof(Discription)} cant be empty and length cant be longer then {(int)DrinkValidation.MAX_NAME_LENGTH} symbols";
            }

            //Checking validation for image uri
            if (string.IsNullOrEmpty(ImageUri.Trim()))
            {
                return $"{nameof(ImageUri)} cant be empty";
            }

            if (Amount <= 0)
            {
                return $"{nameof(Amount)} should be more (not 0)";
            }

            return "";
        }

    }

    //Named constants for validation
    internal enum DrinkValidation : int
    {
        MAX_NAME_LENGTH = 35,
        MAX_DESCRIPTION_LENGTH = 100
    }
}
