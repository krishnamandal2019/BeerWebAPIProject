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
    /// Test class that contains test cases for Beer Controller.
    /// </summary>
    public class BeerControllerTests
    {
        //mocking IbeerService 
        readonly Mock<IBeerService> beerService;
        BeerModel? beerModel;
        public BeerControllerTests()
        {
            beerService = new Mock<IBeerService>();
        }

        [Fact]
        public void AddBeerDetails_RetrunSuccess_ForValidRequest()
        {
            //Arrange
            beerModel = new BeerModel { Name = "Kingfisher", PercentageAlcoholByVolume = 0.8M, Id = 1 };
            beerService.Setup(x => x.CreateBeer(beerModel)).Returns(true);
            var beerController = new BeerController(beerService.Object);

            //Act
            var response = beerController.AddBeer(beerModel);

            //Assert
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, ((ObjectResult)response).StatusCode);
        }

        [Fact]
        public void GetBeerDetails_ReturnSuccess_ForValidInputParameter()
        {
            //Arrange
            var listBeerModel = BeerControllerTestData.GetBeerListByParameter(8.0M, 5.0M);
            beerService.Setup(x => x.GetAllBeers(8.0M, 5.0M)).Returns(listBeerModel);
            var beerController = new BeerController(beerService.Object);

            //Act
            var response = beerController.GetBeerByParameter(8.0M, 5.0M);
            var listBeerModelRes = ((ObjectResult)response).Value as List<BeerModel>;

            //Assert
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, ((ObjectResult)response).StatusCode);
            Assert.Equal(listBeerModel.Count, listBeerModelRes.Count);
            Assert.Equal(listBeerModel[0].Name, listBeerModelRes[0].Name);
        }

        [Fact]
        public void UpdateBeerDetails_ReturnSuccess_ForValidRequest()
        {
            //Arrange
            beerModel = new BeerModel { Name = "TestBeer", PercentageAlcoholByVolume = 0.6M };
            beerService.Setup(x => x.ModifyBeer(beerModel, 1)).Returns(true);
            var beerController = new BeerController(beerService.Object);

            //Act
            var response = beerController.UpdateBeer(beerModel, 1);

            //Assert
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, ((ObjectResult)response).StatusCode);
        }

        [Fact]
        public void UpdateBeer_ReturnBadRequest_ForInvalidModel()
        {
            //Arrange
            BeerModel beerModel = new() { Id = 1, Name = "", PercentageAlcoholByVolume = 8.0M };
            beerService.Setup(x => x.ModifyBeer(beerModel, 1)).Returns(false);
            var beerController = new BeerController(beerService.Object);
            beerController.ModelState.AddModelError("Name", "Name is required");

            //Act
            var response = beerController.UpdateBeer(beerModel, 1);

            //Assert
            Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal(400, ((BadRequestObjectResult)response).StatusCode);
            beerService.Verify(repo => repo.ModifyBeer(beerModel, 1), Times.Never);
        }

        [Fact]
        public void GetBeerDetailsById_ReturnSuccess_ForValidRequest()
        {
            //Arrange
            beerService.Setup(x => x.GetBeerDetailsById(1)).Returns(BeerControllerTestData.GetBeerListById(1));
            var barController = new BeerController(beerService.Object);

            //Act
            var response = barController.GetBeerById(1);
            var beerModel = ((ObjectResult)response).Value as BeerModel;

            //Assert
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(1, beerModel.Id);
            Assert.Equal(200, ((ObjectResult)response).StatusCode);
        }

        [Fact]
        public void GetBeerDetails_ReturnNotFound_ForInvalidBeerId()
        {
            //Arrange
            beerService.Setup(x => x.GetBeerDetailsById(3)).Returns(BeerControllerTestData.GetBeerListById(3));
            var beerController = new BeerController(beerService.Object);

            //Act
            var response = beerController.GetBeerById(3);

            //Assert
            Assert.IsType<NotFoundObjectResult>(response);
            Assert.Equal(404, ((NotFoundObjectResult)response).StatusCode);
        }
    }
}