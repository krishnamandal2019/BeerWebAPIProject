using BeerWebAPI.Service.Interface;
using BeerWebAPI.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeerWebAPI.Controllers
{
    /// <summary>
    /// This controller class contain all action method related to BreweryBeer APIs.
    /// </summary>

    [ApiController]
    public class BreweryBeersController : ControllerBase
    {
        private readonly IBreweryBeerService breweryBeerServices;
        public BreweryBeersController(IBreweryBeerService _breweryBeerServices)
        {
            this.breweryBeerServices = _breweryBeerServices;
        }

        /// <summary>
        /// Add new beer to brewery
        /// </summary>
        /// <param name="breweryBeer"></param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [Route("brewery/beer")]
        public IActionResult AddBeerToBrewery([FromBody] BreweryBeerModel breweryBeer)
        {
            if (breweryBeerServices.IntroduceBeerToBrewery(breweryBeer.BreweryId, breweryBeer.BeerId))
                return Ok("Brewery Beer Linked Successfully");
            return StatusCode(500, "Something went wrong");
        }

        /// <summary>
        /// Get specific brewery with served beers.
        /// </summary>
        /// <param name="breweryid"></param>
        /// <returns>IActionResult</returns>
        [HttpGet]
        [Route("brewery/{breweryId}/beer")]
        public IActionResult GetBreweryWithServedBeersById(int breweryId)
        {
            var result = breweryBeerServices.GetBreweryWithServedBeersByBreweryId(breweryId);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Get all breweries with served beers.
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet]
        [Route("brewery/beer")]
        public IActionResult GetAllBreweryWithServedBeers()
        {
            var result = breweryBeerServices.GetAllBreweriesWithServedBeers();
            if (result == null)
                return NotFound();
            return Ok(result);

        }
    }
}