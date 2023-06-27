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
    /// Test class that contains test cases for Bar Controller.
    /// </summary>
    public class BarControllerTests
    {

        //mocking IBarService 
        readonly Mock<IBarService> barService;

        public BarControllerTests()
        {
            barService = new Mock<IBarService>();
        }

        [Fact]
        public void AddBarDetails_ReturnSuccess_ForValidRequest()
        {
            //Arrange
            BarModel barModel = new() { Name = "Kolkata Club", Address = "New Delhi", Id = 1 };
            barService.Setup(x => x.CreateBar(barModel)).Returns(true);
            var barController = new BarController(barService.Object);

            //Act
            var response = barController.AddBar(barModel);

            //Assert
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, ((ObjectResult)response).StatusCode);
        }

        [Fact]
        public void UpdateBar_ReturnSuccess_ForValidRequest()
        {
            //Arrange
            BarModel barModel = new() { Name = "Test bar", Address = "Noida" };
            barService.Setup(x => x.ModifyBar(barModel, 1)).Returns(true);
            var barController = new BarController(barService.Object);

            //Act
            var response = barController.UpdateBar(barModel, 1);

            //Assert
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, ((ObjectResult)response).StatusCode);
        }

        [Fact]
        public void UpdateBar_ReturnBadRequest_ForInvalidModel()
        {
            //Arrange
            BarModel barModel = new() { Name = "", Address = "", Id = 1 };
            barService.Setup(x => x.ModifyBar(barModel, 1)).Returns(false);
            var barController = new BarController(barService.Object);
            barController.ModelState.AddModelError("Name", "Name is required");

            //Act
            var response = barController.UpdateBar(barModel, 1);

            //Assert
            Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal(400, ((BadRequestObjectResult)response).StatusCode);
            barService.Verify(repo => repo.ModifyBar(barModel, 1), Times.Never);
        }

        [Fact]
        public void GetAllBars_ReturnSuccess_ForValidRequest()
        {
            //Arrange
            var listBarModel = BarControllerTestData.GetBarList();
            barService.Setup(x => x.GetAllBars()).Returns(listBarModel);
            var barController = new BarController(barService.Object);

            //Act
            var response = barController.GetAllBar();
            var listBarResponse = ((ObjectResult)response).Value as List<BarModel>;

            //Assert
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, ((ObjectResult)response).StatusCode);
            Assert.Equal(listBarModel.Count, listBarResponse.Count);
            Assert.Equal(listBarModel[0].Name, listBarResponse[0].Name);
        }

        [Fact]
        public void GetBarDetailsById_ReturnSuccess_ForValidRequest()
        {
            //Arrange
            barService.Setup(x => x.GetBarDetailsById(1)).Returns(BarControllerTestData.GetBarListById(1));
            var barController = new BarController(barService.Object);

            //Act
            var response = barController.GetBarById(1);
            var barModel = ((ObjectResult)response).Value as BarModel;

            //Assert
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, ((ObjectResult)response).StatusCode);
            Assert.Equal(1, barModel.Id);
        }

        [Fact]
        public void GetBar_ReturnNotFound_ForInvalidBarId()
        {
            //Arrange
            barService.Setup(x => x.GetBarDetailsById(3)).Returns(BarControllerTestData.GetBarListById(3));
            var barController = new BarController(barService.Object);

            //Act
            var response = barController.GetBarById(3);

            //Assert
            Assert.IsType<NotFoundObjectResult>(response);
            Assert.Equal(404, ((NotFoundObjectResult)response).StatusCode);
        }

        [Fact]
        public void GetAllBars_ReturnNotFound_ForNonExistingBars()
        {
            //Arrange
            var barController = new BarController(barService.Object);

            //Act
            var response = barController.GetAllBar();

            //Assert
            Assert.IsType<NotFoundResult>(response);
            Assert.Equal(404, ((NotFoundResult)response).StatusCode);
        }
    }
}