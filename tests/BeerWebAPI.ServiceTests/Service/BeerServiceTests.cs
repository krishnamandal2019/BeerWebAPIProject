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
    /// Test class that contains test cases for Beer Service
    /// </summary>
    public class BeerServiceTests
    {
        readonly Mock<IBeerRepository> beerRepository;
        readonly Mock<IRepository<BeerDBModel>> repository;
        public BeerServiceTests()
        {
            beerRepository = new Mock<IBeerRepository>();
            repository = new Mock<IRepository<BeerDBModel>>();
        }

        [Fact]
        public void AddBeer_ReturnSuccess_ForValidRequest()
        {
            //Arrange
            BeerModel beerModel = new() { Name = "Kingfisher", PercentageAlcoholByVolume = 7.0M, Id = 1 };
            repository.Setup(x => x.Add(It.IsAny<BeerDBModel>())).Returns(true);
            var beerService = new BeerService(beerRepository.Object, repository.Object);

            //Act
            var response = beerService.CreateBeer(beerModel);

            //Assert
            Assert.True(response);
            repository.Verify(repo => repo.Add(It.IsAny<BeerDBModel>()), Times.Once());
        }

        [Fact]
        public void GetBeerDetails_ReturnSuccess_ForValidParameter()
        {
            //Arrange
            beerRepository.Setup(x => x.GetBeerByAlcoholParameter(10.0M, 5.0M)).Returns(BeerServiceTestData.GetBeerListByParameter(10.0M, 5.0M));
            var beerService = new BeerService(beerRepository.Object, repository.Object);

            //Act
            List<BeerModel>? listBeer = beerService.GetAllBeers(10.0M, 5.0M);

            //Assert
            Assert.NotNull(listBeer);
            Assert.Single(listBeer);
            beerRepository.Verify(repo => repo.GetBeerByAlcoholParameter(10.0M, 5.0M), Times.Once());
        }

        [Fact]
        public void GetBeerDetails_ReturnSuccess_ForValidBeerId()
        {
            //Arrange
            var beerModel = BeerServiceTestData.GetBeerListById(1);
            repository.Setup(x => x.GetById(1)).Returns(beerModel);
            var beerService = new BeerService(beerRepository.Object, repository.Object);

            //Act
            BeerModel beer = beerService.GetBeerDetailsById(1);

            //Assert
            Assert.NotNull(beer);
            Assert.Equal(beerModel.Name, beer.Name);
            repository.Verify(repo => repo.GetById(1), Times.Once());
        }

        [Fact]
        public void GetBeerDetails_ReturnsNull_ForInvalidBeerId()
        {
            //Arrange
            repository.Setup(x => x.GetById(3)).Returns(BeerServiceTestData.GetBeerListById(3));
            var beerService = new BeerService(beerRepository.Object, repository.Object);

            //Act
            BeerModel beerModel = beerService.GetBeerDetailsById(3);

            //Assert
            Assert.Null(beerModel);
            repository.Verify(repo => repo.GetById(3), Times.Once());
        }

        [Fact]
        public void UpdateBeerDetail_ReturnSuccess_ForValidRequest()
        {
            //Arrange
            BeerModel beerModel = new() { Name = "Tuburg", PercentageAlcoholByVolume = 8.0M };
            repository.Setup(x => x.GetById(1)).Returns(BeerServiceTestData.GetBeerListById(1));
            repository.Setup(x => x.Update(It.IsAny<BeerDBModel>())).Returns(true);
            var beerService = new BeerService(beerRepository.Object, repository.Object);

            //Act
            bool response = beerService.ModifyBeer(beerModel, 1);

            //Assert
            Assert.True(response);
            repository.Verify(repo => repo.GetById(1), Times.Once());
            repository.Verify(repo => repo.Update(It.IsAny<BeerDBModel>()), Times.Once());
        }

        [Fact]
        public void UpdateBeerDetail_ReturnNotSuccess_ForInvalidBeerId()
        {
            //Arrange
            BeerModel beerModel = new() { Name = "Tuburg", PercentageAlcoholByVolume = 8.0M };
            repository.Setup(x => x.GetById(3)).Returns(BeerServiceTestData.GetBeerListById(3));
            var beerService = new BeerService(beerRepository.Object, repository.Object);

            //Act
            bool response = beerService.ModifyBeer(beerModel, 3);

            //Assert
            Assert.False(response);
            repository.Verify(repo => repo.GetById(3), Times.Once());
        }
    }
}