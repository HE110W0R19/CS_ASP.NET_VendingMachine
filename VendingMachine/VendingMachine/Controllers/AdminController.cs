using Microsoft.AspNetCore.Mvc;
using VendingMachine.Application.Services;
using VendingMachine.Contracts;
using VendingMachine.Core.Models;

namespace VendingMachine.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class AdminController : Controller
    {
        private readonly IDrinksService _drinksService;
        private readonly ICoinService _coinService;

        public AdminController(IDrinksService drinksService, ICoinService coinService)
        {
            _drinksService = drinksService;
            _coinService = coinService;
        }

        /// <summary>
        /// Get method for drinks
        /// </summary>
        /// <returns></returns>
        [HttpGet("Drink")]
        public async Task<ActionResult<List<DrinkResponce>>> GetDrinks()
        {
            var drinks = await _drinksService.GetAllDrinks();

            var responce = drinks.Select(d => new DrinkResponce(d.Id, d.Name, d.Description, d.ImageUri, d.Amount, d.Price));

            return Ok(responce.ToList());
        }

        /// <summary>
        /// Post method for drinks
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Drink")]
        public async Task<ActionResult<Guid>> CreateDrink([FromBody] DrinkRequest request)
        {
            var drinks = _drinksService.GetAllDrinks().Result;

            var drinkFromMachine = drinks.Select(d => d)
                .Where(d => d.Name.Trim().ToUpper() == request.Name.Trim().ToUpper())
                .FirstOrDefault();
            
            if (drinkFromMachine != null)
            {
                //if DB contain this drink, just update him with new datas 
                _ = UpdateDrink(drinkFromMachine.Id, request);
                return Ok(drinkFromMachine.Id);
            }

            var (drink, error) = Drink.CreateDrink
                (Guid.NewGuid(), request.Name, request.Discription, request.ImageUri, request.Amount, request.Price);

            if (!string.IsNullOrEmpty(error))
            {
                Console.WriteLine(error);
                return BadRequest(Guid.Empty);
            }

            await _drinksService.CreateDrink(drink);

            return Ok(drink.Id);
        }


        /// <summary>
        /// Put methods for update drinks
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("Drink/{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateDrink(Guid id, [FromBody] DrinkRequest request)
        {
            var error = Drink.DrinkIsExist(id, request.Name, request.Discription, request.ImageUri, request.Amount, request.Price);

            if (!string.IsNullOrEmpty(error))
            {
                Console.WriteLine(error);
                return BadRequest(error);
            }

            await _drinksService.UpdateDrink(id, request.Name, request.Discription, request.ImageUri, request.Amount, request.Price);
            return Ok(id);
        }

        /// <summary>
        /// For update drink (AMOUNT ONLY)
        /// </summary>
        /// <param name="requests"></param>
        /// <returns></returns>
        [HttpPut("Drink")]
        public async Task<ActionResult<List<Guid>>> UpdateDrinksAmount([FromBody] List<DrinkResponce> requests)
        {
            foreach (var request in requests)
            {
                var error = Drink.DrinkIsExist(request.Id, request.Name, request.Discription, request.ImageUri, request.Amount, request.Price);

                if (!string.IsNullOrEmpty(error))
                {
                    Console.WriteLine(error);
                    return BadRequest(error);
                }
                await _drinksService.UpdateDrinksAmount(request.Id, request.Amount);
            }

            return Ok(requests.Select(i => i.Id).ToList());
        }

        /// <summary>
        /// Delete methods for drinks
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Drink/{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteDrink(Guid id)
        {
            await _drinksService.DeleteDrink(id);
            return Ok(id);
        }

        /// <summary>
        /// Get method for coins
        /// </summary>
        /// <returns></returns>
        [HttpGet("Coin")]
        public async Task<ActionResult<List<CoinResponce>>> GetCoins()
        {
            var coins = await _coinService.GetAllCoins();

            var responce = coins.Select(c => new CoinResponce(c.Id, c.Amount, c.Denomination, c.IsAvailable));

            return Ok(responce.ToList());
        }

        /// <summary>
        /// For create coin
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Coin")]
        public async Task<ActionResult<Guid>> CreateCoin([FromBody] CoinRequest request)
        {
            var coins = _coinService.GetAllCoins().Result;

            var coinFromMachine = coins.Select(d => d)
                .Where(d => d.Denomination == request.Denomination)
                .FirstOrDefault();

            if (coinFromMachine != null)
            {
                //if DB contain this coin, just update him with new datas 
                _ = UpdateCoin(coinFromMachine.Id, request);
                return Ok(coinFromMachine.Id);
            }

            var (coin, error) = Coin.CreateCoin
                (Guid.NewGuid(), request.Amount, request.Denomination, request.isAvailable);

            if (!string.IsNullOrEmpty(error))
            {
                Console.WriteLine(error);
                return BadRequest(Guid.Empty);
            }

            await _coinService.CreateCoin(coin);

            return Ok(coin.Id);
        }

        /// <summary>
        /// For update coin
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("Coin/{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateCoin(Guid id, [FromBody] CoinRequest request)
        {
            var error = Coin.CoinIsExist(id, request.Amount, request.Denomination, request.isAvailable);

            if (!string.IsNullOrEmpty(error))
            {
                Console.WriteLine(error);
                return BadRequest(error);
            }
            await _coinService.UpdateCoin(id, request.Amount, request.Denomination, request.isAvailable);
            return Ok(id);
        }

        /// <summary>
        /// Method for update coin (AMOUNT ONLY)
        /// </summary>
        /// <param name="requests"></param>
        /// <returns></returns>
        [HttpPut("Coin")]
        public async Task<ActionResult<List<Guid>>> UpdateCoinsAmount([FromBody] List<CoinResponce> requests)
        {
            foreach (var request in requests)
            {
                await _coinService.UpdateCoinAmount(request.Id, request.Amount);
            }

            return Ok(requests.Select(i => i.Id).ToList());
        }

        /// <summary>
        /// For delete coin
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Coin/{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteCoin(Guid id)
        {
            await _coinService.DeleteCoin(id);
            return Ok(id);
        }
    }
}
