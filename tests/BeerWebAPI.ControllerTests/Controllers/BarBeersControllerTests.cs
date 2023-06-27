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
    /// Test class that contains test cases for BarBeer Controller.
    /// </summary>
    public class BarBeersControllerTests
    {
        //Mocking IBarBeerService 
        readonly Mock<IBarBeerService> barBeerService;
        public BarBeersControllerTests()
        {
            barBeerService = new Mock<IBarBeerService>();
        }

        [Fact]
        public void AddBeerToBar_ReturnSuccess_ForValidRequest()
        {
            //Arrange
            BarBeerModel barBeerModel = new() { BarId = 1, BeerId = 1 };
            barBeerService.Setup(x => x.IntroduceBeerTobar(1, 1)).Returns(true);
            var barBeerController = new BarBeersController(barBeerService.Object);

            //Act
            var response = barBeerController.AddBeerToBar(barBeerModel);

            //Assert
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, ((ObjectResult)response).StatusCode);
        }

        [Fact]
        public void GetAllBarWithServedBeers_ReturnSuccess_ForValidRequest()
        {
            //Arrange
            var listBarBeer = BarBeerControllerTestData.GetBarBeerList();
            barBeerService.Setup(x => x.GetAllBarsWithServedBeers()).Returns(listBarBeer);
            var barBeerController = new BarBeersController(barBeerService.Object);

            //Act
            var response = barBeerController.GetAllBarWithServedBeer();
            var listBarBeerResponse = ((ObjectResult)response).Value as List<BarBeerResponseModel>;

            //Assert
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, ((ObjectResult)response).StatusCode);
            Assert.Equal(listBarBeer.Count, listBarBeerResponse.Count);
            Assert.Equal(listBarBeer[0].BarName, listBarBeerResponse[0].BarName);
        }

        [Fact]
        public void GetBarWithServedBeers_ReturnSuccess_ForValidBarId()
        {
            //Arrange
            var barBeerModel = BarBeerControllerTestData.GetBarBeerListById(1);
            barBeerService.Setup(x => x.GetBarWithServedBeersByBarId(1)).Returns(barBeerModel);
            var barBeerController = new BarBeersController(barBeerService.Object);

            //Act
            var response = barBeerController.GetBarWithServedBeersById(1);
            var listBarBeerResponse = ((ObjectResult)response).Value as BarBeerResponseModel;

            //Assert
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, ((ObjectResult)response).StatusCode);
            Assert.Equal(barBeerModel.BarId, listBarBeerResponse.BarId);
            Assert.Equal(barBeerModel.BarName, listBarBeerResponse.BarName);
        }

        [Fact]
        public void GetAllBarBeers_ReturnNotFound_ForNonExistingBarBeers()
        {
            //Arrange
            var barBeerController = new BarBeersController(barBeerService.Object);

            //Act
            var response = barBeerController.GetAllBarWithServedBeer();

            //Assert
            Assert.IsType<NotFoundResult>(response);
            Assert.Equal(404, ((NotFoundResult)response).StatusCode);
        }

        [Fact]
        public void GetBarWithServedBeersByBarId_ReturnNotFound_ForInvalidBarId()
        {
            //Arrange
            barBeerService.Setup(x => x.GetBarWithServedBeersByBarId(3)).Returns(BarBeerControllerTestData.GetBarBeerListById(3));
            var barBeerController = new BarBeersController(barBeerService.Object);

            //Act
            var response = barBeerController.GetBarWithServedBeersById(3);

            //Assert
            Assert.IsType<NotFoundResult>(response);
            Assert.Equal(404, ((NotFoundResult)response).StatusCode);
        }
    }
}