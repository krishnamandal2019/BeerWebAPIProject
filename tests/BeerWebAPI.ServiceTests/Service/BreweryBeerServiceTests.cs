using BeerWebAPI.DataAccess.Interface;
using Moq;
using Xunit;
using BeerWebAPI.ServiceTests.TestData;
using BeerWebAPI.Shared.Models;
using BeerWebAPI.DataAccess.DbModel;
using BeerWebAPI.Service.Services;

namespace BeerWebAPI.ServiceTests.Service
{
    /// <summary>
    /// Test class that contains test cases for BreweryBeer Service
    /// </summary>
    public class BreweryBeerServiceTests
    {
        readonly Mock<IRelationalRepository<BreweryBeerDBModel, BreweryBeerResponseModel>> breweryBeerRepository;
        public BreweryBeerServiceTests()
        {
            breweryBeerRepository = new Mock<IRelationalRepository<BreweryBeerDBModel, BreweryBeerResponseModel>>();
        }

        [Fact]
        public void AddBeerToBrewery_ReturnSuccess_ForValidRequest()
        {
            //Arrange
            breweryBeerRepository.Setup(x => x.Add(It.IsAny<BreweryBeerDBModel>())).Returns(true);
            var breweryBeerService = new BreweryBeerService(breweryBeerRepository.Object);

            //Act
            var response = breweryBeerService.IntroduceBeerToBrewery(1, 1);

            //Assert
            Assert.True(response);
            breweryBeerRepository.Verify(repo => repo.Add(It.IsAny<BreweryBeerDBModel>()), Times.Once());
        }

        [Fact]
        public void GetBreweryWithServedBeerDetails_ReturnSuccess_ForValidBreweryId()
        {
            //Arrange
            BreweryBeerResponseModel responseModel = BreweryBeerServiceTestData.GetBreweryBeerListById(1);
            breweryBeerRepository.Setup(x => x.GetById(1)).Returns(responseModel);
            var breweryBeerService = new BreweryBeerService(breweryBeerRepository.Object);

            //Act
            BreweryBeerResponseModel? response = breweryBeerService.GetBreweryWithServedBeersByBreweryId(1);

            //Assert
            Assert.NotNull(response);
            Assert.Equal(responseModel.BreweryName, response.BreweryName);
            breweryBeerRepository.Verify(repo => repo.GetById(1), Times.Once());
        }

        [Fact]
        public void GetAllBreweryWithServedBeer_RetrunSuccess_ForValidRequest()
        {
            //Arrange
            List<BreweryBeerResponseModel> responseModels = BreweryBeerServiceTestData.GetBreweryBeerList();
            breweryBeerRepository.Setup(x => x.GetAll()).Returns(responseModels);
            var breweryBeerService = new BreweryBeerService(breweryBeerRepository.Object);

            //Act
            List<BreweryBeerResponseModel> listResponse = breweryBeerService.GetAllBreweriesWithServedBeers();

            //Assert
            Assert.NotNull(listResponse);
            Assert.Equal(responseModels.Count, listResponse.Count);
            Assert.Equal(responseModels[0].BreweryName, listResponse[0].BreweryName);
            breweryBeerRepository.Verify(repo => repo.GetAll(), Times.Once());
        }

        [Fact]
        public void GetBreweryWithServedBeers_ReturnsNull_ForInvalidBreweryId()
        {
            //Arrange
            breweryBeerRepository.Setup(x => x.GetById(3)).Returns(BreweryBeerServiceTestData.GetBreweryBeerListById(3));
            var breweryBeerService = new BreweryBeerService(breweryBeerRepository.Object);

            //Act
            BreweryBeerResponseModel? responseModel = breweryBeerService.GetBreweryWithServedBeersByBreweryId(3);

            //Assert
            Assert.Null(responseModel);
            breweryBeerRepository.Verify(repo => repo.GetById(3), Times.Once());
        }
    }
}