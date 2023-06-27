using Microsoft.AspNetCore.Mvc;
using BeerWebAPI.Service.Interface;
using BeerWebAPI.Shared.Models;

namespace BeerWebAPI.Controllers
{
    /// <summary>
    /// This controller class contain all action method related to Brewery APIs.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class BreweryController : ControllerBase
    {
        private readonly IBreweryService _breweryService;
        public BreweryController(IBreweryService breweryService)
        {
            this._breweryService = breweryService;
        }

        /// <summary>
        /// Add new brewery
        /// </summary>
        /// <param name="brewery"></param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        public IActionResult AddBrewery([FromBody] BreweryModel brewery)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (_breweryService.CreateBrewery(brewery))
                return Ok("Brewery Added Successfully");
            return StatusCode(500, "Something went wrong");
        }

        /// <summary>
        /// Update brewery by Id
        /// </summary>
        /// <param name="brewery"></param>
        /// <param name="id"></param>
        /// <returns>IActionResult</returns>
        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateBrewery([FromBody] BreweryModel brewery, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            bool result = _breweryService.ModifyBrewery(brewery, id);
            if (result)
                return Ok("Brewery details updated successfully");
            return NotFound("Brewery Id does not exist");
        }

        /// <summary>
        /// Get all breweries
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet]
        public IActionResult GetAllBrewery()
        {
            var result = _breweryService.GetAllBreweries();
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Get brewery by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IActionResult</returns>
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetBreweryById(int id)
        {
            var result = _breweryService.GetBreweryDetailsById(id);
            if (result == null)
                return NotFound("Brewery id does not exist");
            return Ok(result);
        }
    }
}