using BeerWebAPI.Controllers;
using BeerWebAPI.ControllerTests.TestData;
using BeerWebAPI.Service.Interface;
using BeerWebAPI.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BeerWebAPI.ControllerTests.Controllers
{
    /// <summary>
    /// Test class that contains test cases for BreweryBeer Controller.
    /// </summary>
    public class BreweryBeersControllerTests
    {
        //Mocking IBreweryBeerService 
        readonly Mock<IBreweryBeerService> breweryBeerService;

        public BreweryBeersControllerTests()
        {
            breweryBeerService = new Mock<IBreweryBeerService>();
        }

        [Fact]
        public void AddBeerToBrewery_ReturnSuccess_ForValidRequest()
        {
            //Arrange
            BreweryBeerModel breweryBeerModel = new() { BreweryId = 1, BeerId = 1 };
            breweryBeerService.Setup(x => x.IntroduceBeerToBrewery(1, 1)).Returns(true);
            var breweryBeersController = new BreweryBeersController(breweryBeerService.Object);

            //Act
            var response = breweryBeersController.AddBeerToBrewery(breweryBeerModel);

            //Assert
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, ((ObjectResult)response).StatusCode);
        }

        [Fact]
        public void GetAllBreweryWithServedBeers_ReturnSuccess_ForValidRequest()
        {
            //Arrange
            var listResponse = BreweryBeerControllerTestData.GetBreweryBeerList();
            breweryBeerService.Setup(x => x.GetAllBreweriesWithServedBeers()).Returns(listResponse);
            var breweryBeerController = new BreweryBeersController(breweryBeerService.Object);

            //Act
            var response = breweryBeerController.GetAllBreweryWithServedBeers();
            var listBreweryBeerRes = ((ObjectResult)response).Value as List<BreweryBeerResponseModel>;

            //Assert
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, ((ObjectResult)response).StatusCode);
            Assert.Equal(listResponse.Count, listBreweryBeerRes.Count);
            Assert.Equal(listResponse[0].BreweryName, listBreweryBeerRes[0].BreweryName);
        }

        [Fact]
        public void GetBreweryWithServedBeersByBreweryId_ReturnSuccess_ForValidBreweryId()
        {
            //Arrange
            var breweryBeerResponse = BreweryBeerControllerTestData.GetBreweryBeerListById(1);
            breweryBeerService.Setup(x => x.GetBreweryWithServedBeersByBreweryId(1)).Returns(breweryBeerResponse);
            var breweryBeerController = new BreweryBeersController(breweryBeerService.Object);

            //Act
            var response = breweryBeerController.GetBreweryWithServedBeersById(1);
            var breweryBeerRes = ((ObjectResult)response).Value as BreweryBeerResponseModel;

            //Assert
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, ((ObjectResult)response).StatusCode);
            Assert.Equal(breweryBeerResponse.BreweryId, breweryBeerRes.BreweryId);
            Assert.Equal(breweryBeerResponse.BreweryName, breweryBeerRes.BreweryName);
        }

        [Fact]
        public void GetAllBreweryBeers_ReturnNotFound_ForNonExistingBreweryBeers()
        {
            //Arrange
            var breweryBeerController = new BreweryBeersController(breweryBeerService.Object);

            //Act
            var response = breweryBeerController.GetAllBreweryWithServedBeers();

            //Assert
            Assert.IsType<NotFoundResult>(response);
            Assert.Equal(404, ((NotFoundResult)response).StatusCode);
        }

        [Fact]
        public void GetBreweryWithServedBeers_ReturnNotFound_ForInvalidBreweryId()
        {
            //Arrange
            breweryBeerService.Setup(x => x.GetBreweryWithServedBeersByBreweryId(3)).Returns(BreweryBeerControllerTestData.GetBreweryBeerListById(3));
            var breweryBeerController = new BreweryBeersController(breweryBeerService.Object);

            //Act
            var response = breweryBeerController.GetBreweryWithServedBeersById(3);

            //Assert
            Assert.IsType<NotFoundResult>(response);
            Assert.Equal(404, ((NotFoundResult)response).StatusCode);
        }
    }
}