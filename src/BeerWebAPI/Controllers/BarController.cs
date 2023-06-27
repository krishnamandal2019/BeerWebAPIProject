using BeerWebAPI.Service.Interface;
using BeerWebAPI.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeerWebAPI.Controllers
{
    /// <summary>
    /// This controller class contain all action method related to Bar APIs.
    /// </summary>
    [Route("[controller]")]
    [ApiController]

    public class BarController : ControllerBase
    {
        private readonly IBarService _barServices;
        public BarController(IBarService barServices)
        {
            this._barServices = barServices;
        }

        /// <summary>
        /// Add new bar
        /// </summary>
        /// <param name="bar"></param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        public IActionResult AddBar([FromBody] BarModel bar)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (_barServices.CreateBar(bar))
                return Ok("Bar Added Successfully");
            return StatusCode(500, "Something went wrong");
        }

        /// <summary>
        /// Update bar based on Id
        /// </summary>
        /// <param name="bar"></param>
        /// <param name="id"></param>
        /// <returns>IActionResult</returns>
        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateBar([FromBody] BarModel bar, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(bar);
            bool result = _barServices.ModifyBar(bar, id);
            if (result)
                return Ok("Bar details updated successfully");
            return NotFound("Bar Id does not exist");
        }

        /// <summary>
        /// Get all bar
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet]
        public IActionResult GetAllBar()
        {
            var result = _barServices.GetAllBars();
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Get bar details by bar id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IActionResult</returns>
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetBarById(int id)
        {
            var result = _barServices.GetBarDetailsById(id);
            if (result == null)
                return NotFound("Bar id does not exist.");
            return Ok(result);
        }
    }
}