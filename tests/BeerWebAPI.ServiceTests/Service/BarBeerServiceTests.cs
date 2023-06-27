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
    /// Test class that contains test cases for BarBeer Service
    /// </summary>
    public class BarBeerServiceTests
    {
        readonly Mock<IRelationalRepository<BarBeerDBModel, BarBeerResponseModel>> barBeerRepository;
        public BarBeerServiceTests()
        {
            barBeerRepository = new Mock<IRelationalRepository<BarBeerDBModel, BarBeerResponseModel>>();
        }

        [Fact]
        public void AddBeerToBar_ReturnSuccess_ForValidInput()
        {
            //Arrange
            barBeerRepository.Setup(x => x.Add(It.IsAny<BarBeerDBModel>())).Returns(true);
            var barBeerService = new BarBeerService(barBeerRepository.Object);

            //act
            bool response = barBeerService.IntroduceBeerTobar(1, 1);

            //Assert
            Assert.True(response);
            barBeerRepository.Verify(repo => repo.Add(It.IsAny<BarBeerDBModel>()), Times.Once());
        }

        [Fact]
        public void GetBarWithServedBeerDetails_ReturnSuccess_ForValidBarId()
        {
            //Arrange
            BarBeerResponseModel? barBeerResponseModel = BarBeerServiceTestData.GetBarBeerListById(1);
            barBeerRepository.Setup(x => x.GetById(1)).Returns(barBeerResponseModel);
            var barBeerService = new BarBeerService(barBeerRepository.Object);

            //Act
            BarBeerResponseModel? barBeerResponse = barBeerService.GetBarWithServedBeersByBarId(1);

            //Assert
            Assert.NotNull(barBeerResponse);
            Assert.Equal(barBeerResponse.BarName, barBeerResponseModel.BarName);
            barBeerRepository.Verify(repo => repo.GetById(1), Times.Once());
        }

        [Fact]
        public void GetAllBarsWithServedBeer_ReturnSuccess_ForValidRequest()
        {
            //Arrange
            List<BarBeerResponseModel>? barBeerResponseModels = BarBeerServiceTestData.GetBarBeerList();
            barBeerRepository.Setup(x => x.GetAll()).Returns(barBeerResponseModels);
            var barBeerService = new BarBeerService(barBeerRepository.Object);

            //Act
            List<BarBeerResponseModel>? listBarBeerResponseModels = barBeerService.GetAllBarsWithServedBeers();

            //Assert
            Assert.NotNull(listBarBeerResponseModels);
            Assert.Equal(listBarBeerResponseModels.Count, barBeerResponseModels.Count);
            Assert.Equal(listBarBeerResponseModels[0].BarName, barBeerResponseModels[0].BarName);
            barBeerRepository.Verify(repo => repo.GetAll(), Times.Once());
        }

        [Fact]
        public void GetBarBeers_ReturnsNull_ForInvalidBarId()
        {
            //Arrange
            barBeerRepository.Setup(x => x.GetById(3)).Returns(BarBeerServiceTestData.GetBarBeerListById(3));
            var barBeerService = new BarBeerService(barBeerRepository.Object);

            //Act
            BarBeerResponseModel? barBeerResponseModel = barBeerService.GetBarWithServedBeersByBarId(3);

            //Assert
            Assert.Null(barBeerResponseModel);
            barBeerRepository.Verify(repo => repo.GetById(3), Times.Once());
        }
    }
}