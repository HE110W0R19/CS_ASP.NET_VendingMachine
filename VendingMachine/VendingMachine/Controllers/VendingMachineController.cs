using Microsoft.AspNetCore.Mvc;
using VendingMachine.Application.Services;
using VendingMachine.Contracts;
using VendingMachine.Core.Models;

namespace VendingMachine.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendingMachineController : Controller
    {
        private readonly IDrinksService _drinksService;
        private readonly ICoinService _coinService;

        public VendingMachineController(IDrinksService drinksService, ICoinService coinService)
        {
            _drinksService = drinksService;
            _coinService = coinService;
        }

        /// <summary>
        /// For get All drinks for view 
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
        /// For get drink by id before buy
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Drink/{id:guid}")]
        public async Task<ActionResult<DrinkResponce?>> GetDrinkById(Guid id)
        {
            var drinks = await _drinksService.GetAllDrinks();

            var responce = drinks.Select(d => new DrinkResponce(d.Id, d.Name, d.Description, d.ImageUri, d.Amount, d.Price)).Where(d => d.Id == id).FirstOrDefault();

            return Ok(responce);
        }

        /// <summary>
        /// For update drink amount after buy
        /// </summary>
        /// <param name="requests"></param>
        /// <returns></returns>
        [HttpPut("Drink")]
        public async Task<ActionResult<List<Guid>>> UpdateDrinksAmount([FromBody] List<DrinkResponce> requests)
        {
            foreach (var request in requests)
            {
                await _drinksService.UpdateDrinksAmount(request.Id, request.Amount);
            }
            
            return Ok(requests.Select(i => i.Id).ToList());
        }

        [HttpPut("Coin")]
        public async Task<ActionResult<List<Guid>>> UpdateCoinsAmount([FromBody] List<CoinResponce> requests)
        {
            foreach(var request in requests)
            {
                await _coinService.UpdateCoinAmount(request.Id, request.Amount);
            }

            return Ok(requests.Select(i => i.Id).ToList());
        }

    }
}
