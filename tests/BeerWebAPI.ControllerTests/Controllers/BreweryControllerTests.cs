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
    /// Test class that contains test cases for Brewery Controller.
    /// </summary>
    public class BreweryControllerTests
    {
        //mocking IBreweryService 
        readonly Mock<IBreweryService> breweryService;
        public BreweryControllerTests()
        {
            breweryService = new Mock<IBreweryService>();

        }

        [Fact]
        public void AddBrewery_ReturnSuccess_ForValidRequest()
        {
            //Arrange
            BreweryModel breweryModel = new() { Name = "Asahi Brewery", Id = 1 };
            breweryService.Setup(x => x.CreateBrewery(breweryModel)).Returns(true);
            var breweryController = new BreweryController(breweryService.Object);

            //Act
            var response = breweryController.AddBrewery(breweryModel);

            //Assert
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, ((ObjectResult)response).StatusCode);
        }

        [Fact]
        public void GetAllBrewery_ReturnSuccess_ForValidRequest()
        {
            //Arrange
            var listResponse = BreweryControllerTestData.GetBreweryList();
            breweryService.Setup(x => x.GetAllBreweries()).Returns(listResponse);
            var breweryController = new BreweryController(breweryService.Object);

            //Act
            var response = breweryController.GetAllBrewery();
            var listBreweryResponse = ((ObjectResult)response).Value as List<BreweryModel>;

            //Assert
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, ((ObjectResult)response).StatusCode);
            Assert.Equal(listResponse.Count, listBreweryResponse.Count);
            Assert.Equal(listResponse[0].Name, listBreweryResponse[0].Name);
        }

        [Fact]
        public void GetBrewery_ReturnSuccess_ForValidBreweryId()
        {
            //Arrange
            breweryService.Setup(x => x.GetBreweryDetailsById(1)).Returns(BreweryControllerTestData.GetBarListById(1));
            var breweryController = new BreweryController(breweryService.Object);

            //Act
            var response = breweryController.GetBreweryById(1);
            var breweryModel = ((ObjectResult)response).Value as BreweryModel;

            //Assert
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, ((ObjectResult)response).StatusCode);
            Assert.Equal(1, breweryModel.Id);
        }

        [Fact]
        public void GetBrewery_ReturnNotFound_ForInvalidBreweryId()
        {
            //Arrange
            breweryService.Setup(x => x.GetBreweryDetailsById(3)).Returns(BreweryControllerTestData.GetBarListById(3));
            var breweryController = new BreweryController(breweryService.Object);

            //Act
            var response = breweryController.GetBreweryById(3);

            //Assert
            Assert.IsType<NotFoundObjectResult>(response);
            Assert.Equal(404, ((ObjectResult)response).StatusCode);
        }

        [Fact]
        public void GetAllBreweries_ReturnNotFound_ForNonExistingBrewery()
        {
            //Arrange
            var breweryController = new BreweryController(breweryService.Object);

            //Act
            var response = breweryController.GetAllBrewery();

            //Assert
            Assert.IsType<NotFoundResult>(response);
            Assert.Equal(404, ((NotFoundResult)response).StatusCode);
        }

        [Fact]
        public void UpdateBeer_ReturnBadRequest_ForInvalidRequest()
        {
            //Arrange
            BreweryModel breweryModel = new() { Id = 1, Name = "" };
            var brewreyController = new BreweryController(breweryService.Object);
            brewreyController.ModelState.AddModelError("Name", "Name is required");

            //Act
            var response = brewreyController.UpdateBrewery(breweryModel, 1);

            //Assert
            Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal(400, ((ObjectResult)response).StatusCode);
            breweryService.Verify(repo => repo.ModifyBrewery(breweryModel, 1), Times.Never);
        }

        [Fact]
        public void UpdateBrewery_ReturnSuccess_ForValidRequest()
        {
            //Arrange
            BreweryModel breweryModel = new() { Name = "New Brewery" };
            breweryService.Setup(x => x.ModifyBrewery(breweryModel, 1)).Returns(true);
            var breweryController = new BreweryController(breweryService.Object);

            //Act
            var response = breweryController.UpdateBrewery(breweryModel, 1);

            //Assert
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, ((ObjectResult)response).StatusCode);
        }
    }
}