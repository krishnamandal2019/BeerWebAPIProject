using BeerWebAPI.Service.Interface;
using BeerWebAPI.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeerWebAPI.Controllers
{
    /// <summary>
    /// This controller class contain all action method related to Beer APIs.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private readonly IBeerService _beerServices;
        public BeerController(IBeerService beerServices)
        {
            this._beerServices = beerServices;
        }

        /// <summary>
        /// Add new beer
        /// </summary>
        /// <param name="beer"></param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        public IActionResult AddBeer([FromBody] BeerModel beer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (_beerServices.CreateBeer(beer))
                return Ok("Beer Added Successfully");
            return StatusCode(500, "Something went wrong");
        }

        /// <summary>
        /// update Beer by Id
        /// </summary>
        /// <param name="bar"></param>
        /// <param name="id"></param>
        /// <returns>IActionResult</returns>
        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateBeer([FromBody] BeerModel beer, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            bool result = _beerServices.ModifyBeer(beer, id);
            if (result)
                return Ok("Beer details updated successfully");
            return NotFound("Beer id does not exist");
        }

        /// <summary>
        /// Get beer with alcohol parameters
        /// </summary>
        /// <param name="barQueryParameter"></param>
        /// <returns>IActionResult</returns>
        [HttpGet]
        public IActionResult GetBeerByParameter([FromQuery] decimal lessthanAlcoholPercentage, [FromQuery] decimal greaterthanAlcoholPercentage)
        {
            var result = _beerServices.GetAllBeers(lessthanAlcoholPercentage, greaterthanAlcoholPercentage);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Get beer details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IActionResult</returns>
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetBeerById(int id)
        {
            var result = _beerServices.GetBeerDetailsById(id);
            if (result == null)
                return NotFound("Beer id does not exist");
            return Ok(result);
        }
    }
}