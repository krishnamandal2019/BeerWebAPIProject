using Microsoft.AspNetCore.Mvc;
using BeerWebAPI.Service.Interface;
using BeerWebAPI.Shared.Models;

namespace BeerWebAPI.Controllers
{
    /// <summary>
    /// This controller class contain all action method related to BarBeer APIs.
    /// </summary>
    [ApiController]
    public class BarBeersController : ControllerBase
    {
        private readonly IBarBeerService _barBeerServices;
        public BarBeersController(IBarBeerService beerBarServices)
        {
            _barBeerServices = beerBarServices;
        }

        /// <summary>
        /// Add new beer to a bar.
        /// </summary>
        /// <param name="barBeer"></param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [Route("bar/beer")]
        public IActionResult AddBeerToBar([FromBody] BarBeerModel barBeer)
        {
            if (_barBeerServices.IntroduceBeerTobar(barBeer.BarId, barBeer.BeerId))
                return Ok("Bar beers linked successfully");
            return StatusCode(500, "Something went wrong");
        }
        /// <summary>
        /// Get specific bar with served beers by barId
        /// </summary>
        /// <param name="barId"></param>
        /// <returns>IActionResult</returns>
        [HttpGet]
        [Route("bar/{barId}/beer")]
        public IActionResult GetBarWithServedBeersById(int barId)
        {
            var result = _barBeerServices.GetBarWithServedBeersByBarId(barId);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
        /// <summary>
        /// Get all bars with served beers details
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet]
        [Route("bar/beer")]
        public IActionResult GetAllBarWithServedBeer()
        {
            var result = _barBeerServices.GetAllBarsWithServedBeers();
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}